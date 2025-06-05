using System.ComponentModel.DataAnnotations;

namespace Dto;

public record GeoJON (PropertiesDto Properties, GeometryDto Geometry,string Type);
public record PropertiesDto(string Nombre,string Type);
public record GeometryDto(IEnumerable<double> Coordinates, string Type);

public record LocalizacionDto
(
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
    string Type
) : IValidatableObject
{
    
    public GeoJON ToGeoJON()
    {
        var geometry = new GeometryDto(
            Coordinates: [Longitud, Latitud],
            Type: "Point"
        );

        var properties = new PropertiesDto(Nombre, Type);
        return new GeoJON(properties, geometry, "Feature");
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Calle))
        {
            yield return new ValidationResult("La calle es obligatoria.", [nameof(Calle)]);
        }
        if (string.IsNullOrWhiteSpace(CodigoPostal))
        {
            yield return new ValidationResult("El código postal es obligatorio.", [nameof(CodigoPostal)]);
        }
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            yield return new ValidationResult("El nombre es obligatorio.", [nameof(Nombre)]);
        }
        if (string.IsNullOrWhiteSpace(Type))
        {
            yield return new ValidationResult("El tipo es obligatorio.", [nameof(Type)]);
        }

        if (Latitud < -90 || Latitud > 90)
        {
            yield return new ValidationResult("La latitud debe estar entre -90 y 90.", [nameof(Latitud)]);
        }
        if (Longitud < -180 || Longitud > 180)
        {
            yield return new ValidationResult("La longitud debe estar entre -180 y 180.", [nameof(Longitud)]);
        }
        if (string.IsNullOrWhiteSpace(Numero) && string.IsNullOrWhiteSpace(Bloque) &&
            string.IsNullOrWhiteSpace(Portal) && string.IsNullOrWhiteSpace(Escalera) &&
            string.IsNullOrWhiteSpace(Piso))
        {
            yield return new ValidationResult("Al menos uno de los campos 'Número', 'Bloque', 'Portal', 'Escalera' o 'Piso' debe ser proporcionado.", [nameof(Numero), nameof(Bloque), nameof(Portal), nameof(Escalera), nameof(Piso)]);
        }
    }

}