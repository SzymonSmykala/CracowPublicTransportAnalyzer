namespace PublicTransportCrawler;

public static class Constants
{
    private static string TramsBaseEndpoint => "http://www.ttss.krakow.pl";
    private static string BusesBaseEndpoint => "http://91.223.13.70";
    
    public static string TramsEndpoint => $"{TramsBaseEndpoint}/internetservice/geoserviceDispatcher/services/vehicleinfo/vehicles?positionType=CORRECTED";
    public static string BusesEndpoint => $"{BusesBaseEndpoint}/internetservice/geoserviceDispatcher/services/vehicleinfo/vehicles?positionType=CORRECTED";

    public static string BusesPathEndpoint =>
        $"{BusesBaseEndpoint}/internetservice/services/tripInfo/tripPassages?tripId=";
    public static string RondoGrunwaldzkieBusesInfo =>
        $"{BusesBaseEndpoint}/internetservice/services/passageInfo/stopPassages/stop?stop=3338";    
    public static string BusesInfoEndpoint => $"{BusesBaseEndpoint}/internetservice/services/passageInfo/stopPassages/stop?stop=";
}