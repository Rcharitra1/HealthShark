using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthShark.DataAccess.Migrations
{
    public partial class addUserAssignmentToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAssignmentVMs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    TrainerId = table.Column<string>(nullable: true),
                    DieticianId = table.Column<string>(nullable: true),
                    DietId = table.Column<int>(nullable: true),
                    WorkoutId = table.Column<int>(nullable: true),
                    BodyTypeId = table.Column<int>(nullable: true),
                    UserPlanId = table.Column<int>(nullable: true),
                    Paid = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignmentVMs", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_BodyTypes_BodyTypeId",
                        column: x => x.BodyTypeId,
                        principalTable: "BodyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_AspNetUsers_DieticianId",
                        column: x => x.DieticianId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_UserPlans_UserPlanId",
                        column: x => x.UserPlanId,
                        principalTable: "UserPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssignmentVMs_WorkOutTypes_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "WorkOutTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_BodyTypeId",
                table: "UserAssignmentVMs",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_DietId",
                table: "UserAssignmentVMs",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_DieticianId",
                table: "UserAssignmentVMs",
                column: "DieticianId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_TrainerId",
                table: "UserAssignmentVMs",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_UserId",
                table: "UserAssignmentVMs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_UserPlanId",
                table: "UserAssignmentVMs",
                column: "UserPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignmentVMs_WorkoutId",
                table: "UserAssignmentVMs",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssignmentVMs");
        }
    }
}
