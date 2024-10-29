using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    public partial class updatepatch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "password",
                value: "$2a$12$xkaiydj8kcvoyFJdTL.5ou.rJYRERXSymin4pTLikQ6dA3F8uJPxK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "password",
                value: "$2a$12$vDQrTy3RH5flY7Zm5lrXDemGEDIozW48kCf9vsAk.LdKmCGa7MO.S");
        }
    }
}
