namespace PublicTransportCrawler;

public static class Constants
{
    public static string TramsEndpoint => "http://www.ttss.krakow.pl/internetservice/geoserviceDispatcher/services/vehicleinfo/vehicles?positionType=CORRECTED";
    public static string RondoGrunwaldzkieBusesInfo =>
        "http://91.223.13.70/internetservice/services/passageInfo/stopPassages/stop?stop=3338";
}