using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P7CreateRestApi.Migrations
{
    public partial class fixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RuleNames",
                table: "RuleNames");

            migrationBuilder.RenameTable(
                name: "RuleNames",
                newName: "Rules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rules",
                table: "Rules",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rules",
                table: "Rules");

            migrationBuilder.RenameTable(
                name: "Rules",
                newName: "RuleNames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RuleNames",
                table: "RuleNames",
                column: "Id");
        }
    }
}
