using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Date;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;
#region Devices Controller
#endregion

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AlertsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AlertsController(ApplicationDbContext context) { _context = context; }

    /// <summary>
    /// Retrieves alerts related to user devices.
    /// </summary>
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Alarm>>> GetAlerts()
    //{
    //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //    return await _context.Alarms.Where(a => a.AlarmId == userId).ToListAsync();
    //
    
}
 

