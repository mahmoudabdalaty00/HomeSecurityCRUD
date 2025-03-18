using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Date;
using Server.Models.Entities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public HousesController(ApplicationDbContext context) { _context = context; }

   
    // Gets all houses for the authenticated user.
 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<House>>> GetHouses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _context.Houses.Where(h => h.UserId == userId).ToListAsync();
    }


    // get a house details by id
    [HttpGet("{id}")]
    public async Task<ActionResult<House>> GetHouse(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var house = await _context.Houses.FirstOrDefaultAsync(h => h.HouseId == id && h.UserId == userId);

        if (house == null) return NotFound();
        return house;
    }
    
    // Adds a new house.  
    [HttpPost]
    public async Task<ActionResult<House>> AddHouse(House house)
    {
        house.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _context.Houses.Add(house);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetHouses), new { id = house.HouseId }, house);
    }

    //update house details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHouse(int id, House houseUpdate)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var house = await _context.Houses.FindAsync(id);

        if (house == null || house.UserId != userId) return NotFound();

        house.Name = houseUpdate.Name;
        house.Address = houseUpdate.Address;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // Deletes a house by id.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHouse(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var house = await _context.Houses.FindAsync(id);

        if (house == null || house.UserId != userId) return NotFound();

        _context.Houses.Remove(house);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}


