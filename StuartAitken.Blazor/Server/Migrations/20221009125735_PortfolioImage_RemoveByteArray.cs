using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuartAitken.Blazor.Server.Migrations
{
    public partial class PortfolioImage_RemoveByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ImageByteArray", table: "PortfolioProjectImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByteArray",
                table: "PortfolioProjectImage",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]
            );
        }
    }
}
