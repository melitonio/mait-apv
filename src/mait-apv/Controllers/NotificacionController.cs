using Dto;
using Entities;
using Interfaces;
using MAIT.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[Route("apv/{apv}/notificacion")]
[ApiController]
public partial class NotificacionController
(
    NotificacionService crudService,
    IApvService dataService,
    ILogger<NotificacionController> logger
) : BaseControllerCrudEntityService<Notificacion, NotificacionPostDto, NotificacionPutDto, NotificacionDto>(crudService, logger)
{
    private readonly IApvService _dataService = dataService;

    protected override Task<ICollection<Notificacion>> OnReadAll()
    {
        var apv = Request.RouteValues["apv"]?.ToString()
            ?? throw new("El APV es obligatorio.");
        
        return base.OnReadAll(x => x.Apv == apv);
    }
    
    protected override Task OnCreateAsync(Notificacion entity, NotificacionPostDto dto)
    {
        var apv = Request.RouteValues["apv"]?.ToString()
            ?? throw new("El APV es obligatorio.");
        
        entity.Apv = _dataService.GetSingleByFilterAsync(a => a.Numero == apv).GetAwaiter().GetResult()?.Numero
            ?? throw new($"No se ha encontrado el APV: {apv}");
        return base.OnCreateAsync(entity, dto);
    }
}

