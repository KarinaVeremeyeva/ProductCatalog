using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Entities;

namespace ProductCatalog.DAL
{
    public static class DataInitializer
    {
        public static void SeedData(this ModelBuilder builder)
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
                    CategoryId = category3,
                    Description = "This is a description for product 5",
                    Price = 45000,
                    GeneralNote = "Sale",
                    SpecialNote = "Warm"
                });

            builder.Entity<Product>()
                .Property(x => x.Price).HasPrecision(18, 2);
        }

        public static void SeedUsers(this ModelBuilder builder)
        {
            var adminId = "2288a9b4-35ee-4c13-b863-36184e701e0a";
            var firstUserId = "5eb5d348-b220-4148-8f9b-570de6262aa8";
            var secondUserId = "3758bb9b-3ad6-457c-9ff5-c88f338c7d48";

            var adminUser = new IdentityUser
            {
                Id = adminId,
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
            };
            var firstUser = new IdentityUser
            {
                Id = firstUserId,
                UserName = "user1@gmail.com",
                NormalizedUserName = "user1@gmail.com".ToUpper(),
                Email = "user1@gmail.com",
                NormalizedEmail = "user1@gmail.com".ToUpper()
            };
            var secondUser = new IdentityUser
            {
                Id = secondUserId,
                UserName = "user2@gmail.com",
                NormalizedUserName = "user2@gmail.com".ToUpper(),
                Email = "user2@gmail.com",
                NormalizedEmail = "user2@gmail.com".ToUpper()
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin-123");
            firstUser.PasswordHash = passwordHasher.HashPassword(firstUser, "123456qw");
            secondUser.PasswordHash = passwordHasher.HashPassword(secondUser, "33N2vm");

            builder.Entity<IdentityUser>().HasData(adminUser, firstUser, secondUser);
            
            var adminRoleId = "e1110812-5f76-4889-93a7-b4c677c2d8dd";
            var userRoleId = "beea0094-0cde-4f04-812b-98c02f4f8e27";
            var advancedUserRoleId = "056096da-3246-40a7-af6f-726f5f4a74ee";

            var adminRole = "Admin";
            var userRole = "User";
            var advancedUserRole = "AdvancedUser";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = adminRole, NormalizedName = adminRole.ToUpper() },
                new IdentityRole { Id = userRoleId, Name = userRole, NormalizedName = userRole.ToUpper() },
                new IdentityRole { Id = advancedUserRoleId, Name = advancedUserRole, NormalizedName = advancedUserRole.ToUpper() }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminId },
                new IdentityUserRole<string> { RoleId = userRoleId, UserId = firstUserId },
                new IdentityUserRole<string> { RoleId = advancedUserRoleId, UserId = secondUserId }
            );
        }
    }
}
