using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHub.Data.Migrations
{
    public partial class editAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Gigs_GigId",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Gigs_GigId",
                table: "Attendances",
                column: "GigId",
                principalTable: "Gigs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Gigs_GigId",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Gigs_GigId",
                table: "Attendances",
                column: "GigId",
                principalTable: "Gigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
