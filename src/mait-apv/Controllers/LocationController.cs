using Dto;
using MAIT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

public partial class ApvController
{

    [HttpPost("{id}/locations")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> SetLocalization(Guid id, LocalizacionDto dto)
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

            if (entity.Localiaciones.Any(x => x.Nombre == dto.Nombre))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("La localización '{Nombre}' ya existe en el apartado con ID: {Id}", dto.Nombre, id);
                return new($"La localización '{dto.Nombre}' ya existe en el apartado con ID: {id}");
            }

            if (!entity.Localiaciones.Any())
            {
                entity.Localiaciones = [dto with { Activa = true }];
            }
            else
            {
                entity.Localiaciones = dto.Activa ?
                [.. entity.Localiaciones.Select(x => x with { Activa = false }), dto]
                : [.. entity.Localiaciones, dto];
            }

            entity.UpdatedBy = GetUsername();
            await _crudService.UpdateItemAsync(entity);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar localización al apartado con ID: {Id}", id);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al agregar localización al apartado");
        }
    }


    [HttpPost("{id}/locations/{nombre}/active")]
    [ProducesResponseType(typeof(ResultModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultModel<bool>> ActiveLocalization(Guid id, string nombre)
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

            if (!entity.Localiaciones.Any(x => x.Nombre == nombre))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogWarning("La localización '{Nombre}' no existe en el apartado con ID: {Id}", nombre, id);
                return new($"La localización '{nombre}' no existe en el apartado con ID: {id}");
            }
            else
            {
                entity.Localiaciones = [.. entity.Localiaciones.Select(x =>
                    x.Nombre == nombre
                        ? x  with { Activa = true }
                        : x with { Activa = false } )];
            }

            entity.UpdatedBy = GetUsername();
            await _crudService.UpdateItemAsync(entity);
            return new(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar localización al apartado con ID: {Id}", id);
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new("Error al agregar localización al apartado");
        }
    }

}
