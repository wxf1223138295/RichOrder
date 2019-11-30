using Microsoft.EntityFrameworkCore.Migrations;

namespace Rich.Order.Infrastructure.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShowName",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ShowName",
                table: "AspNetRoles");
        }
    }
}
