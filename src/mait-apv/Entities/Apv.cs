using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAIT.Interfaces;
using Dto;

namespace Entities;


[Table(nameof(Apv))]
public class Apv : BaseApprovable, INumberSequence
{
    [Required]
    public DateOnly Fecha { get; init; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    public string? Numero { get; set; }
    public string? Codigo { get; set; }
    public DateOnly? FechaBaja { get; init; }
    public string? CodigoPostal { get; init; }      // Zona postal para apartados
    public Guid ContactoId { get; set; }            // Titular del apartado
    public Guid SeriedID { get; set; }              // Serie documental para correspondencia
    public IEnumerable<LocalizacionDto> Localiaciones { get; set; } = [];

    public LocalizacionDto? Localizacion => Localiaciones.SingleOrDefault(x => x.Activa);
    public static string DefaultNumero { get; } = $"apv-{DateTime.Now:yy-MM}-0000000";
}
