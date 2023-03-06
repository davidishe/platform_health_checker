using Checker.Infrastructure.Worker;
using Infrastructure.Services.TelegramService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
      services.AddHostedService<Worker>();
      services.AddSingleton<ITelegramService, TelegramService>();
      // services.AddSingleton<IRestResponseService, RestResponseService>();

    })
    .Build();

await host.RunAsync();







