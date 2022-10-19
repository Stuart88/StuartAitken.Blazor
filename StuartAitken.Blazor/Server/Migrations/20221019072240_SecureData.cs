using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuartAitken.Blazor.Server.Migrations
{
    public partial class SecureData : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SecureData");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecureData",
                columns: table =>
                    new
                    {
                        ID = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Name = table.Column<string>(type: "TEXT", nullable: false),
                        Value = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecureData", x => x.ID);
                }
            );
        }

        #endregion Protected Methods
    }
}
