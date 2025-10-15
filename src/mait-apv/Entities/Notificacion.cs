using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAIT.Interfaces;

namespace Entities;


[Table(nameof(Notificacion))]
public class Notificacion : BaseEntity
{
    [Required]
    public string Apv { get; set; } = string.Empty;  // APV identifier (could be a GUID or other unique string)

    [Required]
    public string Tipo { get; set; } = string.Empty;   // email, sms, push

    public string Asunto { get; set; } = string.Empty;

    [Required]
    public string Mensaje { get; set; } = string.Empty;

    public DateTime FechaEnvio { get; set; } = DateTime.MinValue;

    public DateTime FechaLectura { get; set; } = DateTime.MinValue;

    public string Error { get; set; } = string.Empty;
    
    public bool Enviado => FechaEnvio != DateTime.MinValue && string.IsNullOrEmpty(Error);
    public bool Leido => FechaLectura != DateTime.MinValue;
}