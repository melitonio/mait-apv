using Entities;
using MAIT.Interfaces;

namespace Dto;

public record ApvDto
(
    Guid Id,
    DateOnly Fecha,
    string? Numero,
    string? CodigoPostal,
    string? ZonaPostal,
    bool IsActive,
    bool IsApproved,
    string? NombreCompleto,
    string? EmergenciaNombre,
    string? EmergenciaTelefono,
    string? EmergenciaRelacion,
    double Latitud,
    double Longitud,
    string? FotoVivienda,

    ZonaPostalDto? ZonaPostalDto,

    string? Telefono,
    string? Email,

    string? Calle,
    string? NumeroCalle,
    string? Bloque,
    string? Portal,
    string? Escalera,
    string? Piso,
    string? Puerta,
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
        var localizaciones = entity.Localiaciones.Select(x => LocalizacionDto.FromEntity(x, out LocalizacionDto dto)
            ? dto : throw new($"{x?.Nombre} no se pudo convertir a LocalizacionDto"));

        var activeLocalization = localizaciones.FirstOrDefault(x => x.Activa);

        return new ApvDto
        (
            Id: entity.Id,
            Fecha: entity.Fecha,
            Numero: entity.Numero,
            CodigoPostal: entity.CodigoPostal,
            IsActive: entity.IsEnabled,
            IsApproved: entity.IsApproved ?? false,
            NombreCompleto: entity.Nombre,
            Telefono: entity.Telefono,
            Email: entity.Email,
            EmergenciaNombre: entity.EmergenciaNombre,
            EmergenciaTelefono: entity.EmergenciaTelefono,
            EmergenciaRelacion: entity.EmergenciaRelacion,
            Latitud: entity.Latitud,
            Longitud: entity.Longitud,
            FotoVivienda: entity.FotoVivienda,
            Localiaciones: localizaciones,

            ZonaPostalDto: entity.ZonaPostal,
            ZonaPostal: entity.ZonaPostal?.Zona,

            Calle: activeLocalization?.Calle ?? string.Empty,
            NumeroCalle: activeLocalization?.Numero ?? string.Empty,
            Bloque: activeLocalization?.Bloque ?? string.Empty,
            Portal: activeLocalization?.Portal ?? string.Empty,
            Escalera: activeLocalization?.Escalera ?? string.Empty,
            Piso: activeLocalization?.Piso ?? string.Empty,
            Puerta: activeLocalization?.Puerta ?? string.Empty 
        );
    }
}
