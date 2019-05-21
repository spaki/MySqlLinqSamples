using Microsoft.EntityFrameworkCore.Migrations;

namespace LinqSamples.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );");

            migrationBuilder.CreateTable(
                name: "Avengers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Codename = table.Column<string>(nullable: false),
                    MainSkill = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avengers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avengers");
        }
    }
}
