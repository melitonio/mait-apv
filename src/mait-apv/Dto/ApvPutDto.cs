using System.ComponentModel.DataAnnotations;
using MAIT.Interfaces;
using Entities;

namespace Dto;

public record ApvPutDto(
    Guid Id,
    string? CodigoPostal,
    string? Nombre,
    string? Apellidos,
    string? EmergenciaNombre,
    string? EmergenciaTelefono,
    string? EmergenciaRelacion,
    double Latitud,
    double Longitud,
    string? FotoVivienda
) : IPutDto<Apv>
{
    public void UpdateEntity(in Apv entity, string usuario)
    {
        entity.CodigoPostal = CodigoPostal ?? entity.CodigoPostal;
        entity.Nombre = Nombre ?? entity.Nombre;
        entity.Apellidos = Apellidos ?? entity.Apellidos;
        entity.EmergenciaNombre = EmergenciaNombre ?? entity.EmergenciaNombre;
        entity.EmergenciaTelefono = EmergenciaTelefono ?? entity.EmergenciaTelefono;
        entity.EmergenciaRelacion = EmergenciaRelacion ?? entity.EmergenciaRelacion;
        entity.Latitud = Latitud;
        entity.Longitud = Longitud;
        entity.FotoVivienda = FotoVivienda ?? entity.FotoVivienda;
        entity.UpdatedBy = usuario;
        entity.UpdatedAt = DateTimeOffset.Now;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(CodigoPostal))
        {
            yield return new("El código postal es obligatorio.", [nameof(CodigoPostal)]);
        }
        if (Latitud < -90 || Latitud > 90)
        {
            yield return new("La latitud debe estar entre -90 y 90.", [nameof(Latitud)]);
        }
        if (Longitud < -180 || Longitud > 180)
        {
            yield return new("La longitud debe estar entre -180 y 180.", [nameof(Longitud)]);
        }
    }
}
