using System.Threading.Tasks;


namespace Infrastructure.Services.TelegramService
{
  public interface ITelegramService
  {
    string SendMessage(string destID, string message);

  }
}