using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VolunteerId",
                table: "Resumes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //custom query
            migrationBuilder.Sql("UPDATE Resumes SET Resumes.VolunteerId = Volunteers.Id FROM Resumes INNER JOIN Volunteers ON Volunteers.ResumeId = Resumes.Id");
            //migrationBuilder.Sql("UPDATE Resumes SET VolunteerId = Volunteers.Id FROM Resumes INNER JOIN Volunteers ON Volunteers.Id = Resumes.VolunteerId");

            migrationBuilder.DropForeignKey(
               name: "FK_Volunteers_Resumes_ResumeId",
               table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_ResumeId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Volunteers");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_VolunteerId",
                table: "Resumes",
                column: "VolunteerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Volunteers_VolunteerId",
                table: "Resumes",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Volunteers_VolunteerId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_VolunteerId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Resumes");

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeId",
                table: "Volunteers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_ResumeId",
                table: "Volunteers",
                column: "ResumeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Resumes_ResumeId",
                table: "Volunteers",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
