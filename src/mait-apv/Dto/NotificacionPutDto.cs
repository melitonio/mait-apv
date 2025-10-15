using System.ComponentModel.DataAnnotations;
using MAIT.Interfaces;
using Entities;

namespace Dto;

public record NotificacionPutDto(
    Guid Id
) : IPutDto<Notificacion>
{
    public void UpdateEntity(in Notificacion entity, string usuario)
    {
        throw new InvalidOperationException("No se permite la actualización de notificaciones.");
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Id == Guid.Empty)
            yield return new("El ID de la notificación es obligatorio.", [nameof(Id)]);
    }
}