using Lesson01_API.Data;
using Lesson01_API.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson01_API.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public ISupplierRepository Suppliers { get; }
        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }
        public IOrderDetailRepository OrderDetails { get; }

        public UnitOfWork(AppDbContext context,
            ICategoryRepository categoryRepository = null,
            IProductRepository productRepository = null,
            ISupplierRepository supplierRepository = null,
            ICustomerRepository customerRepository = null,
            IOrderRepository orderRepository = null,
            IOrderDetailRepository orderDetailRepository = null)
        {
            _context = context;
            Categories = categoryRepository ?? new CategoryRepository(_context);
            Products = productRepository ?? new ProductRepository(_context);
            Suppliers = supplierRepository ?? new SupplierRepository(_context);
            Customers = customerRepository ?? new CustomerRepository(_context);
            Orders = orderRepository ?? new OrderRepository(_context);
            OrderDetails = orderDetailRepository ?? new OrderDetailRepository(_context);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
