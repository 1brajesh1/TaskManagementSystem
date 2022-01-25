using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_MyRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_Tasks_TasksId",
                table: "MyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVsTasks_MyUsers_ApplicationUserId",
                table: "UserVsTasks");

            migrationBuilder.DropTable(
                name: "MyUserRoles");

            migrationBuilder.DropTable(
                name: "MyRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVsTasks",
                table: "UserVsTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserVsTasks_TaskId",
                table: "UserVsTasks");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_TasksId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "MyUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserVsTasks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserVsTasks",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRoleId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVsTasks",
                table: "UserVsTasks",
                columns: new[] { "TaskId", "ApplicationUserId" });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_MyUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_TaskId",
                table: "MyUsers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_UserRoleId",
                table: "MyUsers",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_Tasks_TaskId",
                table: "MyUsers",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_AspNetRoles_UserRoleId",
                table: "MyUsers",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVsTasks_MyUsers_ApplicationUserId",
                table: "UserVsTasks",
                column: "ApplicationUserId",
                principalTable: "MyUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_Tasks_TaskId",
                table: "MyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_AspNetRoles_UserRoleId",
                table: "MyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVsTasks_MyUsers_ApplicationUserId",
                table: "UserVsTasks");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVsTasks",
                table: "UserVsTasks");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_TaskId",
                table: "MyUsers");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_UserRoleId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "MyUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserVsTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserVsTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "MyUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVsTasks",
                table: "UserVsTasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MyRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MyUserRoles_MyRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyUserRoles_MyUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVsTasks_TaskId",
                table: "UserVsTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_TasksId",
                table: "MyUsers",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "MyRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyUserRoles_RoleId",
                table: "MyUserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_MyRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "MyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_Tasks_TasksId",
                table: "MyUsers",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVsTasks_MyUsers_ApplicationUserId",
                table: "UserVsTasks",
                column: "ApplicationUserId",
                principalTable: "MyUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
