using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFit.Migrations
{
    /// <inheritdoc />
    public partial class DBChnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedBack",
                table: "Admissions",
                newName: "PlanFeedBack");

            migrationBuilder.AddColumn<string>(
                name: "PlanRating",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoachRating",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CoachFeedback",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanRating",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CoachRating",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CoachFeedback",
                table: "Admissions");

            migrationBuilder.RenameColumn(
                name: "PlanFeedBack",
                table: "Admissions",
                newName: "FeedBack");
        }
    }
}
