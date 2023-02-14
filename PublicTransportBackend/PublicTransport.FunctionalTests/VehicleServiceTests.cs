using Microsoft.AspNetCore.Mvc.Internal;
using NUnit.Framework;
using PublicTransportCrawler.Vehicles;
using PublicTransportCrawler.Vehicles.Factories;

namespace PublicTransport.FunctionalTests;

[TestFixture]
public class VehicleServiceTests
{
    private IVehicleService _vehicleService;

    [SetUp]
    public void SetUp()
    {
        IHttpClientFactory factory = new MyHttpRequestFactory();
        _vehicleService = new VehicleService(factory, new VehicleRequestFactory());
    }
    
    
    [Test]
    public async Task ShouldReturn()
    {
        // Arrange
        
        // Act
        var actual = await _vehicleService.GetAllBusesLinesNamesAsync();

        // Assert

    }
}

public class MyHttpRequestFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient();
    }
}