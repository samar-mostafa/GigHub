using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHub.Data.Migrations
{
    public partial class AddIsCancledToGig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancled",
                table: "Gigs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancled",
                table: "Gigs");
        }
    }
}
