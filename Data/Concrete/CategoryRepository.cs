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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var sql = "SELECT * FROM Categories";
            return await _connection.QueryAsync<Category>(sql, transaction: _transaction);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id }, transaction: _transaction);
        }

        public async Task<int> InsertCategoryAsync(Category category)
        {

            var sql = "INSERT INTO Categories (Name) VALUES (@Name)";
            return await _connection.ExecuteAsync(sql, new { Name = category.Name }, transaction: _transaction);
        }
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            var sql = "UPDATE Categories SET Name = @Name, IsActive = @IsActive WHERE Id = @Id";
            return await _connection.ExecuteAsync(sql, new { Name = category.Name, IsActive = category.IsActive, Id = category.Id}, transaction: _transaction);
        }
    }
}
