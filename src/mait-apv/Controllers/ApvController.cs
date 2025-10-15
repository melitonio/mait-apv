using Dto;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Services;

namespace Controllers;


[Route("apv")]
[ApiController]
public partial class ApvController
(
    IApvService crudService,
    LocalizacionService localizacionService,
    ILogger<ApvController> logger,
    ZonaPostalService zonaPostalService
) : MAIT.Services.BaseControllerCrudApproveEntityService<Apv, ApvPostDto, ApvPutDto, ApvDto>(crudService, logger)
{
    private readonly ZonaPostalService _zonaPostalService = zonaPostalService;
    private readonly LocalizacionService _localizacionService = localizacionService;

    protected async override Task<Apv?> OnRead(Guid id)
    {
        var entity = await base.OnRead(id) ?? throw new($"No se ha encontrado el apartado con ID: {id}");
        entity.ZonaPostal = await _zonaPostalService.GetZonaPostalAsync(entity.CodigoPostal!);
        entity.Localiaciones = await _localizacionService.GetByFilterAsync(l => l.ApvId == id) ?? [];
        return entity;
    }

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

}