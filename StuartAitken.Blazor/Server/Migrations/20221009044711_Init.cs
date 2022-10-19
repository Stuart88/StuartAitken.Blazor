using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuartAitken.Blazor.Server.Migrations
{
    public partial class Init : Migration
    {
        #region Protected Methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PortfolioProjectImage");

            migrationBuilder.DropTable(name: "PortfolioProjectTech");

            migrationBuilder.DropTable(name: "PortfolioProjectType");

            migrationBuilder.DropTable(name: "PortfolioProject");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioProject",
                columns: table =>
                    new
                    {
                        ID = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Name = table.Column<string>(type: "TEXT", nullable: false),
                        ProjectDate = table.Column<string>(type: "TEXT", nullable: false),
                        ProjectDurationWeeks = table.Column<int>(type: "INTEGER", nullable: false),
                        Type = table.Column<string>(type: "TEXT", nullable: false),
                        Description = table.Column<string>(type: "TEXT", nullable: true),
                        Tech = table.Column<string>(type: "TEXT", nullable: false),
                        Urls = table.Column<string>(type: "TEXT", nullable: true),
                        Views = table.Column<int>(type: "INTEGER", nullable: true),
                        Status = table.Column<int>(type: "INTEGER", nullable: true),
                        CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                        ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProject", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "PortfolioProjectTech",
                columns: table =>
                    new
                    {
                        ID = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Name = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProjectTech", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "PortfolioProjectType",
                columns: table =>
                    new
                    {
                        ID = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Name = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProjectType", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "PortfolioProjectImage",
                columns: table =>
                    new
                    {
                        ID = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                        ImageByteArray = table.Column<byte[]>(type: "BLOB", nullable: false),
                        PrimaryImage = table.Column<int>(type: "INTEGER", nullable: true),
                        Status = table.Column<int>(type: "INTEGER", nullable: false),
                        CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                        ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioProjectImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PortfolioProjectImage_PortfolioProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "PortfolioProject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioProjectImage_ProjectId",
                table: "PortfolioProjectImage",
                column: "ProjectId"
            );
        }

        #endregion Protected Methods
    }
}
