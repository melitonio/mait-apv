using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maitapv.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuspendedAt",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "SuspendedBy",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "UnarchivedAt",
                table: "Apv");

            migrationBuilder.DropColumn(
                name: "UnarchivedBy",
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
                name: "UnsuspendedBy",
                table: "Apv",
                newName: "DisabledBy");

            migrationBuilder.RenameColumn(
                name: "UnsuspendedAt",
                table: "Apv",
                newName: "DisabledAt");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Apv",
                newName: "TagsJson");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "Apv",
                newName: "MetadataJson");

            migrationBuilder.RenameColumn(
                name: "IsSuspended",
                table: "Apv",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Apv",
                newName: "IsDisabled");
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

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "Apv",
                newName: "IsSuspended");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Apv",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "DisabledBy",
                table: "Apv",
                newName: "UnsuspendedBy");

            migrationBuilder.RenameColumn(
                name: "DisabledAt",
                table: "Apv",
                newName: "UnsuspendedAt");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SuspendedAt",
                table: "Apv",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspendedBy",
                table: "Apv",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UnarchivedAt",
                table: "Apv",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnarchivedBy",
                table: "Apv",
                type: "text",
                nullable: true);
        }
    }
}
