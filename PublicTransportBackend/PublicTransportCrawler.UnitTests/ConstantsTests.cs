using FluentAssertions;

namespace PublicTransportCrawler.UnitTests;

[TestFixture]
public class ConstantsTests
{
    [Test]
    public void ShouldHaveProperConfiguration()
    {
        // Assert
        Constants.TramsEndpoint.Should()
            .Be(
                "http://www.ttss.krakow.pl/internetservice/geoserviceDispatcher/services/vehicleinfo/vehicles?positionType=CORRECTED");
    }
}