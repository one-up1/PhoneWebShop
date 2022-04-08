using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneWebShop.Business.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    VatPercentage = table.Column<double>(type: "float", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPhone",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsPerOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPhone", x => new { x.OrdersId, x.ProductsPerOrderId });
                    table.ForeignKey(
                        name: "FK_OrderPhone_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPhone_Phones_ProductsPerOrderId",
                        column: x => x.ProductsPerOrderId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPhone_ProductsPerOrderId",
                table: "OrderPhone",
                column: "ProductsPerOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPhone");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
