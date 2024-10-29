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
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Science Fiction", Description = "Books about futuristic science and technology" },
                new Category { Id = 2, Name = "Fantasy", Description = "Books that contain magical elements" },
                new Category { Id = 3, Name = "Mystery", Description = "Books that involve solving a crime or uncovering secrets" },
                new Category { Id = 4, Name = "Biography", Description = "Books about people's life stories" },
                new Category { Id = 5, Name = "Self-Help", Description = "Books aimed at personal development" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Dune", DetailDesc = "A science fiction novel set in a distant future", Price = 19.99m, Quantity = 100, ShortDesc = "Classic Sci-Fi", Sold = 10, Image = "dune.jpg", Author = "Frank Herbert", Factory = "Chilton Books", Discount = 0.10m },
                new Book { Id = 2, Name = "The Hobbit", DetailDesc = "A fantasy adventure of Bilbo Baggins", Price = 14.99m, Quantity = 200, ShortDesc = "Fantasy Adventure", Sold = 20, Image = "hobbit.jpg", Author = "J.R.R. Tolkien", Factory = "George Allen & Unwin", Discount = 0.05m },
                new Book { Id = 3, Name = "The Da Vinci Code", DetailDesc = "A thriller involving cryptic codes and a murder mystery", Price = 12.99m, Quantity = 150, ShortDesc = "Mystery Thriller", Sold = 15, Image = "davinci.jpg", Author = "Dan Brown", Factory = "Doubleday", Discount = 0.15m },
                new Book { Id = 4, Name = "Becoming", DetailDesc = "A memoir by Michelle Obama", Price = 17.99m, Quantity = 120, ShortDesc = "Inspiring Memoir", Sold = 8, Image = "becoming.jpg", Author = "Michelle Obama", Factory = "Crown Publishing", Discount = 0.20m },
                new Book { Id = 5, Name = "Atomic Habits", DetailDesc = "A book about building good habits", Price = 11.99m, Quantity = 300, ShortDesc = "Self-Improvement Guide", Sold = 40, Image = "atomic_habits.jpg", Author = "James Clear", Factory = "Penguin Random House", Discount = 0.05m },
                new Book { Id = 6, Name = "Harry Potter and the Sorcerer's Stone", DetailDesc = "A young wizard's adventure", Price = 16.99m, Quantity = 250, ShortDesc = "Fantasy Magic", Sold = 60, Image = "hp_sorcerer.jpg", Author = "J.K. Rowling", Factory = "Bloomsbury", Discount = 0.08m },
                new Book { Id = 7, Name = "1984", DetailDesc = "A dystopian novel set in a totalitarian society", Price = 13.99m, Quantity = 100, ShortDesc = "Classic Dystopia", Sold = 50, Image = "1984.jpg", Author = "George Orwell", Factory = "Secker & Warburg", Discount = 0.12m },
                new Book { Id = 8, Name = "Educated", DetailDesc = "A memoir about the power of education", Price = 18.99m, Quantity = 80, ShortDesc = "Memoir", Sold = 7, Image = "educated.jpg", Author = "Tara Westover", Factory = "Random House", Discount = 0.15m },
                new Book { Id = 9, Name = "The Catcher in the Rye", DetailDesc = "A classic novel about teenage rebellion", Price = 10.99m, Quantity = 170, ShortDesc = "Classic Novel", Sold = 30, Image = "catcher_rye.jpg", Author = "J.D. Salinger", Factory = "Little, Brown", Discount = 0.10m },
                new Book { Id = 10, Name = "Sapiens", DetailDesc = "A brief history of humankind", Price = 22.99m, Quantity = 90, ShortDesc = "History of Humanity", Sold = 25, Image = "sapiens.jpg", Author = "Yuval Noah Harari", Factory = "Harvill Secker", Discount = 0.10m }
            );

            // Map Books to Categories (optional)
            modelBuilder.Entity("BookCategory").HasData(
                new { BookId = 1L, CategoryId = 1L },
                new { BookId = 2L, CategoryId = 2L },
                new { BookId = 3L, CategoryId = 3L },
                new { BookId = 4L, CategoryId = 4L },
                new { BookId = 5L, CategoryId = 5L },
                new { BookId = 6L, CategoryId = 2L },
                new { BookId = 7L, CategoryId = 1L },
                new { BookId = 8L, CategoryId = 4L },
                new { BookId = 9L, CategoryId = 3L },
                new { BookId = 10L, CategoryId = 5L }
            );
        }
    }
}
