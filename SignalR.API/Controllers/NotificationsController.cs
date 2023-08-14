using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.API.Hubs;

namespace SignalR.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IHubContext<MyHub> _hubContext;

    public NotificationsController(IHubContext<MyHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet("setTeamCount")]
    public async Task<IActionResult> SetTeamCount(int teamCount)
    {
        MyHub.TeamCount = teamCount;
        await _hubContext.Clients.All.SendAsync("Notify", $"arkadaşlar takım {teamCount} kişi olacaktır.");
        return NoContent();
    }
}