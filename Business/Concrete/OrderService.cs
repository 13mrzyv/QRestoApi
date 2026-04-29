using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task PlaceOrderAsync(CreateOrderRequest request)
        {
            _unitOfWork.BeginTransaction();
            try
            {

                decimal totalAmount = 0;
                var orderItems = new List<OrderItem>();

                foreach (var itemReq in request.Items)
                {
                    var product = await _unitOfWork.ProductsRepository.GetProductByIdAsync(itemReq.ProductId);

                    if (product == null)
                    {
                        throw new Exception($"{itemReq.ProductId} nömrəli məhsul tapılmadı!");
                    }

                    if (product.IsAvailable == false) // Stopda olub-olmadığını yoxlayırıq
                    {
                        throw new Exception($"{product.Name} hal-hazırda stokda yoxdur!");
                    }

                    decimal? price = product.Price;
                    //decimal? price = await _unitOfWork.ProductsRepository.GetProductPriceByIdAsync(itemReq.ProductId);

                    if (!price.HasValue) // və ya price == null
                    {
                        throw new Exception($"{itemReq.ProductId} nömrəli məhsul tapılmadı!");
                    }

                    totalAmount += price.Value * itemReq.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = itemReq.ProductId,
                        Quantity = itemReq.Quantity,
                        UnitPrice = price.Value // Bazadan gələn real qiymət
                    });
                }
                var order = new Order
                {
                    TableId = request.TableId,
                    OrderDate = DateTime.Now,
                    Status = 1, // Yeni sifariş
                    TotalAmount = totalAmount
                };

                int TableStatus = await _unitOfWork.TablesRepository.GetTableStatusAsync(request.TableId);

                if (TableStatus == 0)
                {
                    int newOrderId = await _unitOfWork.OrdersRepository.InsertOrderAsync(order);
                    orderItems.ForEach(x => x.OrderId = newOrderId);
                    await _unitOfWork.OrdersRepository.InsertOrderItemsAsync(orderItems);
                    await _unitOfWork.TablesRepository.UpdateTableStatusAsync(request.TableId, 1);
                }
                else
                {
                    //demeli masa dolusu sifarisi hazirda hemin orderid nin uzerine atacayiq
                    var existingOrder = await _unitOfWork.OrdersRepository.GetActiveOrderByTableIdAsync(request.TableId);
                    decimal updatedTotal = existingOrder.TotalAmount + totalAmount;
                    await _unitOfWork.OrdersRepository.UpdateTotalAmountByIdAsync(existingOrder.Id, updatedTotal);
                    orderItems.ForEach(x => x.OrderId = existingOrder.Id);
                    await _unitOfWork.OrdersRepository.InsertOrderItemsAsync(orderItems); ;
                }
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw; // Xətanı yuxarı (Controller-ə) ötür ki, istifadəçi bilsin
            }
        }

        public async Task<OrderDetailResponse> GetOrderDetailsAsync(int tableId)
        {
            var order = await _unitOfWork.OrdersRepository.GetActiveOrderByTableIdAsync(tableId);
            if (order == null)
                return null;
            var items = await _unitOfWork.OrdersRepository.GetOrderItemsByOrderIdAsync(order.Id);
            return new OrderDetailResponse
            {
                OrderId = order.Id,
                TableId = order.TableId,
                TotalAmount = order.TotalAmount,
                Items = items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }
        public async Task CheckoutAsync(int tableId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var activeOrder = await _unitOfWork.OrdersRepository.GetActiveOrderByTableIdAsync(tableId);

                if (activeOrder == null)
                    throw new Exception("Bu masada aktiv sifariş tapılmadı!");
                // 1. Sifarişi "ÖDƏNİB" ET (Status = 2)
                await _unitOfWork.OrdersRepository.UpdateOrderStatusAsync(activeOrder.Id, 2);
                // 2. MASANI "BOŞ" ET (Bax bura önəmlidir!)
                await _unitOfWork.TablesRepository.UpdateTableStatusAsync(tableId, 0);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw; // Xətanı yuxarı (Controller-ə) ötür ki, istifadəçi bilsin
            }
        }
        public async Task<Order> GetActiveOrderIdByTableIdAsync(int tableId)
        {
            var activeOrder = await _unitOfWork.OrdersRepository.GetActiveOrderByTableIdAsync(tableId);
            return activeOrder;
        }
    }
}
