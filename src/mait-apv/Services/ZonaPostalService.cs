using Dto;
using Microsoft.Extensions.Options;

namespace Services;

public class ZonaPostalService(IOptions<GqDataDto> options, ILogger<ZonaPostalService> logger)
{
    private readonly GqDataDto _gq = options.Value;
    private readonly ILogger _logger = logger;


    /// <summary>
    /// Obtiene una zona postal por su código.
    /// </summary>
    public Task<ZonaPostalDto?> GetZonaPostalAsync(string codigoPostal)
    {
        var zona = _gq.Zonas.FirstOrDefault(z => z.Codigo == codigoPostal);
        return Task.FromResult(zona);
    }


    /// <summary>
    /// Obtiene una lista de todas las zonas postales.
    /// </summary>
    public Task<IEnumerable<ZonaPostalDto>> GetZonasPostalesAsync()
    {
        return Task.FromResult(_gq.Zonas ?? []);
    }


    /// <summary>
    /// Obtiene una lista de distritos únicos de las zonas postales.
    /// </summary>
    public Task<IEnumerable<DistritoDto>> GetDistritos()
    {
        return Task.FromResult(_gq.Zonas?.Select(z => new DistritoDto(z.Distrito, z.Provincia))?.Distinct() ?? []);
    }



    /// <summary>
    /// Obtiene una lista de distritos únicos de las zonas postales.
    /// </summary>
    public Task<IEnumerable<ZonaPostalDto>> GetZonasFisicasAsync()
    {
        return Task.FromResult(_gq.Zonas?.Where
        (
            z => !(z.Codigo.EndsWith("07") || z.Codigo.EndsWith("08") || z.Codigo.EndsWith("09"))
        ).Distinct() ?? []);
    }

    /// <summary>
    /// Obtiene una lista de todas las zonas postales de un distrito o zona.
    /// </summary>
    public Task<IEnumerable<ZonaPostalDto>> GetZonasPostalesAsync(string zona)
    {
        if (string.IsNullOrWhiteSpace(zona))
        {
            _logger.LogWarning("El distrito proporcionado es nulo o vacío.");
            throw new ArgumentException("El distrito no puede ser nulo o vacío.", nameof(zona));
        }

        var zonas = _gq.Zonas?.Where(z => z.Distrito.Equals(zona, StringComparison.OrdinalIgnoreCase) ||
                    z.Zona.Equals(zona, StringComparison.OrdinalIgnoreCase)) ?? [];
        return Task.FromResult(zonas);
    }
}