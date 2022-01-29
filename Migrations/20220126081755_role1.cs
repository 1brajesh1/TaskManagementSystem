using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class role1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_TaskId",
                table: "MyUsers",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_Tasks_TaskId",
                table: "MyUsers",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_Tasks_TaskId",
                table: "MyUsers");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_TaskId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "MyUsers");
        }
    }
}
