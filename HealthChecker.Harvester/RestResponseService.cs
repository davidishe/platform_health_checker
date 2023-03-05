using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;


namespace HealthChecker.Harvester

{



  public class RestResponseService : IRestResponseService
  {


    // показывает будущие матчи
    public async Task<object> GetStatus()
    {

      var client = new RestClient("https://coffee.telecost.pro/api/status");
      var request = new RestRequest();
      request.Method = Method.Get;
      request.AddHeader("Content-Type", "application/json");
      var response = await client.ExecuteAsync(request);
      Console.WriteLine(response.Content);
      if (response.Content == null)
        return new ApiResponse()
        {
          Message = "Запрос не прошел"
        };

      var res = JsonConvert.DeserializeObject(response.Content);
      return res;

    }

  }

}