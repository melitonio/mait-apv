using DotNetCore.CAP;
using Entities;
using Events;
using Interfaces;

namespace Subscribers;

public class ExpedienteEventHandler(IApvService srv, ILogger<ExpedienteEventHandler> logger) : ICapSubscribe
{
    private readonly ILogger _logger = logger;
    private readonly IApvService _srv = srv;


    [CapSubscribe(ExpedienteCreadoEvent.Name)]
    public async Task HandleExpedienteCreado(ExpedienteCreadoEvent evt)
    {
        evt.Dto.ToEntity(evt.User, out var apv, evt.Id);

        // if (_srv.Exists<Apv>(x => x.Codigo == evt.Dto.id))
        // {
        //     _logger.LogWarning("El apv {} ya existe.", apv.Numero);
        //     return;
        // }

        await _srv.CreateItemAsync(apv);
        _logger.LogInformation("Apv creado creado: {}", apv.Numero);
    }
}


