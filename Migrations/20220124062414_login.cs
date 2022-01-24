using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_TasksId",
                table: "MyUsers",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_Tasks_TasksId",
                table: "MyUsers",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_Tasks_TasksId",
                table: "MyUsers");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_TasksId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "MyUsers");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "MyUsers",
                type: "int",
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
    }
}
