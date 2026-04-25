using Business.Abstract;
using Business.DTOs.Requests;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductsRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _unitOfWork.ProductsRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<bool> InsertProductAsync(InsertProductRequest insertProductRequest)
        {
            var product = new Product
            {
                CategoryId = insertProductRequest.CategoryId,
                Name = insertProductRequest.Name,
                Description = insertProductRequest.Description,
                Price = insertProductRequest.Price,
                ImageUrl = insertProductRequest.ImageUrl,
                IsAvailable = insertProductRequest.IsAvailable
            };

            await _unitOfWork.ProductsRepository.InsertProductAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductByIdAsync(int id, InsertProductRequest insertProductRequest)
        {
            
            var existingProduct = await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.CategoryId = insertProductRequest.CategoryId;
            existingProduct.Name = insertProductRequest.Name;
            existingProduct.Description = insertProductRequest.Description;
            existingProduct.Price = insertProductRequest.Price;
            existingProduct.ImageUrl = insertProductRequest.ImageUrl;
            existingProduct.IsAvailable = insertProductRequest.IsAvailable;
            await _unitOfWork.ProductsRepository.UpdateProductAsync(existingProduct);
            return true;
        }
        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var existingProduct = await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }
            await _unitOfWork.ProductsRepository.DeleteProductAsync(id);
            return true;

        }
    }
}
