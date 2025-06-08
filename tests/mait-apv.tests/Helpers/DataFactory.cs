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
    Guid Id,
    Guid ContactoId,
    Guid SerieId,
    DateTime Fecha,
    string? Numero,
    string? CodigoPostal
) : ApvPostDto(Id, ContactoId, SerieId, Fecha, Numero, CodigoPostal)
{
    public ApvPostDtoTest() : this(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, null, null) { }

    public ApvPostDtoTest(ApvPostDto dto) : this(dto.Id, dto.ContactoId, dto.SerieId, dto.Fecha, dto.Numero, dto.CodigoPostal) { }
}