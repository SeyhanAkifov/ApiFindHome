using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiFindHome.Migrations
{
    public partial class AddSomePropertiesInPropertyEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Properties");
        }
    }
}
