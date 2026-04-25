using Business.DTOs.Requests;
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
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<bool> InsertProductAsync(InsertProductRequest insertProductRequest);
        Task<bool> UpdateProductByIdAsync(int id, InsertProductRequest insertProductRequest);
        Task<bool> DeleteProductByIdAsync(int id);
    }
}
