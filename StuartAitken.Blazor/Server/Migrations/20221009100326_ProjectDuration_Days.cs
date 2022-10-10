using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuartAitken.Blazor.Server.Migrations
{
    public partial class ProjectDuration_Days : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ProjectDurationWeeks",
                "PortfolioProject",
                "ProjectDurationDays"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ProjectDurationDays",
                "PortfolioProject",
                "ProjectDurationWeeks"
            );
        }
    }
}
