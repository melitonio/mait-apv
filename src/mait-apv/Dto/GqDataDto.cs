namespace Dto;

public record GqDataDto
{
    public string Pais { get; init; } = string.Empty;
    public string Codigo { get; init; } = string.Empty;
    public IEnumerable<ZonaPostalDto> Zonas { get; init; } = [];
}