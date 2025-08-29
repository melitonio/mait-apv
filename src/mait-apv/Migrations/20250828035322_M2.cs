using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maitapv.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Apv",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergenciaNombre",
                table: "Apv",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergenciaRelacion",
                table: "Apv",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergenciaTelefono",
                table: "Apv",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitud",
                table: "Apv",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitud",
                table: "Apv",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Apv",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "EmergenciaNombre",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "EmergenciaRelacion",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "EmergenciaTelefono",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Apv");

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
    }
}
