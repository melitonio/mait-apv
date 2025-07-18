namespace Dto;

public record ZonaPostalDto
{
    public string Provincia { get; init; } = string.Empty;
    public string Distrito { get; init; } = string.Empty;
    public string Zona { get; init; } = string.Empty;
    public string Codigo { get; init; } = string.Empty;
    public double[] Centro { get; init; } = [];
    public double[][] Poligono { get; init; } = [];
    public string Descripcion { get; init; } = string.Empty;
    public override string ToString() => $"{Codigo}, {Zona} - {Distrito}";
}