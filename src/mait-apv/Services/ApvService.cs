using DotNetCore.CAP;
using MAIT.Interfaces;
using MAIT.Services;
using Microsoft.Extensions.Options;

using Events;
using Interfaces;
using Dto;
using Entities;

namespace Services;

public class ApvService : BaseCrudService<ApvService, Apv>, IApvService
{
    private readonly ICapPublisher _capPublisher;
    private readonly GqDataDto _gq;

    public ApvService(IDataService srv, ICapPublisher capPublisher, IOptions<GqDataDto> options, ILogger<ApvService> logger) : base(logger, srv)
    {
        _capPublisher = capPublisher;
        _gq = options.Value;

        _logger.LogInformation("Cargando zonas Postales: {}", _gq.Zonas.Select(a => "\n " + a.ToString()));

        OnItemCreated += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out var dto);
            var evt = new ApvCreadoEvent(args.Id, args.CreatedBy!, dto);
            await _capPublisher.PublishAsync(ApvCreadoEvent.Name, evt);
        };

        OnItemUpdated += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out var dto);
            var evt = new ApvActualizadoEvent(args.Id, args.UpdatedBy!, dto);
            await _capPublisher.PublishAsync(ApvActualizadoEvent.Name, evt);
        };

        OnItemDeleted += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out var dto);
            var evt = new ApvElimidadoEvent(args.Id, GetUsername(), dto);
            await _capPublisher.PublishAsync(ApvElimidadoEvent.Name, evt);
        };
    }

    private static string GetUsername()
    {
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
        return "Unknown";
    }
}
