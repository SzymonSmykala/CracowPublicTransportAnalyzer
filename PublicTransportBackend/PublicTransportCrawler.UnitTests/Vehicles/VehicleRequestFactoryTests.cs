using FluentAssertions;
using PublicTransportCrawler.Vehicles;
using PublicTransportCrawler.Vehicles.Factories;

namespace PublicTransportCrawler.UnitTests.Vehicles;

[TestFixture]
public class VehicleRequestFactoryTests
{
    private IVehicleRequestFactory _uut;

    [SetUp]
    public void SetUp()
    {
        _uut = new VehicleRequestFactory();
    }
    
    [Test]
    public void ShouldCreateGetTramsRequest()
    {
        // Arrange
        
        // Act
        var actual = _uut.CreateGetTramRequest();
        // Assert
        actual.Method.Should().Be(HttpMethod.Get);
        actual.RequestUri.Should().Be(Constants.TramsEndpoint);
    }
}