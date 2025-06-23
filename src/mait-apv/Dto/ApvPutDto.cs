using System.ComponentModel.DataAnnotations;
using MAIT.Interfaces;
using Entities;

namespace Dto;

public record ApvPutDto(
    Guid Id,
    Guid ContactoId,
    Guid SerieId,
    DateTime Fecha,
    string? Numero,
    string? CodigoPostal
) : IPutDto<Apv>
{
    public void UpdateEntity(in Apv entity, string usuario)
    {
        throw new("Operación no permitida");
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Numero))
        {
            yield return new("El número es obligatorio.", [nameof(Numero)]);
        }

        if (Fecha == default)
        {
            yield return new("La fecha es obligatoria.", [nameof(Fecha)]);
        }
    }
}
