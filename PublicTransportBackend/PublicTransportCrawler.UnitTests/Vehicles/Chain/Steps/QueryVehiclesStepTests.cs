using FluentAssertions;
using PublicTransportCrawler.Vehicles;
using PublicTransportCrawler.Vehicles.Chain.Steps;
using TddXt.AnyRoot.Strings;
using Vehicle = PublicTransportCrawler.Vehicles.DTO.Vehicle;

namespace PublicTransportCrawler.UnitTests.Vehicles.Chain.Steps;

[TestFixture]
public class QueryVehiclesStepTests
{
    private IStep _uut;

    [SetUp]
    public void SetUp()
    {
        _uut = new QueryVehiclesStep();
    }

    [Test]
    public async Task ShouldQueryVehicles()
    {
        // Arrange
        var context = Any.Instance<CrawlingContext>();
        var correctVehicle = Any.Instance<Vehicle>();
        var lineToQuery = Any.String();
        correctVehicle.Name = $"{Any.String()}{lineToQuery}{Any.String()}";
        context.Vehicles.Add(correctVehicle);
        context.LineNumber = lineToQuery;
        
        // Act
        await _uut.ExecuteAsync(context);

        // Assert
        context.Vehicles.Should().Contain(correctVehicle);
    }
    
}