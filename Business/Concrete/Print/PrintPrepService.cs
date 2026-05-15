using Business.Abstract.Print;
using Business.DTOs;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.Print
{
    public class PrintPrepService : IPrintPrepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPrintService _printService;

        public PrintPrepService(IUnitOfWork unitOfWork, IPrintService printService)
        {
            _unitOfWork = unitOfWork;
            _printService = printService;
        }

        public async Task PrepareAndPrintKitchenTicketAsync(CreateOrderRequest request)
        {
            // 1. Masanın aktiv sifarişini tapırıq
            var activeOrder = await _unitOfWork.OrdersRepository.GetActiveOrderByTableIdAsync(request.TableId);

            if (activeOrder == null) return; // Və ya xəta fırlada bilərsən

            // 2. Masanın adını tapırıq
            var table = await _unitOfWork.TablesRepository.GetTableNumberByTableIdAsync(request.TableId);

            // 3. Məhsul adlarını hazırlayırıq
            var printItems = new List<KitchenItem>();
            foreach (var item in request.Items)
            {
                var product = await _unitOfWork.ProductsRepository.GetProductByIdAsync(item.ProductId);
                printItems.Add(new KitchenItem
                {
                    Name = product?.Name ?? $"ID: {item.ProductId}",
                    Quantity = item.Quantity,
                    Note = item.Note
                });
            }

            // 4. Modelimizi qururuq
            var ticket = new KitchenTicketModel
            {
                TableName = table,
                OrderNumber = activeOrder.Id,
                OrderTime = DateTime.Now,
                Items = printItems
            };

            // 5. Birbaşa buradan çap servisinə ötürürük
            _printService.PrintKitchenTicket(ticket);
        }
    }
}
