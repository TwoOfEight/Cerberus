using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _105 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_AspNetUsers_UserId",
                table: "Breaks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks");

            migrationBuilder.RenameTable(
                name: "Breaks",
                newName: "TimeOffs");

            migrationBuilder.RenameIndex(
                name: "IX_Breaks_UserId",
                table: "TimeOffs",
                newName: "IX_TimeOffs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeOffs",
                table: "TimeOffs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeOffs_AspNetUsers_UserId",
                table: "TimeOffs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeOffs_AspNetUsers_UserId",
                table: "TimeOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeOffs",
                table: "TimeOffs");

            migrationBuilder.RenameTable(
                name: "TimeOffs",
                newName: "Breaks");

            migrationBuilder.RenameIndex(
                name: "IX_TimeOffs_UserId",
                table: "Breaks",
                newName: "IX_Breaks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_AspNetUsers_UserId",
                table: "Breaks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
