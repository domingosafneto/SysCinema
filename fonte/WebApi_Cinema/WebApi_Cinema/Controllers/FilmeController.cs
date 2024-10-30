using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Cinema.Models;
using WebApi_Cinema.Services;

namespace WebApi_Cinema.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
            var filmes = await _filmeService.GetFilmesAsync();
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _filmeService.GetFilmeByIdAsync(id);
            if (filme == null) return NotFound();
            return Ok(filme);
        }

        [HttpPost]
        public async Task<ActionResult<Filme>> CreateFilme(Filme filme)
        {
            await _filmeService.CreateFilmeAsync(filme);
            return CreatedAtAction(nameof(GetFilme), new { id = filme.IdFilme }, filme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilme(int id, Filme filme)
        {
            if (id != filme.IdFilme) return BadRequest();

            await _filmeService.UpdateFilmeAsync(filme);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            await _filmeService.DeleteFilmeAsync(id);
            return NoContent();
        }
    }

}
