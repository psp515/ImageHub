using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class ImagesPacksNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_GroupId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "PackId",
                table: "Images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PackId",
                table: "Images",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImagePacks_PackId",
                table: "Images",
                column: "PackId",
                principalTable: "ImagePacks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImagePacks_PackId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PackId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Images_GroupId",
                table: "Images",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images",
                column: "GroupId",
                principalTable: "ImagePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
