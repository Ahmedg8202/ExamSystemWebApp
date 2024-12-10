using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesandUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8233bcca-4c82-4b06-9262-f478bf082794");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "156df417-4b34-474f-8ccb-4249abbd3a0d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7962c3d5-8746-4bbd-aec9-662c84fb892d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ae83a610-95b7-4bff-8d12-4fc53defa7c7");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "ExamResults",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_StudentId",
                table: "ExamResults",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_AspNetUsers_StudentId",
                table: "ExamResults",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_AspNetUsers_StudentId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_ExamResults_StudentId",
                table: "ExamResults");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8233bcca-4c82-4b06-9262-f478bf082794", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "156df417-4b34-474f-8ccb-4249abbd3a0d", 0, "59b13c64-4302-4c3a-8673-9ca4ea189117", "admin1@examsystem.com", false, false, null, null, null, null, null, false, "cd629b55-5efa-4b00-b806-5f195e2d739b", false, "admin1" },
                    { "7962c3d5-8746-4bbd-aec9-662c84fb892d", 0, "31d3bc2c-b9f0-426d-b372-08cb87bce51d", "admin2@examsystem.com", false, false, null, null, null, null, null, false, "18ccfb76-335a-467d-bdff-42524982a4f8", false, "admin2" },
                    { "ae83a610-95b7-4bff-8d12-4fc53defa7c7", 0, "7f83f50e-eeb6-4af7-a499-92725e02d4a3", "admin3@examsystem.com", false, false, null, null, null, null, null, false, "7cdf63b2-56e4-4243-9a2f-d168394e60cd", false, "admin3" }
                });
        }
    }
}
