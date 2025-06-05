using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAIT.Interfaces;

namespace Entities;


[Table(nameof(Apv))]
public class Apv : BaseApprovable, INumberSequence
{
    [Required]
    public DateOnly Fecha { get; init; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    public string? Numero { get; set; }
    public DateOnly? FechaBaja { get; set; }
    public string? CodigoPostal { get; set; }      // Zona postal para apartados
    public Guid ContactoId { get; set; }            // Titular del apartado
    public Guid SerieId { get; set; }              // Serie documental para correspondencia
    public IEnumerable<Localizacion> Localiaciones { get; set; } = [];

    public Localizacion? Localizacion => Localiaciones.SingleOrDefault(x => x.Activa);
    public static string DefaultNumero { get; } = $"apv-{DateTime.Now:yy-MM}-0000000";
}
