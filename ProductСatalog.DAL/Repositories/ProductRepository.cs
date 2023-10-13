using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Entities;

namespace ProductCatalog.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return products;
        }

        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }
    }
}
