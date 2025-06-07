using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record class LocalizacionPostDto
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
    string Tipo
) : IPostDto<Localizacion>, IValidatableObject
{
    public bool ToEntity(string usuario, out Localizacion entity, Guid? id = null)
    {
        entity = new Localizacion
        {
            Id = id ?? Guid.NewGuid(),
            Calle = Calle,
            Numero = Numero,
            Bloque = Bloque,
            Portal = Portal,
            Escalera = Escalera,
            Piso = Piso,
            CodigoPostal = CodigoPostal,
            Descripcion = Descripcion,
            Activa = Activa,
            Latitud = Latitud,
            Longitud = Longitud,
            Nombre = Nombre,
            Tipo = Tipo,
            CreatedBy = usuario,
        };
        return true;
    }

    public Localizacion ToEntity(string usuario, Guid? id = null)
    {
        ToEntity(usuario, out var entity, id);
        return entity;
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