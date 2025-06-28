using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maitapv.Migrations
{
    /// <inheritdoc />
    public partial class M0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    FechaBaja = table.Column<DateOnly>(type: "date", nullable: true),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    SerieId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ArchivedBy = table.Column<string>(type: "text", nullable: true),
                    UnarchivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UnarchivedBy = table.Column<string>(type: "text", nullable: true),
                    IsSuspended = table.Column<bool>(type: "boolean", nullable: false),
                    SuspendedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SuspendedBy = table.Column<string>(type: "text", nullable: true),
                    UnsuspendedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UnsuspendedBy = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RejectedBy = table.Column<string>(type: "text", nullable: true),
                    RejectedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Calle = table.Column<string>(type: "text", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    Bloque = table.Column<string>(type: "text", nullable: false),
                    Portal = table.Column<string>(type: "text", nullable: false),
                    Escalera = table.Column<string>(type: "text", nullable: false),
                    Piso = table.Column<string>(type: "text", nullable: false),
                    CodigoPostal = table.Column<string>(type: "text", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    ApvId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizacion_Apv_ApvId",
                        column: x => x.ApvId,
                        principalTable: "Apv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localizacion_ApvId_Nombre",
                table: "Localizacion",
                columns: new[] { "ApvId", "Nombre" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacion");

            migrationBuilder.DropTable(
                name: "Apv");
        }
    }
}
