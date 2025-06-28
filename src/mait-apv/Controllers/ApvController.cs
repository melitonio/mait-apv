using MAIT.Services;
using Dto;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using MAIT.Interfaces;
using Services;

namespace Controllers;


[Route("apv")]
[ApiController]
public partial class ApvController
(
    IApvService crudService,
    ILogger<ApvController> logger,
    ZonaPostalService zonaPostalService
) : ApiCrudControllerBase<Apv, ApvPostDto, ApvPutDto, ApvDto>(crudService, logger)
{
    private readonly ZonaPostalService _zonaPostalService = zonaPostalService;

    protected override Task OnCreateAsync(Apv entity, ApvPostDto dto)
    {
        entity.CodigoPostal = dto.CodigoPostal;
        if (!string.IsNullOrEmpty(entity.CodigoPostal))
        {
            var zonaPostal = _zonaPostalService.GetZonaPostalAsync(entity.CodigoPostal).GetAwaiter().GetResult();
            entity.CodigoPostal = zonaPostal?.Codigo ?? throw new($"No se ha encontrado la zona postal con el código: {entity.CodigoPostal}");
        }
        return base.OnCreateAsync(entity, dto);
    }

    protected override Task OnUpdateAsync(Apv entity, ApvPutDto dto)
    {
        if (!string.IsNullOrEmpty(entity.CodigoPostal))
        {
            var zonaPostal = _zonaPostalService.GetZonaPostalAsync(entity.CodigoPostal).GetAwaiter().GetResult();
            entity.CodigoPostal = zonaPostal?.Codigo ?? throw new($"No se ha encontrado la zona postal con el código: {entity.CodigoPostal}");
        }
        return base.OnUpdateAsync(entity, dto);
    }


    [HttpPost("{id}/active")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> Activate(Guid id)
    {
        try
        {
            var entity = await _crudService.GetByIdAsync(id);
            if (entity == default)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogWarning("No se ha encontrado el apartado con ID: {Id}", id);
                return new($"No se ha encontrado el apartado con ID: {id}");
            }
            if (entity.IsActive)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("El apartado con ID: {Id} ya está activo.", id);
                return new($"El apartado con ID: {id} ya está activo.");
            }
            entity.Activate(GetUsername());
            await _crudService.UpdateItemAsync(entity);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al activar el apartado con ID: {Id}", id);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al activar el apartado");
        }
    }


    [HttpPost("{id}/suspend")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> Suspend(Guid id)
    {
        try
        {
            var entity = await _crudService.GetByIdAsync(id);
            if (entity == default)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogWarning("No se ha encontrado el apartado con ID: {Id}", id);
                return new($"No se ha encontrado el apartado con ID: {id}");
            }
            if (entity.IsSuspended)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("El apartado con ID: {Id} ya está suspendido", id);
                return new($"El apartado con ID: {id} ya está suspendido");
            }
            entity.Suspend(GetUsername());
            await _crudService.UpdateItemAsync(entity);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al suspender el apartado con ID: {Id}", id);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al suspender el apartado");
        }
    }


    [HttpPost("{id}/aprove")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> Aprove(Guid id)
    {
        try
        {
            var entity = await _crudService.GetByIdAsync(id);
            if (entity == default)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogWarning("No se ha encontrado el apartado con ID: {Id}", id);
                return new($"No se ha encontrado el apartado con ID: {id}");
            }

            if (entity.IsApproved == true)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("El apartado con ID: {Id} ya está aprobado", id);
                return new($"El apartado con ID: {id} ya está aprobado");
            }
            entity.Approve(GetUsername());
            await _crudService.UpdateItemAsync(entity);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al aprobar el apartado con ID: {Id}", id);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al aprobar el apartado");
        }
    }

}