using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctors_Web_Forum.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfileInfomationToUserRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Profiles");
        }
    }
}
