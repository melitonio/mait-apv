using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maitapv.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Localizacion",
                newName: "TagsJson");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "Localizacion",
                newName: "MetadataJson");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Apv",
                newName: "TagsJson");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "Apv",
                newName: "MetadataJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagsJson",
                table: "Localizacion",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "MetadataJson",
                table: "Localizacion",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "TagsJson",
                table: "Apv",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "MetadataJson",
                table: "Apv",
                newName: "Metadata");
        }
    }
}
