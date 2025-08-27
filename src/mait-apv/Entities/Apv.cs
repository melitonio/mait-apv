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
    public Guid SerieId { get; set; }              // Serie documental para correspondencia
    public IEnumerable<Localizacion> Localiaciones { get; set; } = [];

    public Localizacion? Localizacion => Localiaciones.SingleOrDefault(x => x.Activa);
    public static string DefaultNumero { get; } = $"apv-{DateTime.Now:yy-MM}-0000000";

    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string NombreCompleto => $"{Nombre} {Apellidos}".Trim();


    public string? EmergenciaNombre { get; set; }
    public string? EmergenciaTelefono { get; set; }
    public string? EmergenciaRelacion { get; set; }

    public double Latitud { get; set; }
    public double Longitud { get; set; }
}
