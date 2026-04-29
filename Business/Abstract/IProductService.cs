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
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductResponse>> GetAllProductsForCustomerAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<bool> InsertProductAsync(InsertProductRequest insertProductRequest);
        Task<bool> UpdateProductByIdAsync(int id, InsertProductRequest insertProductRequest);
        Task<bool> DeleteProductByIdAsync(int id);
        Task<List<ProductResponse>> GetFullMenuAsync();
    }
}
