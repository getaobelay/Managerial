using Microsoft.EntityFrameworkCore.Migrations;

namespace Managerial.Migrations
{
    public partial class updated_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_Products_ProductId",
                table: "product_categories");

            migrationBuilder.DropTable(
                name: "CategoryProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_product_categories_ProductId",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "categories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "product_categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "product_categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_product_categories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "product_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_product_categories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "product_categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "product_categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "product_categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryProductCategory",
                columns: table => new
                {
                    CategroiesId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProductCategory", x => new { x.CategroiesId, x.ProductCategoriesId });
                    table.ForeignKey(
                        name: "FK_CategoryProductCategory_categories_CategroiesId",
                        column: x => x.CategroiesId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProductCategory_product_categories_ProductCategoriesId",
                        column: x => x.ProductCategoriesId,
                        principalTable: "product_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_ProductId",
                table: "product_categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProductCategory_ProductCategoriesId",
                table: "CategoryProductCategory",
                column: "ProductCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_Products_ProductId",
                table: "product_categories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
