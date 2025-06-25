namespace Dto;

public readonly record struct ZonaPostalDto
{
    public string Zona { get; init; }
    public string Distrito { get; init; }
    public string Codigo { get; init; }
    public double[][] Poligono { get; init; }
    public string Descripcion { get; init; }
    public override string ToString() => $"{Codigo}, {Zona} - {Distrito}";
}