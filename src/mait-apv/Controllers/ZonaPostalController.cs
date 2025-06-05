using Dto;
using MAIT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;


[Route("zona-postal")]
[ApiController]
public class ZonaPostalController(ZonaPostalService srv, ILogger<ZonaPostalController> logger) : ControllerBase
{
    private readonly ZonaPostalService _zonaPostalService = srv;
    private readonly ILogger<ZonaPostalController> _logger = logger;


    [HttpGet]
    public ResultModel<IEnumerable<ZonaPostalDto>> GetAll()
    {
        try
        {
            var zonas = _zonaPostalService.GetZonasPostalesAsync().Result.OrderBy(z => z.Codigo);
            return new(zonas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las zonas postales");
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener las zonas postales: {ex.Message}");
        }
    }


    [HttpGet("{codigo}")]
    public ResultModel<ZonaPostalDto> Get(string codigo)
    {
        try
        {
            var zona = _zonaPostalService.GetZonaPostalAsync(codigo).Result;
            if (zona == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new($"Zona postal con código {codigo} no encontrada.");
            }
            return new(zona);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la zona postal con código {Codigo}", codigo);
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener la zona postal: {ex.Message}");
        }
    }

    [HttpGet("filtrar/{zona}")]
    public ResultModel<IEnumerable<ZonaPostalDto>> GetByDistrito(string zona)
    {
        try
        {
            var zonas = _zonaPostalService.GetZonasPostalesAsync().Result
                .Where(z => z.Distrito.Equals(zona, StringComparison.OrdinalIgnoreCase)
                || z.Zona.Equals(zona, StringComparison.OrdinalIgnoreCase)).OrderBy(z => z.Codigo);
            if (!zonas.Any())
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new($"No se encontraron zonas postales para el distrito {zona}.");
            }
            return new(zonas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las zonas postales del distrito {Distrito}", zona);
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener las zonas postales: {ex.Message}");
        }
    }

}