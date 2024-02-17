using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlDiv.API.Migrations
{
    /// <inheritdoc />
    public partial class MOdfcustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersDetail_Customers_ClientId",
                table: "CustomersDetail");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "CustomersDetail",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomersDetail_ClientId",
                table: "CustomersDetail",
                newName: "IX_CustomersDetail_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersDetail_Customers_CustomerId",
                table: "CustomersDetail",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomersDetail_Customers_CustomerId",
                table: "CustomersDetail");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomersDetail",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomersDetail_CustomerId",
                table: "CustomersDetail",
                newName: "IX_CustomersDetail_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersDetail_Customers_ClientId",
                table: "CustomersDetail",
                column: "ClientId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
