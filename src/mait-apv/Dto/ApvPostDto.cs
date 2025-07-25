using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record ApvPostDto(
    string CodigoPostal,
    DateTime Fecha,
    Guid SerieId
) : IPostDto<Apv>
{
    public bool ToEntity(string usuario, out Apv entity, Guid? id = null)
    {
        entity = new Apv
        {
            Id = id ?? Guid.NewGuid(),
            CodigoPostal = CodigoPostal,
            Fecha = DateOnly.FromDateTime(Fecha),
            SerieId = SerieId,
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
