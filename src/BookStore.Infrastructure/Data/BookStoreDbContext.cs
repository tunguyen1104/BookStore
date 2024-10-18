using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStore.Infrastructure.Data
{
    public partial class BookStoreDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartDetail> CartDetails { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<StockImport> StockImports { get; set; }

        public virtual DbSet<StockImportDetail> StockImportDetails { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("BookStoreConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__books__3213E83FB46B716A");

                entity.ToTable("books");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("author");
                entity.Property(e => e.DetailDesc)
                    .IsUnicode(false)
                    .HasColumnName("detail_desc");
                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("discount");
                entity.Property(e => e.Factory)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("factory");
                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("short_desc");
                entity.Property(e => e.Sold)
                    .HasDefaultValue(0L)
                    .HasColumnName("sold");

                entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookCategory",
                        r => r.HasOne<Category>().WithMany()
                            .HasForeignKey("CategoryId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__book_cate__categ__6E01572D"),
                        l => l.HasOne<Book>().WithMany()
                            .HasForeignKey("BookId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__book_cate__book___6D0D32F4"),
                        j =>
                        {
                            j.HasKey("BookId", "CategoryId").HasName("PK__book_cat__1459F47AF53B586B");
                            j.ToTable("book_categories");
                            j.IndexerProperty<long>("BookId").HasColumnName("book_id");
                            j.IndexerProperty<long>("CategoryId").HasColumnName("category_id");
                        });
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__carts__3213E83F78ED2A57");

                entity.ToTable("carts");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Sum).HasColumnName("sum");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__carts__user_id__5EBF139D");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__cart_det__3213E83F6409673C");

                entity.ToTable("cart_detail");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book).WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cart_deta__book___628FA481");

                entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cart_deta__cart___619B8048");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FE423EA6F");

                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__customer__3213E83FA96F4118");

                entity.ToTable("customers");

                entity.HasIndex(e => e.UserId, "UQ__customer__B9BE370E06240819").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Points)
                    .HasDefaultValue(0L)
                    .HasColumnName("points");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__customers__user___5070F446");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F96F2727D");

                entity.ToTable("employees");

                entity.HasIndex(e => e.UserId, "UQ__employee__B9BE370EE42C34DB").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");
                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("position");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__employees__user___5441852A");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FF633A05F");

                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
                entity.Property(e => e.OrderDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("order_date");
                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__customer__5AEE82B9");

                entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__employee__5BE2A6F2");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__order_de__3213E83F8211B8D1");

                entity.ToTable("order_detail");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_det__book___66603565");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_det__order__656C112C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F29EBD87B");

                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<StockImport>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__stock_im__3213E83F2D75BE0C");

                entity.ToTable("stock_imports");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
                entity.Property(e => e.ImportDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("import_date");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.TotalCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_cost");

                entity.HasOne(d => d.Employee).WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__stock_imp__emplo__72C60C4A");

                entity.HasOne(d => d.Supplier).WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__stock_imp__suppl__71D1E811");
            });

            modelBuilder.Entity<StockImportDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__stock_im__3213E83FB3C74451");

                entity.ToTable("stock_import_details");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.StockImportId).HasColumnName("stock_import_id");

                entity.HasOne(d => d.Book).WithMany(p => p.StockImportDetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__stock_imp__book___76969D2E");

                entity.HasOne(d => d.StockImport).WithMany(p => p.StockImportDetails)
                    .HasForeignKey(d => d.StockImportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__stock_imp__stock__75A278F5");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83FC1B8A092");

                entity.ToTable("suppliers");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");
                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contact_email");
                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contact_phone");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__users__3213E83FC7AEE2AD");

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");
                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("avatar");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("full_name");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");
                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__users__role_id__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
