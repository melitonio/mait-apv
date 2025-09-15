using Bogus;
using Entities;
using Dto;

namespace Helpers;

public static class DataFactory
{
    public static Faker<ApvPostDtoTest> ContactoDtoFaker => new Faker<ApvPostDtoTest>()
        .RuleFor(c => c.Fecha, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.CodigoPostal, f => f.Address.ZipCode());

    public static IEnumerable<ApvPostDtoTest> GenerateMany(int count = 50)
        => ContactoDtoFaker.Generate(count);
}


public record ApvPostDtoTest
(
    DateTime Fecha,
    string CodigoPostal
) : ApvPostDto
(
    CodigoPostal: CodigoPostal,
    Fecha: Fecha,
    Nombre: "",
    Apellidos: "",
    Telefono: "",
    EmergenciaNombre: "",
    EmergenciaTelefono: "",
    EmergenciaRelacion: "",
    Latitud: 0,
    Longitud: 0,
    Email: "",
    FotoVivienda: "",
    Calle:"",
    NumeroCalle: "",
    Piso: "",
    Puerta: "",
    Escalera: "",
    Bloque: "",
    Portal: ""    
)
{
    public ApvPostDtoTest() : this(DateTime.Now, "") { }

    public ApvPostDtoTest(ApvPostDto dto) : this(CodigoPostal: dto.CodigoPostal, Fecha: dto.Fecha) { }
}