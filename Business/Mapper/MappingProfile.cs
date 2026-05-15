using AutoMapper;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product -> ProductResponse
            CreateMap<Product, ProductResponse>();
            CreateMap<OrderWithTableNumber, OrderSummaryResponse>();
            CreateMap<CreateExpenseRequest, Expense>();
            // Gələcəkdə digər mapping-lər də bura gələcək:
            // CreateMap<Category, CategoryDto>();
        }
    }
}
