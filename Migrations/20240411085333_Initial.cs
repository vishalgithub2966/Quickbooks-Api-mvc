using Microsoft.EntityFrameworkCore.Migrations;

namespace OAuth2_CoreMVC_Sample.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    RealmId = table.Column<string>(maxLength: 50, nullable: false),
                    AccessToken = table.Column<string>(maxLength: 1000, nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.RealmId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
