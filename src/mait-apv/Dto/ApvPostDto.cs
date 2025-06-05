using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record class ApvPostDto : IPostDto<ApvPostDto, Apv>
{
    public Guid ContactoId { get; init; }
    public Guid SeriedID { get; set; }           // Serie documental para correspondencia
    public DateTime Fecha { get; init; }
    public string? CodigoPostal { get; init; }  // Zona postal para apartados

    public bool ToEntity(string usuario, out Apv entity, Guid? id = null)
    {
        entity = new Apv
        {
            Id = id ?? Guid.NewGuid(),
            Fecha = DateOnly.FromDateTime(Fecha),
            CodigoPostal = CodigoPostal,
            ContactoId = ContactoId,
            SerieId = SeriedID,
        };
        return true;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Fecha == default) yield return new("La fecha es obligatoria.", [nameof(Fecha)]);
    }
}
