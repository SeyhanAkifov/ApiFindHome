using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiFindHome.Migrations
{
    public partial class ChangeTypeOfcolumnInPropertyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_CreatorId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CreatorId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CreatorId",
                table: "Properties",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_CreatorId",
                table: "Properties",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
