using System.Net.Http.Json;
using FluentAssertions;
using Integration;
using Helpers;
using MAIT.Interfaces;
using Dto;

namespace Endpoints;

public class EndpointTests(CustomWebApplicationFactory<Program> factory)
        : IntegrationTestBase(factory), IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    protected override bool UseTransaction => false; // desactiva rollback

    protected override async Task SeedAsync()
    {
        //    var series = SerieDataFactory.GenerateMany(3);
        //    Db.Database.EnsureDeleted();
        //   Db.AddRange(series);
        //    await Db.SaveChangesAsync();

        await Task.CompletedTask; // Simula un tiempo de espera para evitar conflictos de ID
    }

    [Fact]
    public async Task Post_Get_Delete_Serie_Works_Correctly()
    {
        // Arrange
        var n = 160;
        var items = n > 0 ? DataFactory.GenerateMany(n) : [];
        List<Guid> ids = [];
        foreach (var item in items)
        {
            // Act - POST
            var post = await _client.PostAsJsonAsync("/apv", item);
            var message = await post.Content.ReadAsStringAsync();
            post.EnsureSuccessStatusCode();

            var postResult = await post.Content.ReadFromJsonAsync<ResultModel<ApvDto>>();
            var id = postResult!.Data!.Id;

            // Act - GET
            var get = await _client.GetAsync($"/apv/{id}");
            var getResult = await get.Content.ReadFromJsonAsync<ResultModel<ApvDto>>();
            get.EnsureSuccessStatusCode();

            // Assert
            getResult.Should().NotBeNull();
            getResult!.Data!.CodigoPostal.Should().Be(item.Codigo);
            ids.Add(id);
        }

        // Act - DELETE
        foreach (var id in ids.Take(1))
        {
            await Task.Delay(5000); // Simula un tiempo de espera para evitar conflictos de ID

            // DELETE (opcional)
            var delete = await _client.DeleteAsync($"/apv/{id}");
            delete.EnsureSuccessStatusCode();
        }
    }


    [Fact]
    public async Task CanListSeededSeries()
    {
        var response = await _client.GetAsync("/apv");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ResultModel<IEnumerable<ApvDto>>>();
        result!.Data.Should().NotBeNull();
        result!.Data!.Count().Should().BeGreaterThanOrEqualTo(105);
    }
}
