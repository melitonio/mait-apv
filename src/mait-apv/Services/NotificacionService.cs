using Entities;
using MAIT.Interfaces;
using MAIT.Services;

namespace Services;

public class NotificacionService(IDataService srv, ILogger<NotificacionService> logger)
    : BaseCrudService<Notificacion>(logger, srv)
{
    protected override void Init() { }
}