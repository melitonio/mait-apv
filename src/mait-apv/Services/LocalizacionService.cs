using Entities;
using MAIT.Interfaces;
using MAIT.Services;

namespace Services;

public class LocalizacionService(IDataService dataService1, ILogger<LocalizacionService> logger)
: BaseCrudService<LocalizacionService, Localizacion>(logger, dataService1)
{

}
