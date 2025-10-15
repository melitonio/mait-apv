using System.ComponentModel.DataAnnotations;
using Entities;
using MAIT.Interfaces;

namespace Dto;

public record NotificacionPostDto(
    string Tipo,
    string Asunto,
    string Mensaje
) : IPostDto<Notificacion>
{
    public bool ToEntity(string usuario, out Notificacion entity, Guid? id = null)
    {
        entity = new Notificacion
        {
            Id = id ?? Guid.NewGuid(),
            Tipo = Tipo,
            Asunto = Asunto,
            Mensaje = Mensaje,
            FechaEnvio = DateTime.MinValue,
            FechaLectura = DateTime.MinValue,
            Error = string.Empty,
            CreatedBy = usuario
        };
        return true;
    }

    public Notificacion ToEntity(string usuario, Guid? id = null)
    {
        ToEntity(usuario, out var entity, id);
        return entity;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Tipo)) 
            yield return new("El tipo de notificación es obligatorio.", [nameof(Tipo)]);
        
        if (string.IsNullOrWhiteSpace(Asunto)) 
            yield return new("El asunto es obligatorio.", [nameof(Asunto)]);
        
        if (string.IsNullOrWhiteSpace(Mensaje)) 
            yield return new("El mensaje es obligatorio.", [nameof(Mensaje)]);

        // Validar tipos permitidos
        var tiposPermitidos = new[] { "email", "sms", "push" };
        if (!string.IsNullOrWhiteSpace(Tipo) && !tiposPermitidos.Contains(Tipo.ToLower()))
            yield return new($"El tipo debe ser uno de: {string.Join(", ", tiposPermitidos)}.", [nameof(Tipo)]);
    }
}