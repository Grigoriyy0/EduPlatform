using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduNEXT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_TimeZone_Remove_Colums_Email_Surname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Students",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "Students",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
