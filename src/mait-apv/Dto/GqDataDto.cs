using System.Text.Json.Serialization;

namespace Dto;

public record GqDataDto(
    string Pais,
    string Codigo,
    IEnumerable<ZonaPostalDto> Zonas
)
{
}