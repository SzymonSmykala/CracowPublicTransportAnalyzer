using FluentAssertions;
using NSubstitute;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler.UnitTests.Vehicles;

[TestFixture]
public class VehicleServiceTests
{
    private IHttpClientFactory _httpClientFactory;
    private IVehicleRequestFactory _vehicleRequestFactory;
    private IVehicleService _uut;

    [SetUp]
    public void SetUp()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _vehicleRequestFactory = Substitute.For<IVehicleRequestFactory>();
        _uut = new VehicleService(_httpClientFactory, _vehicleRequestFactory);
    }

    [Test]
    public async Task ShouldGetAllVehicles()
    {
        // Arrange
        
        // Act
        var actual = await _uut.GetAllVehicles();

        // Assert
        actual.Should().NotBeNull();
    }
}