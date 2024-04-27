using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("ALTER TABLE [VolunteerDB].[dbo].[VolunteerJobOffers] ADD StatusCopy NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET StatusCopy = Status;");
            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET Status = ' ';");


            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "VolunteerJobOffers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET Status = CASE WHEN StatusCopy = 'unapprove' THEN 0 WHEN StatusCopy = 'confirm' THEN 1 WHEN StatusCopy = 'rejected' THEN 2 ELSE Status END;");
            migrationBuilder.Sql("ALTER TABLE [VolunteerDB].[dbo].[VolunteerJobOffers] DROP COLUMN StatusCopy;");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [VolunteerDB].[dbo].[VolunteerJobOffers] ADD StatusCopy INT;");
            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET StatusCopy = Status;");
            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET Status = ' ';");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "VolunteerJobOffers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql("UPDATE [VolunteerDB].[dbo].[VolunteerJobOffers] SET Status = CASE WHEN StatusCopy = 0 THEN 'unapprove' WHEN StatusCopy = 1 THEN 'confirm' WHEN StatusCopy = 2 THEN 'rejected' ELSE Status END;");
            migrationBuilder.Sql("ALTER TABLE [VolunteerDB].[dbo].[VolunteerJobOffers] DROP COLUMN StatusCopy;");
        }
    }
}
