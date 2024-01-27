using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace ImageHub.Api.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class ThumbnailsSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Thumbnails");

            migrationBuilder.AddColumn<int>(
                name: "ProcessingStatus",
                table: "Thumbnails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingStatus",
                table: "Thumbnails");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Thumbnails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Thumbnails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "Thumbnails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
