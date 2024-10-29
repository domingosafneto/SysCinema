using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Cinema.Models;
using WebApi_Cinema.Services;

namespace WebApi_Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetAllFilmes()
        {
            var filmes = await _filmeService.GetAllFilmesAsync();
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilmeById(int id)
        {
            var filme = await _filmeService.GetFilmeByIdAsync(id);
            if (filme == null)
                return NotFound();

            return Ok(filme);
        }

        [HttpPost]
        public async Task<ActionResult<Filme>> AddFilme(Filme filme)
        {
            var createdFilme = await _filmeService.AddFilmeAsync(filme);
            return CreatedAtAction(nameof(GetFilmeById), new { id = createdFilme.IdFilme }, createdFilme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilme(int id, Filme updatedFilme)
        {
            var result = await _filmeService.UpdateFilmeAsync(id, updatedFilme);
            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var isDeleted = await _filmeService.DeleteFilmeAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
