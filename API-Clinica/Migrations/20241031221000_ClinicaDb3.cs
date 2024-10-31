using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class ClinicaDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Specialty_SpecialtyId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_SpecialtyId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "medical_history",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patient_id",
                table: "Appointment",
                column: "patient_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_patient_id",
                table: "Appointment",
                column: "patient_id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Specialty_patient_id",
                table: "Appointment",
                column: "patient_id",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_patient_id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Specialty_patient_id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_patient_id",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "medical_history",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SpecialtyId",
                table: "Appointment",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Specialty_SpecialtyId",
                table: "Appointment",
                column: "SpecialtyId",
                principalTable: "Specialty",
                principalColumn: "Id");
        }
    }
}
