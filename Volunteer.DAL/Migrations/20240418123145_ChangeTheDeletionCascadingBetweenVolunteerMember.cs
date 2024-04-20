using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheDeletionCascadingBetweenVolunteerMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Volunteers_VolunteerId",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Volunteers_VolunteerId",
                table: "Members",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Volunteers_VolunteerId",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Volunteers_VolunteerId",
                table: "Members",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
