using Entities;
using MAIT.Interfaces;

namespace Dto;

public record ApvDto
(
    Guid Id,
    Guid SerieId,
    DateOnly Fecha,
    string? Numero,
    string? CodigoPostal,
    bool IsActive,
    bool IsApproved,
    IEnumerable<LocalizacionDto> Localiaciones
) : IGetDto<Apv>
{
    public static bool FromEntity<TDto>(Apv entity, out TDto dto) where TDto : IGetDto<Apv>
    {
        dto = (TDto)FromEntity(entity);
        return true;
    }

    public static IGetDto<Apv> FromEntity(Apv entity)
    {
        return new ApvDto
        (
            Id: entity.Id,
            SerieId: entity.SerieId,
            Fecha: entity.Fecha,
            Numero: entity.Numero,
            CodigoPostal: entity.CodigoPostal,
            IsActive: entity.IsEnabled,
            IsApproved: entity.IsApproved ?? false,
            Localiaciones: entity.Localiaciones.Select(x => LocalizacionDto.FromEntity(x, out LocalizacionDto dto) ? dto : throw new($"{x?.Nombre} no se pudo convertir a LocalizacionDto"))
        );
    }
}
