using Microsoft.AspNetCore.Mvc;
using Server.Date;
using Server.Models.Dtos;
using Server.Models.Entities;
using System.Security;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllhistory()
        {
            var history = _context.Histories.ToList();
            
            return Ok(history);
        }

        // here we skip Getbyid method & Put  beacause it is history 
        //were we can't update 
        [HttpPost]
        public IActionResult AddHistory(AddHistoryDto historyDto)
        {

            var history = new History
            {
                ImageUrl = historyDto.ImageUrl,
                Date = historyDto.Date
            };
            _context.Histories.Add(history);
            _context.SaveChanges();
            return Ok(history);

        }

        [HttpDelete()]
        public IActionResult DeleteHistory()
        {
            // Calculate the date one week ago
            var oneWeekAgo = DateTime.Now.AddDays(-7);

            // Retrieve and filter history records older than one week
            var historiesToRemove = _context.Histories
                .Where(h => h.Date < oneWeekAgo)
                .ToList();

            // Remove the filtered records
            _context.Histories.RemoveRange(historiesToRemove);

            // Save changes to the database
            _context.SaveChanges();

            return Ok();
        }

    }
}

 