using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthShark.DataAccess.Migrations
{
    public partial class updarteUserAssignmentVM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserAssignmentVMs",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserAssignmentVMs",
                newName: "id");
        }
    }
}
