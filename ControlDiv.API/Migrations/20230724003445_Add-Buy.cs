using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlDiv.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBuy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");
        }
    }
}
