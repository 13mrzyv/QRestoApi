using Dapper;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }



        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var sql = "SELECT * FROM Products WHERE IsDeleted = 0";
            return await _connection.QueryAsync<Product>(sql, transaction: _transaction);
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id AND IsDeleted = 0";
            return await _connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id }, transaction: _transaction);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var sql = "SELECT * FROM Products WHERE CategoryId = @Category AND IsDeleted = 0";
            return await _connection.QueryAsync<Product>(sql, new { Category = categoryId }, transaction: _transaction);
        }
        public async Task<int> InsertProductAsync(Product product)
        {
            var sql = @"INSERT INTO Products (CategoryId, Name, Description, Price, ImageUrl, IsAvailable) 
                        VALUES (@CategoryId, @Name, @Description, @Price, @ImageUrl, @IsAvailable)";
            return await _connection.ExecuteAsync(sql, product, transaction: _transaction);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var sql = @"UPDATE Products SET CategoryId=@CategoryId, Name=@Name, Description=@Description, 
                        Price=@Price, ImageUrl=@ImageUrl, IsAvailable=@IsAvailable WHERE Id=@Id";
            return await _connection.ExecuteAsync(sql, product, transaction: _transaction);
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var sql = "UPDATE Products SET IsDeleted = 1 WHERE Id = @Id";
            return await _connection.ExecuteAsync(sql, new { Id = id }, transaction: _transaction);
        }
        public async Task<decimal?> GetProductPriceByIdAsync(int id)
        {
            var sql = "SELECT Price FROM Products WHERE Id = @Id AND IsDeleted = 0";
            return await _connection.QueryFirstOrDefaultAsync<decimal?>(sql, new { Id = id }, transaction: _transaction);
        }
        public async Task<bool> UpdateProductStatusAsync(int id, bool isAvailable)
        {
            var sql = "UPDATE Products SET IsAvailable = @IsAvailable WHERE Id = @Id AND IsDeleted = 0";
            var affectedRows = await _connection.ExecuteAsync(sql, new { Id = id, IsAvailable = isAvailable }, transaction: _transaction);
            return affectedRows > 0;
        }




        //public async Task<string> GetProductNameByIdAsync(int id)
        //{
        //    var sql = "SELECT Name FROM Products WHERE Id = @Id AND IsDeleted = 0";
        //    return await _connection.QueryFirstOrDefaultAsync<string>(sql, new { Id = id }, transaction: _transaction);
        //}
    }
}
