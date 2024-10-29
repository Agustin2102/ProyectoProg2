using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class ClinicaDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialty_Doctor_DoctorId",
                table: "Specialty");

            migrationBuilder.DropIndex(
                name: "IX_Specialty_DoctorId",
                table: "Specialty");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Specialty");

            migrationBuilder.CreateTable(
                name: "DoctorSpecialty",
                columns: table => new
                {
                    DoctorsId = table.Column<int>(type: "int", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialty", x => new { x.DoctorsId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Doctor_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialty_Specialty_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialty_SpecialtiesId",
                table: "DoctorSpecialty",
                column: "SpecialtiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialty");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Specialty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialty_DoctorId",
                table: "Specialty",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialty_Doctor_DoctorId",
                table: "Specialty",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
