using System.ComponentModel.DataAnnotations;
using MAIT.Interfaces;
using Entities;

namespace Dto;

public record class ApvPutDto : IPutDto<ApvPutDto, Apv>
{
    public Guid Id { get; set; }
    public Guid ContactoId { get; set; }
    public DateTime Fecha { get; init; }
    public string? CodigoPostal { get; set; }
    public string? Numero { get; set; }

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
