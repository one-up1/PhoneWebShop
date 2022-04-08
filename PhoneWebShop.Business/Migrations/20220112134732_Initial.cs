using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneWebShop.Business.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATPrice = table.Column<double>(type: "float", nullable: false),
                    VAT = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Apple" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Samsung" });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "BrandId", "Description", "Stock", "Type", "VAT", "VATPrice" },
                values: new object[] { 1, 1, "An oldie but goldie.", 1, "Iphone 6", 0.0, 150.0 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "BrandId", "Description", "Stock", "Type", "VAT", "VATPrice" },
                values: new object[] { 2, 1, "Better than Iphone 6 but worse than the Samsung Galaxy S7.", 69, "Iphone 7", 0.0, 299.0 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "BrandId", "Description", "Stock", "Type", "VAT", "VATPrice" },
                values: new object[] { 3, 2, "Better than Iphone 7 but worse than the Samsung Galaxy S8.", 32, "Galaxy S7", 0.0, 369.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_BrandId",
                table: "Phones",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
