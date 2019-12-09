using Microsoft.EntityFrameworkCore.Migrations;

namespace Rich.Order.Infrastructure.Migrations
{
    public partial class addpage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "dbo",
                table: "PagePermission",
                newName: "RoleIds");

            migrationBuilder.AddColumn<bool>(
                name: "AlwaysShow",
                schema: "dbo",
                table: "ManagerPage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "dbo",
                table: "ManagerPage",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoCache",
                schema: "dbo",
                table: "ManagerPage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Redirect",
                schema: "dbo",
                table: "ManagerPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "ManagerPage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlwaysShow",
                schema: "dbo",
                table: "ManagerPage");

            migrationBuilder.DropColumn(
                name: "Icon",
                schema: "dbo",
                table: "ManagerPage");

            migrationBuilder.DropColumn(
                name: "NoCache",
                schema: "dbo",
                table: "ManagerPage");

            migrationBuilder.DropColumn(
                name: "Redirect",
                schema: "dbo",
                table: "ManagerPage");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "ManagerPage");

            migrationBuilder.RenameColumn(
                name: "RoleIds",
                schema: "dbo",
                table: "PagePermission",
                newName: "RoleId");
        }
    }
}
