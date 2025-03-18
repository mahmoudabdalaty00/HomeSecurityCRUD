using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Date;
using Server.Models.Entities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Security;
using Server.Models.Dtos;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DevicesController(ApplicationDbContext context)
    {
        _context = context;
    }

 
    // Gets all devices for the authenticated user.    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _context.Devices
            .Where(d => d.House.UserId == userId)
            .ToListAsync();
    }

   
    // Gets a specific device by ID.   
    [HttpGet("{id}")]
    public async Task<ActionResult<Device>> GetDevice(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var device = await _context.Devices
            .Include(d => d.House)
            .FirstOrDefaultAsync(d => d.DeviceId == id && d.House.UserId == userId);

        if (device == null) return NotFound();
        return device;
    }

 
    // Adds a new device to a house.
    [HttpPost]
    public async Task<ActionResult<Device>> AddDevice(Device device)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var house = await _context.Houses.FindAsync(device.HouseId);

        if (house == null || house.UserId != userId) return Unauthorized();

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDevice), new { id = device.DeviceId }, device);
    }

 
    // Updates an existing device.    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDevice(int id, UpdateDeviceDto deviceUpdate)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var device = await _context.Devices
            .Include(d => d.House)
            .FirstOrDefaultAsync(d => d.DeviceId == id && d.House.UserId == userId);

        if (device == null) return NotFound();

        device.DeviceName = deviceUpdate.DeviceName;
        device.Status = deviceUpdate.Status;
        device.DeviceType = deviceUpdate.DeviceType;

        await _context.SaveChangesAsync();

        return NoContent();
    }
 

    // Deletes a device. 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var device = await _context.Devices
            .Include(d => d.House)
            .FirstOrDefaultAsync(d => d.DeviceId == id && d.House.UserId == userId);

        if (device == null) return NotFound();

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


