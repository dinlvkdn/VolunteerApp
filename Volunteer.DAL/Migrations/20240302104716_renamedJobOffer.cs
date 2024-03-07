using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class renamedJobOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobOfferTitle",
                table: "JobOffer",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "JobOfferStreet",
                table: "JobOffer",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "JobOfferCountry",
                table: "JobOffer",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "JobOfferCity",
                table: "JobOffer",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "JobOffer",
                newName: "JobOfferTitle");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "JobOffer",
                newName: "JobOfferStreet");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "JobOffer",
                newName: "JobOfferCountry");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "JobOffer",
                newName: "JobOfferCity");
        }
    }
}
