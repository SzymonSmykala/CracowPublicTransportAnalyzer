using FluentAssertions;
using NSubstitute;
using PublicTransportCrawler.Vehicles;
using PublicTransportCrawler.Vehicles.Chain.Steps;
using TddXt.AnyRoot.Collections;
using Vehicle = PublicTransportCrawler.Vehicles.DTO.Vehicle;

namespace PublicTransportCrawler.UnitTests.Vehicles.Chain.Steps;

[TestFixture]
public class GetAllBusesStepTests
{
    private IVehicleService _vehicleService;
    private IStep _uut;

    [SetUp]
    public void SetUp()
    {
        _vehicleService = Substitute.For<IVehicleService>();
        _uut = new GetAllBusesStep(_vehicleService);
    }

    [Test]
    public void ShouldGetAllBuses()
    {
        // Arrange
        var context = Any.Instance<CrawlingContext>();
        var busesResult = Any.List<Vehicle>();
        _vehicleService.GetAllBusesAsync().Returns(busesResult);
        
        // Act
        _uut.ExecuteAsync(context);

        // Assert
        context.Vehicles.Should().BeEquivalentTo(busesResult);
        _vehicleService.Received().GetAllBusesAsync();
    }
}