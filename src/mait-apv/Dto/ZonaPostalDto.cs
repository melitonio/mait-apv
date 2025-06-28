namespace Dto;

public record ZonaPostalDto
{
    public string Zona { get; init; } = string.Empty;
    public string Distrito { get; init; } = string.Empty;
    public string Codigo { get; init; } = string.Empty;
    public double[][] Poligono { get; init; } = [];
    public string Descripcion { get; init; } = string.Empty;
    public override string ToString() => $"{Codigo}, {Zona} - {Distrito}";
}