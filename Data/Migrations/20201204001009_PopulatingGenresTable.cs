using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHub.Data.Migrations
{
    public partial class PopulatingGenresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Genres(Name) values ('Jazz')");
            migrationBuilder.Sql("insert into Genres(Name) values ('Blues')");
            migrationBuilder.Sql("insert into Genres(Name) values ('Rock')");
            migrationBuilder.Sql("insert into Genres(Name) values ('Country')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Genres where Id in (1,2,3,4)");
        }
    }
}
