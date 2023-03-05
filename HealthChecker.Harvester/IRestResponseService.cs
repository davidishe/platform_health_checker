using RestSharp;
using HealthChecker.Harvester;

public interface IRestResponseService
{
  Task<object> GetStatus();

}