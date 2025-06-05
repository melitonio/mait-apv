using Dto;
using Microsoft.Extensions.Options;

namespace Services;

public class ZonaPostalService(IOptions<GqDataDto> options, ILogger<ZonaPostalService> logger)
{
    private readonly GqDataDto _gq = options.Value;
    private readonly ILogger _logger = logger;

    public Task<ZonaPostalDto> GetZonaPostalAsync(string codigoPostal)
    {
        var zona = _gq.Zonas.FirstOrDefault(z => z.Codigo == codigoPostal) ?? throw new KeyNotFoundException($"Zona postal con código {codigoPostal} no encontrada.");
        return Task.FromResult(zona);
    }

    public Task<IEnumerable<ZonaPostalDto>> GetZonasPostalesAsync()
    {
        return Task.FromResult(_gq.Zonas ?? []);
    }
}