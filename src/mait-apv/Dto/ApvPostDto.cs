using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record ApvPostDto(
    string CodigoPostal,
    DateTime Fecha,
    string? Nombre,
    string? Apellidos,
    string? Telefono,
    string? Email,
    string? EmergenciaNombre,
    string? EmergenciaTelefono,
    string? EmergenciaRelacion,
    double Latitud,
    double Longitud,
    string? FotoVivienda,

    string? Calle,
    string? NumeroCalle,
    string? Bloque,
    string? Portal,
    string? Escalera,
    string? Piso,
    string? Puerta
) : IPostDto<Apv>
{
    public bool ToEntity(string usuario, out Apv entity, Guid? id = null)
    {
        entity = new Apv
        {
            Id = id ?? Guid.NewGuid(),
            CodigoPostal = CodigoPostal,
            Fecha = DateOnly.FromDateTime(Fecha),
            EmergenciaNombre = EmergenciaNombre,
            Telefono = Telefono,
            Email = Email,
            EmergenciaTelefono = EmergenciaTelefono,
            EmergenciaRelacion = EmergenciaRelacion,
            Latitud = Latitud,
            Longitud = Longitud,
            Nombre = (Nombre + " " + Apellidos).Trim(),
            FotoVivienda = FotoVivienda,
            CreatedBy = usuario,
            Localiaciones =
            [
                new Localizacion
                {
                    Calle = Calle ?? string.Empty,
                    Numero = NumeroCalle ?? string.Empty,
                    Bloque = Bloque ?? string.Empty,
                    Portal = Portal ?? string.Empty,
                    Escalera = Escalera ?? string.Empty,
                    Piso = Piso ?? string.Empty,
                    Puerta = Puerta ?? string.Empty,
                    CodigoPostal = CodigoPostal,
                    Activa = true,
                    Latitud = Latitud,
                    Longitud = Longitud,
                    Nombre = "Domicilio Principal",
                    Tipo = "Principal",
                    CreatedBy = usuario,
                }
            ]
        };
        return true;
    }

    public Apv ToEntity(string usuario, Guid? id = null)
    {
        ToEntity(usuario, out var entity, id);
        return entity;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CodigoPostal == default) yield return new("El código postal es obligatorio.", [nameof(CodigoPostal)]);
        if (Fecha == default) yield return new("Indique la fecha de Apv.", [nameof(Fecha)]);
    }
}
