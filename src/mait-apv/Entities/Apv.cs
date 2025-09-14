using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dto;
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
    public Guid SerieId { get; set; }              // Serie documental para correspondencia
    public IEnumerable<Localizacion> Localiaciones { get; set; } = [];

    [NotMapped]
    public ZonaPostalDto? ZonaPostal { get; set; }

    public Localizacion? Localizacion => Localiaciones.SingleOrDefault(x => x.Activa);
    public static string DefaultNumero { get; } = $"apv-{DateTime.Now:yy}-00000";

    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }


    public string? EmergenciaNombre { get; set; }
    public string? EmergenciaTelefono { get; set; }
    public string? EmergenciaRelacion { get; set; }

    public double Latitud { get; set; }
    public double Longitud { get; set; }

    public string? FotoVivienda { get; set; }
}
