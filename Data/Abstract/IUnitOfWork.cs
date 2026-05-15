using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoriesRepository { get; }
        IProductRepository ProductsRepository { get; }
        ITableRepository TablesRepository { get; }
        IOrderRepository OrdersRepository { get; }
        IOrderItemRepository OrderItemsRepository { get; }  
        IReportsRepository ReportsRepository { get; }
        IExpenseRepository ExpensesRepository { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
