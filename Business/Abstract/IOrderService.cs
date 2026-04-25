using Business.DTOs;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(CreateOrderRequest request);
        Task<OrderDetailResponse> GetOrderDetailsAsync(int id);
        Task CheckoutAsync(CheckoutRequest request);
    }
}
