using System.ComponentModel.DataAnnotations;
using MAIT.Interfaces;
using Entities;

namespace Dto;

public record class ApvPutDto : IPutDto<ApvPutDto, Apv>
{
    public Guid Id { get; set; }
    public Guid ContactoId { get; set; }
    public DateTime Fecha { get; init; }
    public string? Numero { get; set; }

    public void UpdateEntity(in Apv entity, string usuario)
    {
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Numero))
        {
            yield return new ValidationResult("El número es obligatorio.", [nameof(Numero)]);
        }

        if (Fecha == default)
        {
            yield return new ValidationResult("La fecha es obligatoria.", [nameof(Fecha)]);
        }
    }
}
