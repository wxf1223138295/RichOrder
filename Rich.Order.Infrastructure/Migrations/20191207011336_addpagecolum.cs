using Microsoft.EntityFrameworkCore.Migrations;

namespace Rich.Order.Infrastructure.Migrations
{
    public partial class addpagecolum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                schema: "dbo",
                table: "PagePermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentPageId",
                schema: "dbo",
                table: "PagePermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PageUrlName",
                schema: "dbo",
                table: "ManagerPage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelId",
                schema: "dbo",
                table: "PagePermission");

            migrationBuilder.DropColumn(
                name: "ParentPageId",
                schema: "dbo",
                table: "PagePermission");

            migrationBuilder.DropColumn(
                name: "PageUrlName",
                schema: "dbo",
                table: "ManagerPage");
        }
    }
}
