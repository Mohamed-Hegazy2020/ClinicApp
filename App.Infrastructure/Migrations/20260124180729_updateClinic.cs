using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile",
                schema: "Basic",
                table: "Clinic",
                newName: "WorkTimes");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Basic",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                schema: "Basic",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile1",
                schema: "Basic",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile2",
                schema: "Basic",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Basic",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                schema: "Basic",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Mobile1",
                schema: "Basic",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Mobile2",
                schema: "Basic",
                table: "Clinic");

            migrationBuilder.RenameColumn(
                name: "WorkTimes",
                schema: "Basic",
                table: "Clinic",
                newName: "Mobile");
        }
    }
}
