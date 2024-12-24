using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctors_Web_Forum.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateAddDoctorProfileToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DoctorProfiles",
                newName: "ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_ProfileId",
                table: "DoctorProfiles",
                column: "ProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Profiles_ProfileId",
                table: "DoctorProfiles",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Profiles_ProfileId",
                table: "DoctorProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_ProfileId",
                table: "DoctorProfiles");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "DoctorProfiles",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
