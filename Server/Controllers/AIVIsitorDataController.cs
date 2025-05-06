using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DTOs.AIVIsitorDataDTO;
using Server.Repo.interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIVIsitorDataController : ControllerBase
    {
        private readonly IAIVIsitorDataRepository _aIVIsitor;

        public AIVIsitorDataController(IAIVIsitorDataRepository aIVIsitor)
        {
            _aIVIsitor = aIVIsitor;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var Visitors = await _aIVIsitor.GetAllAsync();

                if (Visitors == null)
                {
                    return NotFound("No products found");
                }

                return Ok(Visitors);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var visitor = await _aIVIsitor.GetByIdAsync(id);
                if (visitor == null)
                {
                    return NotFound("No product found");
                }
                return Ok(visitor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(CreateAiVIsitorDataDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest("Invalid data");
                }
                var result = await _aIVIsitor.AddAsync(entity);
                if (result)
                {
                    return Ok("Visitor added successfully");
                }
                else
                {
                    return BadRequest("Failed to add visitor");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
