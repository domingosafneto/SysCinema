using Microsoft.AspNetCore.Mvc;
using WebApi_Cinema.Models;
using WebApi_Cinema.Services;

namespace WebApi_Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GerenciamentoController : ControllerBase
    {
        private readonly GerenciamentoService _gerenciamentoService;

        public GerenciamentoController(GerenciamentoService gerenciamentoService)
        {
            _gerenciamentoService = gerenciamentoService;
        }

        
        [HttpGet("filme/{filmeId}/salas")]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalasByFilmeId(int idFilme)
        {
            var salas = await _gerenciamentoService.GetSalasByFilmeIdAsync(idFilme);
            if (salas == null || !salas.Any())
                return NotFound("Nenhuma sala encontrada para o filme especificado.");

            return Ok(salas);
        }

        
        [HttpPost("sala/{salaId}/adicionar-filme/{idFilme}")]
        public async Task<IActionResult> AdicionarFilmeASala(int idSala, int idFilme)
        {
            try
            {
                bool resultado = await _gerenciamentoService.AdicionarFilmeASalaAsync(idSala, idFilme);
                if (resultado)
                    return Ok("Filme adicionado à sala com sucesso.");
                else
                    return BadRequest("Não foi possível adicionar o filme à sala.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpDelete("sala/{idSala}/remover-filme/{idFilme}")]
        public async Task<IActionResult> RemoverFilmeDaSala(int idSala, int idFilme)
        {
            var resultado = await _gerenciamentoService.RemoverFilmeDaSalaAsync(idSala, idFilme);

            if (!resultado)
                return NotFound("A associação entre o filme e a sala não foi encontrada.");

            return Ok("Filme removido da sala com sucesso.");
        }


        [HttpPut("filme/{idFilme}/transferir")]
        public async Task<IActionResult> TransferirFilmeParaOutraSala(int idFilme, int idSalaOrigem, int idSalaDestino)
        {
            var resultado = await _gerenciamentoService.TransferirFilmeParaOutraSalaAsync(idFilme, idSalaOrigem, idSalaDestino);

            if (!resultado)
                return BadRequest("Transferência inválida: verifique as salas e o filme informados.");

            return Ok("Filme transferido com sucesso para a nova sala.");
        }


        [HttpGet("sala/{salaId}/filmes")]
        public async Task<ActionResult<List<Filme>>> GetFilmesBySalaId(int idSala)
        {
            var filmes = await _gerenciamentoService.GetFilmesBySalaIdAsync(idSala);

            if (filmes == null || !filmes.Any())
                return NotFound("Nenhum filme encontrado para a sala especificada.");

            return Ok(filmes);
        }
    }
}
