using Microsoft.AspNetCore.Mvc;
using WebApi_Cinema.Models;
using WebApi_Cinema.Services;

namespace WebApi_Cinema.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly SalaService _salaService;

        public SalaController(SalaService salaService)
        {
            _salaService = salaService;
        }

        // GET: api/Sala
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            var salas = await _salaService.GetSalasAsync();
            return Ok(salas);
        }

        // GET: api/Sala/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _salaService.GetSalaByIdAsync(id);
            if (sala == null) return NotFound();
            return Ok(sala);
        }

        // POST: api/Sala
        [HttpPost]
        public async Task<ActionResult<Sala>> CreateSala(Sala sala)
        {
            await _salaService.CreateSalaAsync(sala);
            return CreatedAtAction(nameof(GetSala), new { id = sala.IdSala }, sala);
        }

        // PUT: api/Sala/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSala(int id, Sala sala)
        {
            if (id != sala.IdSala) return BadRequest();

            await _salaService.UpdateSalaAsync(sala);
            return NoContent();
        }

        // DELETE: api/Sala/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            await _salaService.DeleteSalaAsync(id);
            return NoContent();
        }

    }
}
