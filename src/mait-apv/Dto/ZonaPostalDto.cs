namespace Dto;

public record ZonaPostalDto(
    string Zona,
    string Distrito,
    string Codigo,
    double[][] Poligono,
    string Descripcion
)
{
    public override string ToString() => $"{Codigo}, {Zona} - {Distrito}";
}