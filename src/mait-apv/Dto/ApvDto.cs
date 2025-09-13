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
    string? Nombre,
    string? Apellidos,
    string? EmergenciaNombre,
    string? EmergenciaTelefono,
    string? EmergenciaRelacion,
    double Latitud,
    double Longitud,
    string? FotoVivienda,
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
            Nombre: entity.Nombre,
            Apellidos: entity.Apellidos,
            EmergenciaNombre: entity.EmergenciaNombre,
            EmergenciaTelefono: entity.EmergenciaTelefono,
            EmergenciaRelacion: entity.EmergenciaRelacion,
            Latitud: entity.Latitud,
            Longitud: entity.Longitud,
            FotoVivienda: entity.FotoVivienda,
            Localiaciones: entity.Localiaciones.Select(x => LocalizacionDto.FromEntity(x, out LocalizacionDto dto) ? dto : throw new($"{x?.Nombre} no se pudo convertir a LocalizacionDto"))
        );
    }
}
