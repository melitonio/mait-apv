using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record class ApvPostDto (
    Guid Id,
    Guid ContactoId,
    Guid SerieId,
    DateTime Fecha,
    string? Numero,
    string? CodigoPostal
) : IPostDto<Apv>
{
    public bool ToEntity(string usuario, out Apv entity, Guid? id = null)
    {
        entity = new Apv
        {
            Id = id ?? Guid.NewGuid(),
            Fecha = DateOnly.FromDateTime(Fecha),
            CodigoPostal = CodigoPostal,
            ContactoId = ContactoId,
            SerieId = SerieId,
        };
        return true;
    }

    public Apv ToEntity(string usuario, Guid? id = null)
    {
        ToEntity(usuario, out var entity, id);
        entity.Numero = Numero ?? Apv.DefaultNumero;
        return entity;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Fecha == default) yield return new("El código postal es obligatorio.", [nameof(CodigoPostal)]);
    }
}
