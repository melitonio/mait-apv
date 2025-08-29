using Entities;
using MAIT.Interfaces;
using MAIT.Services;

namespace Services;

public class LocalizacionService(IDataService dataService1, ILogger<LocalizacionService> logger)
: BaseCrudService<Localizacion>(logger, dataService1)
{
    protected override void Init()
    {

    }
}
