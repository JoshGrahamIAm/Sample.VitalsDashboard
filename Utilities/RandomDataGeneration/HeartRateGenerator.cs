// HeartRateGenerator.cs
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class HeartRateGenerator : BackgroundService
{
    private readonly IHubContext<HeartRateHub> _hubContext;
    private readonly Random _random;

    public HeartRateGenerator(IHubContext<HeartRateHub> hubContext)
    {
        _hubContext = hubContext;
        _random = new Random();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Generate a random heart rate between 60 and 100.
            var heartRate = _random.Next(60, 101);

      // Generate a random O2 sat rate between 70 and 98.
      var satRate = _random.Next(60, 101);


      // Send the heart rate data to the hub.
      await _hubContext.Clients.All.SendAsync("ReceiveHeartRateUpdate", DateTime.Now, heartRate, satRate);

            // Wait for 5 seconds before generating the next heart rate.
            await Task.Delay(5000);
        }
    }
}
