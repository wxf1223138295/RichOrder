using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rich.Order.Infrastructure.Migrations
{
    public partial class addrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageUrl",
                schema: "dbo",
                table: "PagePermission",
                newName: "Creator");

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                schema: "dbo",
                table: "PagePermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ManagerPage",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    PageUrl = table.Column<string>(nullable: true),
                    PageShowName = table.Column<string>(nullable: true),
                    PageDescribe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerPage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerPage",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "PageId",
                schema: "dbo",
                table: "PagePermission");

            migrationBuilder.RenameColumn(
                name: "Creator",
                schema: "dbo",
                table: "PagePermission",
                newName: "PageUrl");
        }
    }
}
