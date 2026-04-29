using AutoMapper;
using Business.Abstract;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
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
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductsRepository.GetAllProductsAsync();
        }
        public async Task<IEnumerable<ProductResponse>> GetAllProductsForCustomerAsync()
        {   
            var products = await _unitOfWork.ProductsRepository.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _unitOfWork.ProductsRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<List<ProductResponse>> GetFullMenuAsync()
        {
            var allProducts = await _unitOfWork.ProductsRepository.GetAllProductsAsync();
            var allCategories = await _unitOfWork.CategoriesRepository.GetAllCategoriesAsync();

            // 2. AutoMapper ilə əsas hissələri map edirik
            var response = _mapper.Map<List<ProductResponse>>(allProducts);

            // 3. İndi isə CategoryName hissəsini "yamayırıq"
            foreach (var item in response)
            {
                // Hər məhsulun öz CategoryId-sinə uyğun adı tapırıq
                item.CategoryName = allCategories.FirstOrDefault(c => c.Id == item.CategoryId)?.Name;
            }

            return response;
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
