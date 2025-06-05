using Dto;
using Entities;
using MAIT.Interfaces;
using MAIT.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("apv/localizaciones")]
public class LocalizacionController(
    IDataService dataService,
    LocalizacionService srv,
    ZonaPostalService zonaPostalService,
    ILogger<LocalizacionController> logger
) : ApiCrudControllerBase<Localizacion, LocalizacionPostDto, LocalizacionPutDto, LocalizacionDto>(srv, logger)
{
    private readonly IDataService _dataService = dataService;
    private readonly ZonaPostalService _zonaPostalService = zonaPostalService;


    protected override Task OnCreateAsync(Localizacion entity, LocalizacionPostDto dto)
    {
        entity.CodigoPostal = dto.CodigoPostal;
        if (!string.IsNullOrEmpty(entity.CodigoPostal))
        {
            var zonaPostal = _zonaPostalService.GetZonaPostalAsync(entity.CodigoPostal).GetAwaiter().GetResult();
            entity.CodigoPostal = zonaPostal?.Codigo ?? throw new($"No se ha encontrado el código postal: {entity.CodigoPostal}");
        }
        return base.OnCreateAsync(entity, dto);
    }


    protected override Task OnUpdateAsync(Localizacion entity, LocalizacionPutDto dto)
    {
        entity.CodigoPostal = dto.CodigoPostal;
        if (!string.IsNullOrEmpty(entity.CodigoPostal))
        {
            var zonaPostal = _zonaPostalService.GetZonaPostalAsync(entity.CodigoPostal).GetAwaiter().GetResult();
            entity.CodigoPostal = zonaPostal?.Codigo ?? throw new($"No se ha encontrado el código postal: {entity.CodigoPostal}");
        }
        return base.OnUpdateAsync(entity, dto);
    }


    [HttpPost("{apvId}/locations")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> SetLocalization(Guid apvId, LocalizacionPostDto dto)
    {
        try
        {
            if (!_dataService.Exists<Apv>(x => x.Id == apvId))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("El apartado con ID: {Id} no existe o no coincide con el ID del dto", apvId);
                return new($"El apartado con ID: {apvId} no existe o no coincide con el ID del dto");
            }

            var localizaciones = (await _crudService.GetByFilterAsync(x => x.ApvId == apvId)).ToList();

            if (localizaciones.Any(x => x.Nombre == dto.Nombre))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("La localización '{Nombre}' ya existe en el apartado con ID: {Id}", dto.Nombre, apvId);
                return new($"La localización '{dto.Nombre}' ya existe en el apartado con ID: {apvId}");
            }

            dto.ToEntity(GetUsername(), out var entity);
            entity.ApvId = apvId;
            if (localizaciones.Count == 0)
            {
                entity.Activa = true; // Set the first localization as active
                await OnCreateAsync(entity, dto);
                return new(true);
            }
            else
            {
                await OnCreateAsync(entity, dto);
                foreach (var loc in localizaciones)
                {
                    loc.Activa = !dto.Activa && loc.Activa; // Set all existing localizations as inactive
                    await _crudService.UpdateItemAsync(loc);
                }
            }
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar localización al apartado con ID: {Id}", apvId);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al agregar localización al apartado");
        }
    }


    [HttpPost("{apvId}/locations/{nombre}/active")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> ActiveLocalization(Guid apvId, string nombre)
    {
        try
        {
            var loc = await _crudService.GetSingleByFilterAsync(x => x.ApvId == apvId && x.Nombre == nombre);
            if (loc == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogWarning("No se ha encontrado la localización '{Nombre}' en el apartado con ID: {Id}", nombre, apvId);
                return new($"No se ha encontrado la localización '{nombre}' en el apartado con ID: {apvId}");
            }
            loc.Activa = true; // Set the specified localization as active
            await _crudService.UpdateItemAsync(loc);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar localización al apartado con ID: {Id}", apvId);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al activar la localización");
        }
    }

}
