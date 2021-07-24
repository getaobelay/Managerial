using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Managerial.Migrations
{
    public partial class updated_product_categories_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_categories_CategoryId",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_product_categories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "product_categories");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "product_categories");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "product_categories",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ProductId",
                table: "product_categories");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "product_categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "product_categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_categories_CategoryId",
                table: "product_categories",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_product_categories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "product_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
