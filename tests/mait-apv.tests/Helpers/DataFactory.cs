using Bogus;
using Entities;
using Dto;

namespace Helpers;

public static class DataFactory
{
    public static Faker<ApvPostDtoTest> ContactoDtoFaker => new Faker<ApvPostDtoTest>()
        .RuleFor(c => c.Fecha, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.CodigoPostal, f => f.Address.ZipCode())
        .RuleFor(c => c.SerieId, f => f.Random.Guid());

    public static IEnumerable<ApvPostDtoTest> GenerateMany(int count = 50)
        => ContactoDtoFaker.Generate(count);
}


public record ApvPostDtoTest
(
    Guid SerieId,
    DateTime Fecha,
    string CodigoPostal
) : ApvPostDto(CodigoPostal: CodigoPostal, Fecha: Fecha, SerieId: SerieId, Nombre: "", Apellidos: "", EmergenciaNombre: "", EmergenciaTelefono: "", EmergenciaRelacion: "", Latitud: 0, Longitud: 0)
{
    public ApvPostDtoTest() : this(Guid.NewGuid(), DateTime.Now, "") { }

    public ApvPostDtoTest(ApvPostDto dto) : this(CodigoPostal: dto.CodigoPostal, Fecha: dto.Fecha, SerieId: dto.SerieId) { }
}