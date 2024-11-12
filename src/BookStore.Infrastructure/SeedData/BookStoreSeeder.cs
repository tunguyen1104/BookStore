using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.SeedData
{
    public static class BookStoreSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed admin
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("admin123", BCrypt.Net.BCrypt.GenerateSalt(12));

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "ADMIN", Description = "Administrator role" },
                new Role { Id = 2, Name = "USER", Description = "User role" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin User",
                    Email = "admin@bookstore.com",
                    Password = adminPassword,
                    RoleId = 1,
                    Address = "123 Admin St",
                    Phone = "123456789",
                    Avatar = "admin.png"
                }
            );
        }
    }
}
