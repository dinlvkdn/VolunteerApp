using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateJobOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobOfferTitle",
                table: "JobOffer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobOfferTitle",
                table: "JobOffer");
        }
    }
}
