using Dto;
using MAIT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;


[Route("apv/zonas")]
[ApiController]
public class ZonaPostalController(ZonaPostalService srv, ILogger<ZonaPostalController> logger) : ControllerBase
{
    private readonly ZonaPostalService _zonaPostalService = srv;
    private readonly ILogger<ZonaPostalController> _logger = logger;



    [HttpGet]
    public async ValueTask<ResultModel<IEnumerable<ZonaPostalDto>>> GetAll()
    {
        try
        {
            var zonas = await _zonaPostalService.GetZonasPostalesAsync();
            return new(zonas.OrderBy(z => z.Codigo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las zonas postales");
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener las zonas postales: {ex.Message}");
        }
    }


    [HttpGet("filtrar/{zona}")]
    public async ValueTask<ResultModel<IEnumerable<ZonaPostalDto>>> GetByDistrito(string zona)
    {
        try
        {
            var zonas = await _zonaPostalService.GetZonasPostalesAsync(zona);
            return new(zonas.OrderBy(z => z.Codigo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las zonas postales del distrito {Distrito}", zona);
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener las zonas postales: {ex.Message}");
        }
    }


    [HttpGet("{codigo}")]
    public async ValueTask<ResultModel<ZonaPostalDto>> Get(string codigo)
    {
        try
        {
            var zona = await _zonaPostalService.GetZonaPostalAsync(codigo);
            if (zona == default)
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


    [HttpGet("distritos")]
    public async ValueTask<ResultModel<IEnumerable<string>>> GetDistritos()
    {
        try
        {
            var distritos = await _zonaPostalService.GetDistritos();
            return new(distritos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los distritos");
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new($"Error al obtener los distritos: {ex.Message}");
        }
    }

}