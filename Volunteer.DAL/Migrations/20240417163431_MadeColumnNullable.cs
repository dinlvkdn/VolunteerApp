using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MadeColumnNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Feedbacks_FeedbackId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_FeedbackId",
                table: "Members");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Members_FeedbackId",
                table: "Members",
                column: "FeedbackId",
                unique: true,
                filter: "[FeedbackId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Feedbacks_FeedbackId",
                table: "Members",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Feedbacks_FeedbackId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_FeedbackId",
                table: "Members");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_FeedbackId",
                table: "Members",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Feedbacks_FeedbackId",
                table: "Members",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
