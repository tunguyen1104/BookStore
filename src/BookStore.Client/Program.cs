using BookStore.Application.Services;
using BookStore.Application.Services.Impl;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register IService -> ServiceImpl
            builder.Services.AddScoped(typeof(IService<>), typeof(ServiceImpl<>));
            // Register IRepository -> Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Change the connection string in appsettings.json to your local sql server
            builder.Services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}