using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModelCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagePacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileExtension = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ImagePacks_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ImagePacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileExtension = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thumbnails_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_GroupId",
                table: "Images",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_ImageId",
                table: "Thumbnails",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thumbnails");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ImagePacks");
        }
    }
}
