using Bogus;
using Entities;
using Dto;

namespace Helpers;

public static class DataFactory
{
    public static Faker<ApvPostDto> ContactoDtoFaker => new Faker<ApvPostDto>()
        .RuleFor(c => c.Fecha, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
        .RuleFor(c => c.Codigo, f => f.Random.AlphaNumeric(10))
        .RuleFor(c => c.CodigoPostal, f => f.Address.ZipCode())
        .RuleFor(c => c.ContactoId, f => f.Random.Guid())
        .RuleFor(c => c.SeriedID, f => f.Random.Guid());

    public static IEnumerable<ApvPostDto> GenerateMany(int count = 50)
        => ContactoDtoFaker.Generate(count);
}
