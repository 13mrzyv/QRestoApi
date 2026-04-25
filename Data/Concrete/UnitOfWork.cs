using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private ITableRepository _tableRepository;
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private IReportsRepository _reportsRepository;

        public UnitOfWork(DbContext dbContext)
        {
            _connection = dbContext.CreateConnection();
            _connection.Open();
        }

        public ICategoryRepository CategoriesRepository => _categoryRepository ??= new CategoryRepository(_connection, _transaction);
        public IProductRepository ProductsRepository => _productRepository ??= new ProductRepository(_connection, _transaction);

        public ITableRepository TablesRepository => _tableRepository ??= new TableRepository(_connection, _transaction);

        public IOrderRepository OrdersRepository => _orderRepository ??= new OrderRepository(_connection, _transaction);

        public IOrderItemRepository OrderItemsRepository => _orderItemRepository ??= new OrderItemRepository(_connection, _transaction);
        public IReportsRepository ReportsRepository => _reportsRepository ??= new ReportsRepository(_connection, _transaction);

        public void BeginTransaction() => _transaction = _connection.BeginTransaction();

        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
