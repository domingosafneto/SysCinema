using Microsoft.AspNetCore.Mvc;
using WebApi_Cinema.Models;
using WebApi_Cinema.Services;

namespace WebApi_Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly SalaService _salaService;

        public SalaController(SalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetAllSalas()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return Ok(salas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSalaById(int id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null)
                return NotFound();

            return Ok(sala);
        }

        [HttpPost]
        public async Task<ActionResult<Sala>> AddSala(Sala sala)
        {
            var createdSala = await _salaService.AddSalaAsync(sala);
            return CreatedAtAction(nameof(GetSalaById), new { id = createdSala.IdSala }, createdSala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSala(int id, Sala updatedSala)
        {
            var result = await _salaService.UpdateSalaAsync(id, updatedSala);
            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var isDeleted = await _salaService.DeleteSalaAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
