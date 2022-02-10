using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class profilepicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "MyUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "MyUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "MyUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "MyUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
