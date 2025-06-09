using System.ComponentModel.DataAnnotations.Schema;
using MAIT.Interfaces;

namespace Entities;

[Table(nameof(Localizacion))]
public class Localizacion : BaseEntity
{
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bloque { get; set; } = string.Empty;
    public string Portal { get; set; } = string.Empty; // Portal o número de entrada
    public string Escalera { get; set; } = string.Empty; // Escalera del edificio
    public string Piso { get; set; } = string.Empty; // Piso del edificio
    public string CodigoPostal { get; set; } = string.Empty; // de la zona postal asociada a la dirección
    public bool Activa { get; set; }
    public double Latitud { get; set; } // Latitud de la ubicación
    public double Longitud { get; set; } // Longitud de la ubicación
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;

    public Guid ApvId { get; set; } // Identificador del apartado al que pertenece esta localización

    [ForeignKey(nameof(ApvId))]
    public Apv? Apv { get; set; } // Navegación a la entidad Apv asociada
}