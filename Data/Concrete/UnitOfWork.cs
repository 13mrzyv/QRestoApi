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
        private IExpenseRepository _expenseRepository;

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
        public IExpenseRepository ExpensesRepository => _expenseRepository ??= new ExpenseRepository(_connection, _transaction);    

        public void BeginTransaction() => _transaction = _connection.BeginTransaction();

        //public void Commit()
        //{
        //    _transaction?.Commit();
        //    Dispose();
        //}

        //public void Rollback()
        //{
        //    _transaction?.Rollback();
        //    Dispose();
        //}

        //public void Dispose()
        //{
        //    _transaction?.Dispose();
        //    _connection?.Dispose();
        //}

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            finally
            {
                // Tranzaksiyanı təmizləyirik ki, eyni UnitOfWork daxilində 
                // növbəti sorğular (məs: çap üçün adların çəkilməsi) tranzaksiyasız davam edə bilsin.
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            // Bu metod avtomatik olaraq HTTP Request-in sonunda (Controller-dən cavab qayıdanda) 
            // .NET tərəfindən çağırılacaq. Bağlantı məhz burada tamamilə qapanacaq.
            _transaction?.Dispose();
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection?.Dispose();
        }
    }
}
