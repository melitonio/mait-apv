using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record class ApvPostDto : IPostDto<ApvPostDto, Apv>
{
    public Guid ContactoId { get; init; }
    public Guid SeriedID { get; set; }              // Serie documental para correspondencia
    public DateTime Fecha { get; init; }
    public string? Codigo { get; init; }
    public string? CodigoPostal { get; init; }  // Zona postal para apartados

    public bool ToEntity(string usuario, out Apv entity, Guid? id = null)
    {
        entity = new Apv
        {
            Fecha = DateOnly.FromDateTime(Fecha),
            Codigo = Codigo,
            ContactoId = ContactoId,
            SeriedID = SeriedID,
            CodigoPostal = CodigoPostal,
        };
        return true;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Fecha == default)
        {
            yield return new ValidationResult("La fecha es obligatoria.", [nameof(Fecha)]);
        }
    }
}
