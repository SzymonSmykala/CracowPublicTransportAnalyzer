using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PublicTransportCrawler.Stops.DTO;

namespace PublicTransportCrawler.Stops;

class StopService : IStopService
{
    private readonly HttpClient _httpClient;

    public StopService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<Actual>> GetRondoGrunwaldzkieDataAsync()
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(Constants.RondoGrunwaldzkieBusesInfo),
            Method = HttpMethod.Get
        };
        var response = await _httpClient.SendAsync(request);
        // var responseAsString = await response.Content.ReadAsStringAsync();
        var responseAsString = "{\n  \"actual\": [\n    {\n      \"actualRelativeTime\": -113,\n      \"direction\": \"Nowy Bieżanów Południe\",\n      \"mixedTime\": \"15:09\",\n      \"passageid\": \"-1152921503529674060\",\n      \"patternText\": \"503\",\n      \"plannedTime\": \"15:09\",\n      \"routeId\": \"8095257447305838728\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191549196\"\n    },\n    {\n      \"actualRelativeTime\": 7,\n      \"actualTime\": \"15:11\",\n      \"direction\": \"Kraków Airport\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529666986\",\n      \"patternText\": \"300\",\n      \"plannedTime\": \"15:09\",\n      \"routeId\": \"8095257447305838757\",\n      \"status\": \"STOPPING\",\n      \"tripId\": \"8095261304191368976\",\n      \"vehicleId\": \"-1152921492936757053\"\n    },\n    {\n      \"actualRelativeTime\": 7,\n      \"actualTime\": \"15:11\",\n      \"direction\": \"Górka Narodowa\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529671950\",\n      \"patternText\": \"164\",\n      \"plannedTime\": \"15:10\",\n      \"routeId\": \"8095257447305839188\",\n      \"status\": \"STOPPING\",\n      \"tripId\": \"8095261304190811918\",\n      \"vehicleId\": \"-1152921492936757536\"\n    },\n    {\n      \"actualRelativeTime\": 7,\n      \"actualTime\": \"15:11\",\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529676084\",\n      \"patternText\": \"300\",\n      \"plannedTime\": \"15:09\",\n      \"routeId\": \"8095257447305838757\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191360783\",\n      \"vehicleId\": \"-1152921492936757881\"\n    },\n    {\n      \"actualRelativeTime\": 67,\n      \"actualTime\": \"15:12\",\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"1 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529676776\",\n      \"patternText\": \"112\",\n      \"plannedTime\": \"15:12\",\n      \"routeId\": \"8095257447305838603\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190447377\",\n      \"vehicleId\": \"-1152921492936757155\"\n    },\n    {\n      \"actualRelativeTime\": 67,\n      \"direction\": \"Górka Narodowa Wschód\",\n      \"mixedTime\": \"15:12\",\n      \"passageid\": \"-1152921503529672716\",\n      \"patternText\": \"503\",\n      \"plannedTime\": \"15:12\",\n      \"routeId\": \"8095257447305838728\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191536908\"\n    },\n    {\n      \"actualRelativeTime\": 127,\n      \"actualTime\": \"15:13\",\n      \"direction\": \"Jeziorzany Pętla\",\n      \"mixedTime\": \"2 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529663331\",\n      \"patternText\": \"219\",\n      \"plannedTime\": \"15:12\",\n      \"routeId\": \"8095257447305839105\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191139597\",\n      \"vehicleId\": \"-1152921492936757556\"\n    },\n    {\n      \"actualRelativeTime\": 127,\n      \"actualTime\": \"15:13\",\n      \"direction\": \"Kraków Airport\",\n      \"mixedTime\": \"2 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529666985\",\n      \"patternText\": \"300\",\n      \"plannedTime\": \"15:11\",\n      \"routeId\": \"8095257447305838757\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191368976\",\n      \"vehicleId\": \"-1152921492936757053\"\n    },\n    {\n      \"actualRelativeTime\": 247,\n      \"actualTime\": \"15:15\",\n      \"direction\": \"Jeziorzany Pętla\",\n      \"mixedTime\": \"4 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529663330\",\n      \"patternText\": \"219\",\n      \"plannedTime\": \"15:14\",\n      \"routeId\": \"8095257447305839105\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191139597\",\n      \"vehicleId\": \"-1152921492936757556\"\n    },\n    {\n      \"actualRelativeTime\": 307,\n      \"actualTime\": \"15:16\",\n      \"direction\": \"Os. Kurdwanów\",\n      \"mixedTime\": \"5 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529670895\",\n      \"patternText\": \"179\",\n      \"plannedTime\": \"15:15\",\n      \"routeId\": \"8095257447305839198\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190971669\",\n      \"vehicleId\": \"-1152921492936757758\"\n    },\n    {\n      \"actualRelativeTime\": 427,\n      \"actualTime\": \"15:18\",\n      \"direction\": \"Krowodrza Górka\",\n      \"mixedTime\": \"7 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529672656\",\n      \"patternText\": \"194\",\n      \"plannedTime\": \"15:17\",\n      \"routeId\": \"8095257447305839204\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191065868\",\n      \"vehicleId\": \"-1152921492936757181\"\n    },\n    {\n      \"actualRelativeTime\": 487,\n      \"actualTime\": \"15:19\",\n      \"direction\": \"Dworzec Główny Zachód\",\n      \"mixedTime\": \"8 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529672002\",\n      \"patternText\": \"179\",\n      \"plannedTime\": \"15:16\",\n      \"routeId\": \"8095257447305839198\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190983957\",\n      \"vehicleId\": \"-1152921492936757766\"\n    },\n    {\n      \"actualRelativeTime\": 487,\n      \"actualTime\": \"15:19\",\n      \"direction\": \"Rżąka\",\n      \"mixedTime\": \"8 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529675418\",\n      \"patternText\": \"144\",\n      \"plannedTime\": \"15:13\",\n      \"routeId\": \"8095257447305839179\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190689031\",\n      \"vehicleId\": \"-1152921492936757740\"\n    },\n    {\n      \"actualRelativeTime\": 487,\n      \"direction\": \"Ugorek\",\n      \"mixedTime\": \"15:19\",\n      \"passageid\": \"-1152921503529662331\",\n      \"patternText\": \"424\",\n      \"plannedTime\": \"15:19\",\n      \"routeId\": \"8095257447305838722\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191450894\"\n    },\n    {\n      \"actualRelativeTime\": 547,\n      \"actualTime\": \"15:20\",\n      \"direction\": \"Krowodrza Górka\",\n      \"mixedTime\": \"9 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529672655\",\n      \"patternText\": \"194\",\n      \"plannedTime\": \"15:19\",\n      \"routeId\": \"8095257447305839204\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191065868\",\n      \"vehicleId\": \"-1152921492936757181\"\n    },\n    {\n      \"actualRelativeTime\": 607,\n      \"direction\": \"Ugorek\",\n      \"mixedTime\": \"15:21\",\n      \"passageid\": \"-1152921503529662330\",\n      \"patternText\": \"424\",\n      \"plannedTime\": \"15:21\",\n      \"routeId\": \"8095257447305838722\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191450894\"\n    },\n    {\n      \"actualRelativeTime\": 607,\n      \"actualTime\": \"15:21\",\n      \"direction\": \"Azory\",\n      \"mixedTime\": \"10 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529670175\",\n      \"patternText\": \"173\",\n      \"plannedTime\": \"15:21\",\n      \"routeId\": \"8095257447305839194\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190914312\",\n      \"vehicleId\": \"-1152921492936757750\"\n    },\n    {\n      \"actualRelativeTime\": 667,\n      \"actualTime\": \"15:22\",\n      \"direction\": \"Piaski Wielkie\",\n      \"mixedTime\": \"11 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529668793\",\n      \"patternText\": \"469\",\n      \"plannedTime\": \"15:21\",\n      \"routeId\": \"8095257447305838791\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190852871\",\n      \"vehicleId\": \"-1152921492936757141\"\n    },\n    {\n      \"actualRelativeTime\": 667,\n      \"actualTime\": \"15:22\",\n      \"direction\": \"Nowy Bieżanów Południe\",\n      \"mixedTime\": \"11 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529669604\",\n      \"patternText\": \"173\",\n      \"plannedTime\": \"15:20\",\n      \"routeId\": \"8095257447305839194\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190902023\",\n      \"vehicleId\": \"-1152921492936757746\"\n    },\n    {\n      \"actualRelativeTime\": 667,\n      \"actualTime\": \"15:22\",\n      \"direction\": \"Górka Narodowa\",\n      \"mixedTime\": \"11 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529670541\",\n      \"patternText\": \"169\",\n      \"plannedTime\": \"15:22\",\n      \"routeId\": \"8095257447305839191\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190865162\",\n      \"vehicleId\": \"-1152921492936757129\"\n    },\n    {\n      \"actualRelativeTime\": 787,\n      \"actualTime\": \"15:24\",\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"13 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529671321\",\n      \"patternText\": \"124\",\n      \"plannedTime\": \"15:23\",\n      \"routeId\": \"8095257447305838610\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190500622\",\n      \"vehicleId\": \"-1152921492936757125\"\n    },\n    {\n      \"actualRelativeTime\": 847,\n      \"actualTime\": \"15:25\",\n      \"direction\": \"Pod Fortem\",\n      \"mixedTime\": \"14 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529669452\",\n      \"patternText\": \"194\",\n      \"plannedTime\": \"15:24\",\n      \"routeId\": \"8095257447305839204\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191053575\",\n      \"vehicleId\": \"-1152921492936757247\"\n    },\n    {\n      \"actualRelativeTime\": 967,\n      \"direction\": \"Będkowice Pętla\",\n      \"mixedTime\": \"15:27\",\n      \"passageid\": \"-1152921503529657990\",\n      \"patternText\": \"310\",\n      \"plannedTime\": \"15:27\",\n      \"routeId\": \"8095257447305838758\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191426316\"\n    },\n    {\n      \"actualRelativeTime\": 1027,\n      \"actualTime\": \"15:28\",\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"17 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529661895\",\n      \"patternText\": \"101\",\n      \"plannedTime\": \"15:28\",\n      \"routeId\": \"8095257447305838594\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190377765\",\n      \"vehicleId\": \"-1152921492936757532\"\n    },\n    {\n      \"actualRelativeTime\": 1087,\n      \"direction\": \"Będkowice Pętla\",\n      \"mixedTime\": \"15:29\",\n      \"passageid\": \"-1152921503529657989\",\n      \"patternText\": \"310\",\n      \"plannedTime\": \"15:29\",\n      \"routeId\": \"8095257447305838758\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191426316\"\n    },\n    {\n      \"actualRelativeTime\": 1087,\n      \"actualTime\": \"15:29\",\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"18 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529673148\",\n      \"patternText\": \"307\",\n      \"plannedTime\": \"15:29\",\n      \"routeId\": \"8095257447305838653\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191409933\",\n      \"vehicleId\": \"-1152921492936757582\"\n    },\n    {\n      \"actualRelativeTime\": 1147,\n      \"actualTime\": \"15:30\",\n      \"direction\": \"Michałowice P+R\",\n      \"mixedTime\": \"19 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529658305\",\n      \"patternText\": \"307\",\n      \"plannedTime\": \"15:30\",\n      \"routeId\": \"8095257447305838653\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191422223\",\n      \"vehicleId\": \"-1152921492936757428\"\n    },\n    {\n      \"actualRelativeTime\": 1147,\n      \"actualTime\": \"15:30\",\n      \"direction\": \"Górka Narodowa\",\n      \"mixedTime\": \"19 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529663683\",\n      \"patternText\": \"164\",\n      \"plannedTime\": \"15:30\",\n      \"routeId\": \"8095257447305839188\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190816014\",\n      \"vehicleId\": \"-1152921492936757538\"\n    },\n    {\n      \"actualRelativeTime\": 1147,\n      \"actualTime\": \"15:30\",\n      \"direction\": \"Nowy Bieżanów Południe\",\n      \"mixedTime\": \"19 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529664866\",\n      \"patternText\": \"503\",\n      \"plannedTime\": \"15:29\",\n      \"routeId\": \"8095257447305838728\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191528716\",\n      \"vehicleId\": \"-1152921492936757818\"\n    },\n    {\n      \"actualRelativeTime\": 1147,\n      \"actualTime\": \"15:30\",\n      \"direction\": \"Piaski Nowe\",\n      \"mixedTime\": \"19 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529669879\",\n      \"patternText\": \"164\",\n      \"plannedTime\": \"15:27\",\n      \"routeId\": \"8095257447305839188\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190824205\",\n      \"vehicleId\": \"-1152921492936757402\"\n    },\n    {\n      \"actualRelativeTime\": 1207,\n      \"actualTime\": \"15:31\",\n      \"direction\": \"Prądnik Biały\",\n      \"mixedTime\": \"20 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529667057\",\n      \"patternText\": \"144\",\n      \"plannedTime\": \"15:29\",\n      \"routeId\": \"8095257447305839179\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190672658\",\n      \"vehicleId\": \"-1152921492936757730\"\n    },\n    {\n      \"actualRelativeTime\": 1267,\n      \"actualTime\": \"15:32\",\n      \"direction\": \"Michałowice P+R\",\n      \"mixedTime\": \"21 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529658304\",\n      \"patternText\": \"307\",\n      \"plannedTime\": \"15:32\",\n      \"routeId\": \"8095257447305838653\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304191422223\",\n      \"vehicleId\": \"-1152921492936757428\"\n    },\n    {\n      \"actualRelativeTime\": 1267,\n      \"actualTime\": \"15:32\",\n      \"direction\": \"Tyniec Kamieniołom\",\n      \"mixedTime\": \"21 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529656760\",\n      \"patternText\": \"112\",\n      \"plannedTime\": \"15:32\",\n      \"routeId\": \"8095257447305838603\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190447378\",\n      \"vehicleId\": \"-1152921492936757155\"\n    },\n    {\n      \"actualRelativeTime\": 1267,\n      \"direction\": \"Os. Podwawelskie\",\n      \"mixedTime\": \"15:32\",\n      \"passageid\": \"-1152921503529663327\",\n      \"patternText\": \"162\",\n      \"plannedTime\": \"15:32\",\n      \"routeId\": \"8095257447305839186\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304190795537\"\n    },\n    {\n      \"actualRelativeTime\": 1267,\n      \"direction\": \"Górka Narodowa Wschód\",\n      \"mixedTime\": \"15:32\",\n      \"passageid\": \"-1152921503529664609\",\n      \"patternText\": \"503\",\n      \"plannedTime\": \"15:32\",\n      \"routeId\": \"8095257447305838728\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304191541004\"\n    },\n    {\n      \"actualRelativeTime\": 1327,\n      \"direction\": \"Rżąka\",\n      \"mixedTime\": \"15:33\",\n      \"passageid\": \"-1152921503529663326\",\n      \"patternText\": \"144\",\n      \"plannedTime\": \"15:33\",\n      \"routeId\": \"8095257447305839179\",\n      \"status\": \"PLANNED\",\n      \"tripId\": \"8095261304190693131\"\n    },\n    {\n      \"actualRelativeTime\": 1387,\n      \"actualTime\": \"15:34\",\n      \"direction\": \"Tyniec Kamieniołom\",\n      \"mixedTime\": \"23 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529656759\",\n      \"patternText\": \"112\",\n      \"plannedTime\": \"15:34\",\n      \"routeId\": \"8095257447305838603\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190447378\",\n      \"vehicleId\": \"-1152921492936757155\"\n    },\n    {\n      \"actualRelativeTime\": 1447,\n      \"actualTime\": \"15:35\",\n      \"direction\": \"Os. Kurdwanów\",\n      \"mixedTime\": \"24 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529661611\",\n      \"patternText\": \"179\",\n      \"plannedTime\": \"15:35\",\n      \"routeId\": \"8095257447305839198\",\n      \"status\": \"PREDICTED\",\n      \"tripId\": \"8095261304190975768\",\n      \"vehicleId\": \"-1152921492936757762\"\n    }\n  ],\n  \"directions\": [],\n  \"firstPassageTime\": 1668346853000,\n  \"generalAlerts\": [],\n  \"lastPassageTime\": 1668354300000,\n  \"old\": [\n    {\n      \"actualRelativeTime\": -353,\n      \"direction\": \"Górka Narodowa\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529676432\",\n      \"patternText\": \"169\",\n      \"plannedTime\": \"15:02\",\n      \"routeId\": \"8095257447305839191\",\n      \"status\": \"DEPARTED\",\n      \"tripId\": \"8095261304190861066\",\n      \"vehicleId\": \"-1152921492936757127\"\n    },\n    {\n      \"actualRelativeTime\": -113,\n      \"direction\": \"Piaski Nowe\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529677669\",\n      \"patternText\": \"164\",\n      \"plannedTime\": \"15:07\",\n      \"routeId\": \"8095257447305839188\",\n      \"status\": \"DEPARTED\",\n      \"tripId\": \"8095261304190820105\",\n      \"vehicleId\": \"-1152921492936757368\"\n    },\n    {\n      \"actualRelativeTime\": -53,\n      \"direction\": \"Prądnik Biały\",\n      \"mixedTime\": \"0 %UNIT_MIN%\",\n      \"passageid\": \"-1152921503529676123\",\n      \"patternText\": \"144\",\n      \"plannedTime\": \"15:09\",\n      \"routeId\": \"8095257447305839179\",\n      \"status\": \"DEPARTED\",\n      \"tripId\": \"8095261304190701329\",\n      \"vehicleId\": \"-1152921492936757798\"\n    }\n  ],\n  \"routes\": [\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Kopiec Kościuszki\",\n        \"Os. Podwawelskie\"\n      ],\n      \"id\": \"8095257447305838594\",\n      \"name\": \"101\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"101\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Tyniec Kamieniołom\",\n        \"Os. Podwawelskie\"\n      ],\n      \"id\": \"8095257447305838603\",\n      \"name\": \"112\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"112\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"TAURON Arena Kraków Wieczysta\",\n        \"Os. Podwawelskie\"\n      ],\n      \"id\": \"8095257447305838610\",\n      \"name\": \"124\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"124\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Prądnik Biały\",\n        \"Rżąka\"\n      ],\n      \"id\": \"8095257447305839179\",\n      \"name\": \"144\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"144\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Podgórki Tynieckie\",\n        \"Os. Podwawelskie\"\n      ],\n      \"id\": \"8095257447305839186\",\n      \"name\": \"162\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"162\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Górka Narodowa\",\n        \"Piaski Nowe\"\n      ],\n      \"id\": \"8095257447305839188\",\n      \"name\": \"164\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"164\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Zajezdnia Wola Duchacka\",\n        \"Górka Narodowa\"\n      ],\n      \"id\": \"8095257447305839191\",\n      \"name\": \"169\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"169\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Azory\",\n        \"Nowy Bieżanów Południe\"\n      ],\n      \"id\": \"8095257447305839194\",\n      \"name\": \"173\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"173\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Os. Kurdwanów\",\n        \"Dworzec Główny Zachód\"\n      ],\n      \"id\": \"8095257447305839198\",\n      \"name\": \"179\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"179\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Krowodrza Górka\",\n        \"Pod Fortem\"\n      ],\n      \"id\": \"8095257447305839204\",\n      \"name\": \"194\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"194\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Os. Podwawelskie\",\n        \"Jeziorzany Pętla\"\n      ],\n      \"id\": \"8095257447305839105\",\n      \"name\": \"219\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"219\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Kraków Airport\",\n        \"Os. Podwawelskie\"\n      ],\n      \"id\": \"8095257447305838757\",\n      \"name\": \"300\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"300\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Os. Podwawelskie\",\n        \"Michałowice P+R\"\n      ],\n      \"id\": \"8095257447305838653\",\n      \"name\": \"307\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"307\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Os. Podwawelskie\",\n        \"Będkowice Pętla\"\n      ],\n      \"id\": \"8095257447305838758\",\n      \"name\": \"310\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"310\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Os. Podwawelskie\",\n        \"Ugorek\"\n      ],\n      \"id\": \"8095257447305838722\",\n      \"name\": \"424\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"424\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Piaski Wielkie\",\n        \"Piaski Wielkie\"\n      ],\n      \"id\": \"8095257447305838791\",\n      \"name\": \"469\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"469\"\n    },\n    {\n      \"alerts\": [],\n      \"authority\": \"MPKBUS\",\n      \"directions\": [\n        \"Górka Narodowa Wschód\",\n        \"Nowy Bieżanów Południe\"\n      ],\n      \"id\": \"8095257447305838728\",\n      \"name\": \"503\",\n      \"routeType\": \"bus\",\n      \"shortName\": \"503\"\n    }\n  ],\n  \"stopName\": \"Rondo Grunwaldzkie\",\n  \"stopShortName\": \"3338\"\n}";
        var converted = StopResponse.FromJson(responseAsString);
        return converted.Actual;
    }
}