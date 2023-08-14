using Microsoft.AspNetCore.SignalR;

namespace SignalR.API.Hubs;
public class MyHub : Hub
{
    public static List<string> Names { get; set; } = new();
    private static int ClientCount { get; set; } = 0;
    public static int TeamCount { get; set; } = 7;
    public async Task SendMessageAsync(string name)
    {
        Names.Add(name);
        await Clients.All.SendAsync("ReceiveMessage", name);
    }
    public async Task GetMessageAsync()
    {
        await Clients.All.SendAsync("ReceiveNames", Names);
    }
    public async Task SendName(string name)
    {
        if (Names.Count >= TeamCount)
            await Clients.All.SendAsync("Error", $"takım en fazla {TeamCount} olabilir.");
        else
        {
            Names.Add(name);
            await Clients.All.SendAsync("ReceiveName}", name);
        }
    }
    public async override Task OnConnectedAsync()
    {
        ClientCount++;
        await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        await base.OnConnectedAsync();
    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        ClientCount--;
        await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        await base.OnDisconnectedAsync(exception);
    }
}