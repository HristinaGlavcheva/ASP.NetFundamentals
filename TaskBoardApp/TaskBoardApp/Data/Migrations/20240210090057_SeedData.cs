using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Descripion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257", 0, "0f5838ac-ec23-4556-a8d0-ebf7ffb17648", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEIQ1K5Yx7hUj8UypNcWmK64ZUBU/Z8DFaRl0Umkdi5SI5Hs8qxvnNYQ5b1fzdg1jHQ==", null, false, "0873e786-eab7-4277-a2f3-8b5b2a05bfdc", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Descripion", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 25, 11, 0, 56, 624, DateTimeKind.Local).AddTicks(6766), "Implement better styling for all public pages", "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257", "Improve CSS styles" },
                    { 2, 1, new DateTime(2024, 2, 5, 11, 0, 56, 624, DateTimeKind.Local).AddTicks(6831), "Create Android client app for the TaskBoard RESTful API", "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257", "Android Client App" },
                    { 3, 2, new DateTime(2024, 2, 9, 11, 0, 56, 624, DateTimeKind.Local).AddTicks(6844), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257", "Desktop Client App" },
                    { 4, 3, new DateTime(2024, 2, 9, 11, 0, 56, 624, DateTimeKind.Local).AddTicks(6852), "Implement [Create Task] page for adding new tasks", "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5b70ef5-5016-4e6d-a4ad-1c89bd8ac257");
        }
    }
}
