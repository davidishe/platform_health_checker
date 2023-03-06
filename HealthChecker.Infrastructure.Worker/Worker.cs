

using Infrastructure.Services.TelegramService;

namespace Checker.Infrastructure.Worker;

public class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;
  private readonly ITelegramService _telegramService;
  public Worker(
    ILogger<Worker> logger,
    ITelegramService telegramService
    )
  {
    _logger = logger;
    _telegramService = telegramService;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      var healthCheckStatus = await GetHealthCheckStatus();
      if (healthCheckStatus)
        _telegramService.SendMessage("-1001834328957", "Все хорошо! Сервис работает.");

      if (!healthCheckStatus)
        _telegramService.SendMessage("-1001834328957", "Все плохо! Сервис не работает.");

      var second = 1000; // 1000 = 1sec; 
      await Task.Delay(second * 60 * 60, stoppingToken);
    }
  }



  // показывает будущие матчи
  private static async Task<bool> GetHealthCheckStatus()
  {

    var client = new HttpClient();
    var request = new HttpRequestMessage
    {
      Method = HttpMethod.Get,
      RequestUri = new Uri("https://coffee.telecost.pro/api/status"),
    };


    using (var response = await client.SendAsync(request))
    {
      try
      {
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        if (int.Parse(body) == 200)
          return true;
      }
      catch (System.Exception ex)
      {

        return false;
        throw;
      }

      return false;


    }




  }


}



// public class Worker : BackgroundService
// {
//   private readonly ILogger<Worker> _logger;

//   private readonly IRestResponseService _npdService;

//   private readonly IDbRepository<SmeClient> _clientsRepo;



//   public Worker(
//     ILogger<Worker> logger,
//     IDbRepository<SmeClient> clientsRepo,
//     IRestResponseService npdService
//     )
//   {
//     _logger = logger;
//     _npdService = npdService;
//     _clientsRepo = clientsRepo;
//   }

//   protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//   {
//     while (!stoppingToken.IsCancellationRequested)
//     {
//       _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

//       var client = _clientsRepo.GetAll().Where(z => z.IsNpdTax == false).FirstOrDefault();
//       // 400800570836
//       await GetHealthCheckStatus(client.Inn);
//       var second = 1000; // 1000 = 1sec; 
//       await Task.Delay(second * 32, stoppingToken);
//     }
//   }



//   // показывает будущие матчи
//   private async Task GetHealthCheckStatus(string inn)
//   {

//     // var entitys = _
//     await _npdService.GetStatus(inn);

//   }


// }
