using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMealOrdering.Server.Data.Migrations
{
    public partial class userPasswordAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                schema: "public",
                table: "users",
                type: "character varying",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                schema: "public",
                table: "users");
        }
    }
}
