using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthShark.DataAccess.Migrations
{
    public partial class addBodyTypeUpdatedToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "BodyTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BodyTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BodyTypes");

            migrationBuilder.AddColumn<int>(
                name: "BodyId",
                table: "BodyTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyTypes",
                table: "BodyTypes",
                column: "BodyId");
        }
    }
}
