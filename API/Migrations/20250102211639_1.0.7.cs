using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _107 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TimeOffs");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "TimeOffs");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "Shifts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Shifts",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Shifts",
                newName: "EndDate");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TimeOffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TimeOffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TimeOffs");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Shifts",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Shifts",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Shifts",
                newName: "TaskDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TimeOffs",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TimeOffs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "TimeOffs",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }
    }
}
