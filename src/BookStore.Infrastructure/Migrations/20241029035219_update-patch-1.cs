using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    public partial class updatepatch1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK__users__role_id__4BAC3F29",
            table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK__book_cate__book___6D0D32F4",
                table: "book_categories");

            migrationBuilder.DropForeignKey(
                name: "FK__book_cate__categ__6E01572D",
                table: "book_categories");

            migrationBuilder.DropForeignKey(
                name: "FK__cart_deta__book___628FA481",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__cart_deta__cart___619B8048",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__carts__user_id__5EBF139D",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK__order_det__book___66603565",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__order_det__order__656C112C",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__customer__5AEE82B9",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__employee__5BE2A6F2",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__book___76969D2E",
                table: "stock_import_details");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__stock__75A278F5",
                table: "stock_import_details");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__emplo__72C60C4A",
                table: "stock_imports");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__suppl__71D1E811",
                table: "stock_imports");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK__users__3213E83FC7AEE2AD",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__supplier__3213E83FC1B8A092",
                table: "suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__stock_im__3213E83F2D75BE0C",
                table: "stock_imports");

            migrationBuilder.DropIndex(
                name: "IX_stock_imports_employee_id",
                table: "stock_imports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__stock_im__3213E83FB3C74451",
                table: "stock_import_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83F29EBD87B",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83FF633A05F",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_customer_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__order_de__3213E83F8211B8D1",
                table: "order_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__categori__3213E83FE423EA6F",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK__carts__3213E83F78ED2A57",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__cart_det__3213E83F6409673C",
                table: "cart_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__books__3213E83FB46B716A",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK__book_cat__1459F47AF53B586B",
                table: "book_categories");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "stock_imports");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_employee_id",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__users__3213E83F945A9790",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__supplier__3213E83F033D9E5E",
                table: "suppliers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__stock_im__3213E83FE6AFC046",
                table: "stock_imports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__stock_im__3213E83F39FB200D",
                table: "stock_import_details",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83F593785AB",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83FBB119652",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__order_de__3213E83F013C6BDF",
                table: "order_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__categori__3213E83F7AC34821",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__carts__3213E83F79F76684",
                table: "carts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__cart_det__3213E83F9AEF95B3",
                table: "cart_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__books__3213E83F4C89C83B",
                table: "books",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__book_cat__1459F47AB0EFE0DB",
                table: "book_categories",
                columns: new[] { "book_id", "category_id" });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "id", "author", "detail_desc", "discount", "factory", "image", "name", "price", "quantity", "short_desc", "sold" },
                values: new object[,]
                {
                    { 1L, "Frank Herbert", "A science fiction novel set in a distant future", 0.10m, "Chilton Books", "dune.jpg", "Dune", 19.99m, 100L, "Classic Sci-Fi", 10L },
                    { 2L, "J.R.R. Tolkien", "A fantasy adventure of Bilbo Baggins", 0.05m, "George Allen & Unwin", "hobbit.jpg", "The Hobbit", 14.99m, 200L, "Fantasy Adventure", 20L },
                    { 3L, "Dan Brown", "A thriller involving cryptic codes and a murder mystery", 0.15m, "Doubleday", "davinci.jpg", "The Da Vinci Code", 12.99m, 150L, "Mystery Thriller", 15L },
                    { 4L, "Michelle Obama", "A memoir by Michelle Obama", 0.20m, "Crown Publishing", "becoming.jpg", "Becoming", 17.99m, 120L, "Inspiring Memoir", 8L },
                    { 5L, "James Clear", "A book about building good habits", 0.05m, "Penguin Random House", "atomic_habits.jpg", "Atomic Habits", 11.99m, 300L, "Self-Improvement Guide", 40L },
                    { 6L, "J.K. Rowling", "A young wizard's adventure", 0.08m, "Bloomsbury", "hp_sorcerer.jpg", "Harry Potter and the Sorcerer's Stone", 16.99m, 250L, "Fantasy Magic", 60L },
                    { 7L, "George Orwell", "A dystopian novel set in a totalitarian society", 0.12m, "Secker & Warburg", "1984.jpg", "1984", 13.99m, 100L, "Classic Dystopia", 50L },
                    { 8L, "Tara Westover", "A memoir about the power of education", 0.15m, "Random House", "educated.jpg", "Educated", 18.99m, 80L, "Memoir", 7L },
                    { 9L, "J.D. Salinger", "A classic novel about teenage rebellion", 0.10m, "Little, Brown", "catcher_rye.jpg", "The Catcher in the Rye", 10.99m, 170L, "Classic Novel", 30L },
                    { 10L, "Yuval Noah Harari", "A brief history of humankind", 0.10m, "Harvill Secker", "sapiens.jpg", "Sapiens", 22.99m, 90L, "History of Humanity", 25L }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1L, "Books about futuristic science and technology", "Science Fiction" },
                    { 2L, "Books that contain magical elements", "Fantasy" },
                    { 3L, "Books that involve solving a crime or uncovering secrets", "Mystery" },
                    { 4L, "Books about people's life stories", "Biography" },
                    { 5L, "Books aimed at personal development", "Self-Help" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1L, "Administrator role", "ADMIN" },
                    { 2L, "User role", "USER" }
                });

            migrationBuilder.InsertData(
                table: "book_categories",
                columns: new[] { "book_id", "category_id" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L },
                    { 3L, 3L },
                    { 4L, 4L },
                    { 5L, 5L },
                    { 6L, 2L },
                    { 7L, 1L },
                    { 8L, 4L },
                    { 9L, 3L },
                    { 10L, 5L }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "address", "avatar", "email", "full_name", "password", "phone", "role_id" },
                values: new object[] { 1L, "123 Admin St", "admin.png", "admin@bookstore.com", "Admin User", "$2a$12$vDQrTy3RH5flY7Zm5lrXDemGEDIozW48kCf9vsAk.LdKmCGa7MO.S", "123456789", 1L });

            migrationBuilder.AddForeignKey(
                name: "FK__book_cate__book___6383C8BA",
                table: "book_categories",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__book_cate__categ__6477ECF3",
                table: "book_categories",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__cart_deta__book___59063A47",
                table: "cart_detail",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__cart_deta__cart___5812160E",
                table: "cart_detail",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__carts__user_id__5535A963",
                table: "carts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__order_det__book___5CD6CB2B",
                table: "order_detail",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__order_det__order__5BE2A6F2",
                table: "order_detail",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__user_id__52593CB8",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__book___6C190EBB",
                table: "stock_import_details",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__stock__6B24EA82",
                table: "stock_import_details",
                column: "stock_import_id",
                principalTable: "stock_imports",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__suppl__68487DD7",
                table: "stock_imports",
                column: "supplier_id",
                principalTable: "suppliers",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__book_cate__book___6383C8BA",
                table: "book_categories");

            migrationBuilder.DropForeignKey(
                name: "FK__book_cate__categ__6477ECF3",
                table: "book_categories");

            migrationBuilder.DropForeignKey(
                name: "FK__cart_deta__book___59063A47",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__cart_deta__cart___5812160E",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__carts__user_id__5535A963",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK__order_det__book___5CD6CB2B",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__order_det__order__5BE2A6F2",
                table: "order_detail");

            migrationBuilder.DropForeignKey(
                name: "FK__orders__user_id__52593CB8",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__book___6C190EBB",
                table: "stock_import_details");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__stock__6B24EA82",
                table: "stock_import_details");

            migrationBuilder.DropForeignKey(
                name: "FK__stock_imp__suppl__68487DD7",
                table: "stock_imports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__users__3213E83F945A9790",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__supplier__3213E83F033D9E5E",
                table: "suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK__stock_im__3213E83FE6AFC046",
                table: "stock_imports");

            migrationBuilder.DropPrimaryKey(
                name: "PK__stock_im__3213E83F39FB200D",
                table: "stock_import_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK__roles__3213E83F593785AB",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__orders__3213E83FBB119652",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK__order_de__3213E83F013C6BDF",
                table: "order_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__categori__3213E83F7AC34821",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK__carts__3213E83F79F76684",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__cart_det__3213E83F9AEF95B3",
                table: "cart_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK__books__3213E83F4C89C83B",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK__book_cat__1459F47AB0EFE0DB",
                table: "book_categories");

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 3L, 3L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 4L, 4L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 5L, 5L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 6L, 2L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 7L, 1L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 8L, 4L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 9L, 3L });

            migrationBuilder.DeleteData(
                table: "book_categories",
                keyColumns: new[] { "book_id", "category_id" },
                keyValues: new object[] { 10L, 5L });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "orders",
                newName: "employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "orders",
                newName: "IX_orders_employee_id");

            migrationBuilder.AddColumn<long>(
                name: "employee_id",
                table: "stock_imports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "customer_id",
                table: "orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK__users__3213E83FC7AEE2AD",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__supplier__3213E83FC1B8A092",
                table: "suppliers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__stock_im__3213E83F2D75BE0C",
                table: "stock_imports",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__stock_im__3213E83FB3C74451",
                table: "stock_import_details",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__roles__3213E83F29EBD87B",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__orders__3213E83FF633A05F",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__order_de__3213E83F8211B8D1",
                table: "order_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__categori__3213E83FE423EA6F",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__carts__3213E83F78ED2A57",
                table: "carts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__cart_det__3213E83F6409673C",
                table: "cart_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__books__3213E83FB46B716A",
                table: "books",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__book_cat__1459F47AF53B586B",
                table: "book_categories",
                columns: new[] { "book_id", "category_id" });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    points = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__3213E83FA96F4118", x => x.id);
                    table.ForeignKey(
                        name: "FK__customers__user___5070F446",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    position = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__employee__3213E83F96F2727D", x => x.id);
                    table.ForeignKey(
                        name: "FK__employees__user___5441852A",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stock_imports_employee_id",
                table: "stock_imports",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "UQ__customer__B9BE370E06240819",
                table: "customers",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__employee__B9BE370EE42C34DB",
                table: "employees",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK__book_cate__book___6D0D32F4",
                table: "book_categories",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__book_cate__categ__6E01572D",
                table: "book_categories",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__cart_deta__book___628FA481",
                table: "cart_detail",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__cart_deta__cart___619B8048",
                table: "cart_detail",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__carts__user_id__5EBF139D",
                table: "carts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__order_det__book___66603565",
                table: "order_detail",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__order_det__order__656C112C",
                table: "order_detail",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__customer__5AEE82B9",
                table: "orders",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__orders__employee__5BE2A6F2",
                table: "orders",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__book___76969D2E",
                table: "stock_import_details",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__stock__75A278F5",
                table: "stock_import_details",
                column: "stock_import_id",
                principalTable: "stock_imports",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__emplo__72C60C4A",
                table: "stock_imports",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__stock_imp__suppl__71D1E811",
                table: "stock_imports",
                column: "supplier_id",
                principalTable: "suppliers",
                principalColumn: "id");
        }
    }
}
