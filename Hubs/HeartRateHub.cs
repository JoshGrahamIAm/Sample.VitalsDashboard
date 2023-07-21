// HeartRateHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class HeartRateHub : Hub
{
    // Define a method that clients can call to receive heart rate updates.
    public async Task ReceiveHeartRateUpdate(DateTime date, int rate, int oxygen)
    {
        // Broadcast the heart rate update to all connected clients.
        await Clients.All.SendAsync("ReceiveHeartRateUpdate", date, rate, oxygen);
    }
}
