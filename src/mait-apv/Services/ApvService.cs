using DotNetCore.CAP;
using MAIT.Interfaces;
using MAIT.Services;

using Events;
using Interfaces;
using Dto;
using Entities;

namespace Services;

public class ApvService : BaseCrudService<Apv>, IApvService
{
    private readonly ICapPublisher _capPublisher;

    public ApvService(IDataService srv, ICapPublisher capPublisher, ILogger<ApvService> logger) : base(logger, srv)
    {
        _capPublisher = capPublisher;

        OnItemCreated += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out ApvDto dto);
            var evt = new ApvCreadoEvent(args.Id, args.CreatedBy!, dto);
            await _capPublisher.PublishAsync(ApvCreadoEvent.Name, evt);
        };

        OnItemUpdated += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out ApvDto dto);
            var evt = new ApvActualizadoEvent(args.Id, args.UpdatedBy!, dto);
            await _capPublisher.PublishAsync(ApvActualizadoEvent.Name, evt);
        };

        OnItemDeleted += async (sender, args) =>
        {
            ApvDto.FromEntity(args, out ApvDto dto);
            var evt = new ApvElimidadoEvent(args.Id, GetUsername(), dto);
            await _capPublisher.PublishAsync(ApvElimidadoEvent.Name, evt);
        };
    }

    private static string GetUsername()
    {
        // return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";
        return "Unknown";
    }

    protected override void Init()
    {
    }
}
