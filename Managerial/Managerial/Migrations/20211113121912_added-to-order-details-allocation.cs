using Microsoft.EntityFrameworkCore.Migrations;

namespace Managerial.Migrations
{
    public partial class addedtoorderdetailsallocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allocation_OrderDetails_OrderDetailId",
                table: "Allocation");

            migrationBuilder.DropIndex(
                name: "IX_Allocation_OrderDetailId",
                table: "Allocation");

            migrationBuilder.AddColumn<int>(
                name: "AllocationId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Allocation_OrderDetails_Id",
                table: "Allocation",
                column: "Id",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allocation_OrderDetails_Id",
                table: "Allocation");

            migrationBuilder.DropColumn(
                name: "AllocationId",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_OrderDetailId",
                table: "Allocation",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allocation_OrderDetails_OrderDetailId",
                table: "Allocation",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
