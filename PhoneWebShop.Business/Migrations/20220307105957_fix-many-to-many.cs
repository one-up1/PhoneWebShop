using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneWebShop.Business.Migrations
{
    public partial class fixmanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPhone");

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Phones_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrder");

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
    }
}
