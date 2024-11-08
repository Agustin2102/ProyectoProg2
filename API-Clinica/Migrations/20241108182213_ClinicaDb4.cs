using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class ClinicaDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Specialty_patient_id",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_specialty_id",
                table: "Appointment",
                column: "specialty_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Specialty_specialty_id",
                table: "Appointment",
                column: "specialty_id",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Specialty_specialty_id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_specialty_id",
                table: "Appointment");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Specialty_patient_id",
                table: "Appointment",
                column: "patient_id",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
