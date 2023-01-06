using System.Collections.Generic;

namespace PublicTransportCrawler.Vehicles;

public class CrawlingContext
{
    public List<DTO.Vehicle> Vehicles { get; set; }
    public string LineNumber { get; set; }
}