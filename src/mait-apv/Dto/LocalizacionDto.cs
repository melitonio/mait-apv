using Entities;
using MAIT.Interfaces;

namespace Dto;

public record GeoJSON(string Type, PropertiesDto Properties, GeometryDto Geometry);
public record PropertiesDto(string Nombre, string Type);
public record GeometryDto(string Type, IEnumerable<double> Coordinatesstring);

public record LocalizacionDto
(
    Guid Id,
    string Calle,
    string Numero,
    string Bloque,
    string Portal,
    string Escalera,
    string Piso,
    string CodigoPostal, // de la zona postal asociada a la dirección
    string Descripcion,
    bool Activa,
    double Latitud,
    double Longitud,
    string Nombre, // nombre de la dirección, por ejemplo: "Oficina Central", "Almacén Principal", etc.
    string Type,
    Guid ApvId = default // Identificador del apartado al que pertenece esta localización
) : IGetDto<LocalizacionDto, Localizacion>
{
    public GeoJSON GeoJSON => new("Feature", new(Nombre, Type), new("Point", [Longitud, Latitud]));

    public static bool FromEntity(Localizacion entity, out LocalizacionDto dto)
    {
        dto = new LocalizacionDto
        (
            Id: entity.Id,
            Calle: entity.Calle,
            Numero: entity.Numero,
            Bloque: entity.Bloque,
            Portal: entity.Portal,
            Escalera: entity.Escalera,
            Piso: entity.Piso,
            CodigoPostal: entity.CodigoPostal,
            Descripcion: entity.Descripcion ?? string.Empty,
            Activa: entity.Activa,
            Latitud: entity.Latitud,
            Longitud: entity.Longitud,
            Nombre: entity.Nombre,
            Type: entity.Tipo,
            ApvId: entity.ApvId
        );
        return true;
    }
}