using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class ImagesRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "Images",
                newName: "Path");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images",
                column: "GroupId",
                principalTable: "ImagePacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Images",
                newName: "FileExtension");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Images",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<byte[]>(
                name: "Bytes",
                table: "Images",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImagePacks_GroupId",
                table: "Images",
                column: "GroupId",
                principalTable: "ImagePacks",
                principalColumn: "Id");
        }
    }
}
