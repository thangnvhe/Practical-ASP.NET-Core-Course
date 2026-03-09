using System.Threading;
using System.Threading.Tasks;

namespace Lesson01_API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ISupplierRepository Suppliers { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Dispose();
    }
}
