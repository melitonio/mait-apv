using Entities;
using MAIT.Interfaces;

namespace Dto;

public record ApvDto : IGetDto<ApvDto, Apv>
{
    public Guid Id { get; set; }
    public Guid ContactoId { get; set; }
    public Guid SeriedID { get; set; }              // Serie documental para correspondencia
    public DateOnly Fecha { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public string? Numero { get; set; }
    public string? Codigo { get; set; }
    public bool IsActive { get; set; }
    public bool IsApproved { get; set; }

    public IEnumerable<LocalizacionDto> Localiaciones { get; private set; } = [];

    public static bool FromEntity(Apv entity, out ApvDto dto)
    {
        dto = new ApvDto
        {
            Id = entity.Id,
            Fecha = entity.Fecha,
            Numero = entity.Numero ?? string.Empty,
            Codigo = entity.Codigo,
            ContactoId = entity.ContactoId,
            SeriedID = entity.SeriedID,
            IsActive = entity.IsActive && (entity.IsApproved ?? false),
            IsApproved = entity.IsApproved ?? false,
            Localiaciones = entity.Localiaciones
        };
        return true;
    }
}
