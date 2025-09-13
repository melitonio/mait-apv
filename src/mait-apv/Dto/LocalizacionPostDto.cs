using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record class LocalizacionPostDto
(
    string? Calle,
    string? Numero,
    string? Bloque,
    string? Portal,
    string? Escalera,
    string? Piso,
    string? Puerta,
    string? CodigoPostal, // de la zona postal asociada a la dirección
    string? Descripcion,
    bool Activa,
    double Latitud,
    double Longitud,
    string? Titulo, // nombre de la dirección, por ejemplo: "Oficina Central", "Almacén Principal", etc.
    string? Tipo
) : IPostDto<Localizacion>, IValidatableObject
{
    public bool ToEntity(string usuario, out Localizacion entity, Guid? id = null)
    {
        entity = ToEntity(usuario, id);
        return true;
    }

    public Localizacion ToEntity(string usuario, Guid? id = null)
    {
        return new Localizacion
        {
            Id = id ?? Guid.NewGuid(),
            Calle = Calle ?? string.Empty,
            Numero = Numero ?? string.Empty,
            Bloque = Bloque ?? string.Empty,
            Portal = Portal ?? string.Empty,
            Escalera = Escalera ?? string.Empty,
            Piso = Piso ?? string.Empty,
            Puerta = Puerta ?? string.Empty,
            CodigoPostal = CodigoPostal ?? string.Empty,
            Descripcion = Descripcion ?? string.Empty,
            Activa = Activa,
            Latitud = Latitud,
            Longitud = Longitud,
            Nombre = Titulo ?? string.Empty,
            Tipo = Tipo ?? string.Empty,
            CreatedBy = usuario,
        };
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
        if (string.IsNullOrWhiteSpace(Titulo))
        {
            yield return new ValidationResult("El nombre es obligatorio.", [nameof(Titulo)]);
        }
        if (string.IsNullOrWhiteSpace(Tipo))
        {
            yield return new ValidationResult("El tipo es obligatorio.", [nameof(Tipo)]);
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