using Bogus;
using Entities;
using Dto;

namespace Helpers;

public static class DataFactory
{
    public static Faker<ApvPostDtoTest> ContactoDtoFaker => new Faker<ApvPostDtoTest>()
        .RuleFor(c => c.Fecha, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.CodigoPostal, f => f.Address.ZipCode())
        .RuleFor(c => c.ContactoId, f => f.Random.Guid())
        .RuleFor(c => c.SerieId, f => f.Random.Guid());

    public static IEnumerable<ApvPostDtoTest> GenerateMany(int count = 50)
        => ContactoDtoFaker.Generate(count);
}


public record ApvPostDtoTest
(
    Guid ContactoId,
    Guid SerieId,
    DateTime Fecha,
    string CodigoPostal
) : ApvPostDto(ContactoId: ContactoId, SerieId: SerieId, Fecha: Fecha, CodigoPostal: CodigoPostal)
{
    public ApvPostDtoTest() : this(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "") { }

    public ApvPostDtoTest(ApvPostDto dto) : this(ContactoId: dto.ContactoId, SerieId: dto.SerieId, Fecha: dto.Fecha, CodigoPostal: dto.CodigoPostal) { }
}