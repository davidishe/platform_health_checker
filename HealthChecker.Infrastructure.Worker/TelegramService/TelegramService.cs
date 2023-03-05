using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.TelegramService
{
  public class TelegramService : ITelegramService
  {

    private readonly string _telegramToken;
    ILogger<TelegramService> _logger;


    public TelegramService(
      IConfiguration config,
      ILogger<TelegramService> logger
    )
    {
      _telegramToken = config.GetSection("BotSettings:TelegramToken").Value;
      _logger = logger;
    }



    public string SendMessage(string destID, string message)
    {
      try
      {
        string urlString = $"https://api.telegram.org/bot{_telegramToken}/sendMessage?chat_id={destID}&text={message}";
        WebClient webclient = new WebClient();
        var result = webclient.DownloadString(urlString);
        return result;
      }
      catch (Exception ex)
      {
        _logger.LogError(Guid.NewGuid().ToString(), $"Произошла ошибка: {ex}");
        return "Произошла ошибка";
      }

    }



  }
}