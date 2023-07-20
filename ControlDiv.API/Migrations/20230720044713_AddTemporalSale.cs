using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlDiv.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTemporalSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temporals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoucherCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Montreceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temporals_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Temporals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temporals_AccountId",
                table: "Temporals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporals_UserId",
                table: "Temporals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporals_VoucherCode",
                table: "Temporals",
                column: "VoucherCode",
                unique: true,
                filter: "[VoucherCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temporals");
        }
    }
}
