using FluentAssertions;
using PublicTransportCrawler.Vehicles;

namespace PublicTransportCrawler.UnitTests.Vehicles;

[TestFixture]
public class VehicleServiceTests
{
    private IVehicleService _uut;
    
    [SetUp]
    public void SetUp()
    {
        _uut = new VehicleService();
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