using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    public partial class initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    detail_desc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    short_desc = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    sold = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L),
                    image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    author = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    factory = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__books__3213E83FB46B716A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__3213E83FE423EA6F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F29EBD87B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    contact_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    contact_phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__3213E83FC1B8A092", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_categories",
                columns: table => new
                {
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__book_cat__1459F47AF53B586B", x => new { x.book_id, x.category_id });
                    table.ForeignKey(
                        name: "FK__book_cate__book___6D0D32F4",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__book_cate__categ__6E01572D",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    avatar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83FC7AEE2AD", x => x.id);
                    table.ForeignKey(
                        name: "FK__users__role_id__4BAC3F29",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sum = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__carts__3213E83F78ED2A57", x => x.id);
                    table.ForeignKey(
                        name: "FK__carts__user_id__5EBF139D",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

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
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "cart_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cart_id = table.Column<long>(type: "bigint", nullable: false),
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart_det__3213E83F6409673C", x => x.id);
                    table.ForeignKey(
                        name: "FK__cart_deta__book___628FA481",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__cart_deta__cart___619B8048",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__3213E83FF633A05F", x => x.id);
                    table.ForeignKey(
                        name: "FK__orders__customer__5AEE82B9",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__employee__5BE2A6F2",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "stock_imports",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    import_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    total_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock_im__3213E83F2D75BE0C", x => x.id);
                    table.ForeignKey(
                        name: "FK__stock_imp__emplo__72C60C4A",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__stock_imp__suppl__71D1E811",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_de__3213E83F8211B8D1", x => x.id);
                    table.ForeignKey(
                        name: "FK__order_det__book___66603565",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__order_det__order__656C112C",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "stock_import_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stock_import_id = table.Column<long>(type: "bigint", nullable: false),
                    book_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock_im__3213E83FB3C74451", x => x.id);
                    table.ForeignKey(
                        name: "FK__stock_imp__book___76969D2E",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__stock_imp__stock__75A278F5",
                        column: x => x.stock_import_id,
                        principalTable: "stock_imports",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_book_categories_category_id",
                table: "book_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_book_id",
                table: "cart_detail",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_cart_id",
                table: "cart_detail",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_user_id",
                table: "carts",
                column: "user_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_book_id",
                table: "order_detail",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_employee_id",
                table: "orders",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_import_details_book_id",
                table: "stock_import_details",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_import_details_stock_import_id",
                table: "stock_import_details",
                column: "stock_import_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_imports_employee_id",
                table: "stock_imports",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_imports_supplier_id",
                table: "stock_imports",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_categories");

            migrationBuilder.DropTable(
                name: "cart_detail");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "stock_import_details");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "stock_imports");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
