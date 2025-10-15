using Entities;
using MAIT.Interfaces;

namespace Dto;


public record NotificacionDto(
    Guid? Id,
    string? Apv,
    string? Tipo,
    string? Asunto,
    string? Mensaje,
    DateTime? FechaEnvio,
    DateTime? FechaLectura,
    string? Error,
    bool? Enviado,
    bool? Leida,
    DateTime? Fecha
) : IGetDto<Notificacion>
{

    public static bool FromEntity<TDto>(Notificacion entity, out TDto dto) where TDto : IGetDto<Notificacion>
    {
        dto = (TDto)FromEntity(entity);
        return true;
    }

    public static IGetDto<Notificacion> FromEntity(Notificacion entity)
    {
        return new NotificacionDto
        (
            Id: entity.Id,
            Apv: entity.Apv,
            Tipo: entity.Tipo,
            Asunto: entity.Asunto,
            Mensaje: entity.Mensaje,
            FechaEnvio: entity.FechaEnvio,
            FechaLectura: entity.FechaLectura,
            Error: entity.Error,
            Enviado: entity.Enviado,
            Leida: entity.Leido,
            Fecha: entity.CreatedAt?.DateTime
        );
    }
}