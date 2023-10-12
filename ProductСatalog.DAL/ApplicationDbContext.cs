using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Entities;

namespace ProductCatalog.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var category1 = Guid.Parse("027ab076-461c-4f42-a09b-e052b818aa57");
            var category2 = Guid.Parse("a71fe02a-a524-49ee-9f26-d156c0b62d6c");
            var category3 = Guid.Parse("d4490f24-d752-458f-b324-a604e79b2f2e");

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = category1,
                    Name = "Category 1",
                },
                new Category
                {
                    Id = category2,
                    Name = "Category 2",
                },
                new Category
                {
                    Id = category3,
                    Name = "Category 3",
                });

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("86ca08b6-3b59-4046-9c3f-260f0631ceb8"),
                    Name = "Product 1",
                    CategoryId = category1,
                    Description = "This is a description for product 1",
                    Price = 10000,
                    GeneralNote = "Sale",
                    SpecialNote = "Oversalted"
                },
                new Product
                {
                    Id = Guid.Parse("75ac7087-0031-4895-8a7c-60e87b96a5d7"),
                    Name = "Product 2",
                    CategoryId = category1,
                    Description = "This is a description for product 2",
                    Price = 20000,
                    GeneralNote = "Sale",
                    SpecialNote = "Oversalted"
                },
                new Product
                {
                    Id = Guid.Parse("7cda37ad-0d0a-4685-8104-3f435f5eeaf6"),
                    Name = "Product 3",
                    CategoryId = category2,
                    Description = "This is a description for product 3",
                    Price = 30000,
                    GeneralNote = "Sale",
                    SpecialNote = "Tasty"
                },
                new Product
                {
                    Id = Guid.Parse("15fbac63-871c-4eb5-bcaa-900179d7d8e4"),
                    Name = "Product 4",
                    CategoryId = category2,
                    Description = "This is a description for product 4",
                    Price = 15000,
                    GeneralNote = "Sale",
                    SpecialNote = "Cold"
                },
                new Product
                {
                    Id = Guid.Parse("b06cc374-2161-474b-bd67-469ab1e757bf"),
                    Name = "Product 5",
                    CategoryId= category3,
                    Description = "This is a description for product 5",
                    Price = 45000,
                    GeneralNote = "Sale",
                    SpecialNote = "Warm"
                });

            builder.Entity<Product>()
                .Property(x => x.Price).HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }
    }
}
