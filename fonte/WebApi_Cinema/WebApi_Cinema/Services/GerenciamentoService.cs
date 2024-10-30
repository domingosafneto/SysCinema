using Microsoft.EntityFrameworkCore;
using WebApi_Cinema.Data;
using WebApi_Cinema.Models;

namespace WebApi_Cinema.Services
{
    public class GerenciamentoService
    {
        private readonly Api_DbContext _context;

        public GerenciamentoService(Api_DbContext context)
        {
            _context = context;
        }

        // Adiciona um filme a uma sala
        public async Task<bool> AdicionarFilmeASalaAsync(int idSala, int idFilme)
        {            
            var sala = await _context.Salas.FindAsync(idSala);
            if (sala == null)
            {
                throw new ArgumentException("Sala não encontrada.");
            }
           
            var filme = await _context.Filmes.FindAsync(idFilme);
            if (filme == null)
            {
                throw new ArgumentException("Filme não encontrado.");
            }
            
            var salaFilmeExistente = await _context.SalaFilmes
                .AnyAsync(sf => sf.IdSala == idSala && sf.IdFilme == idFilme);
            if (salaFilmeExistente)
            {
                throw new InvalidOperationException("Esse filme já está vinculado a esta sala.");
            }
            
            var salaFilme = new SalaFilme
            {
                IdSala = idSala,
                IdFilme = idFilme
            };
            
            _context.SalaFilmes.Add(salaFilme);
            await _context.SaveChangesAsync();

            return true;
        }

        // remove filme de uma determinada sala
        public async Task<bool> RemoverFilmeDaSalaAsync(int idSala, int idFilme)
        {
            var salaFilme = await _context.SalaFilmes
                .FirstOrDefaultAsync(sf => sf.IdSala == idSala && sf.IdFilme == idFilme);

            if (salaFilme == null)
            {
                return false; 
            }

            _context.SalaFilmes.Remove(salaFilme);
            await _context.SaveChangesAsync();
            return true;
        }

        // transfere o filme de uma sala para outra
        public async Task<bool> TransferirFilmeParaOutraSalaAsync(int idFilme, int idSalaOrigem, int idSalaDestino)
        {            
            var salaFilmeOrigem = await _context.SalaFilmes
                .FirstOrDefaultAsync(sf => sf.IdSala == idSalaOrigem && sf.IdFilme == idFilme);

            if (salaFilmeOrigem == null)
            {                
                return false;
            }
           
            var salaFilmeDestino = await _context.SalaFilmes
                .FirstOrDefaultAsync(sf => sf.IdSala == idSalaDestino && sf.IdFilme == idFilme);

            if (salaFilmeDestino != null)
            {                
                return false;
            }
            
            _context.SalaFilmes.Remove(salaFilmeOrigem);
            
            var novaSalaFilme = new SalaFilme
            {
                IdSala = idSalaDestino,
                IdFilme = idFilme
            };
            await _context.SalaFilmes.AddAsync(novaSalaFilme);

           
            await _context.SaveChangesAsync();
            return true;
        }


        // retorna os filmes de uma determinada sala
        public async Task<List<Filme>> GetFilmesBySalaIdAsync(int idSala)
        {
            var filmes = await _context.SalaFilmes
                .Where(sf => sf.IdSala == idSala)
                .Include(sf => sf.Filme)
                .Select(sf => sf.Filme)
                .ToListAsync();

            return filmes;
        }

        // traz as salas de determinado filme
        public async Task<IEnumerable<Sala>> GetSalasByFilmeIdAsync(int idFilme)
        {
            return await _context.SalaFilmes
                .Where(sf => sf.IdFilme == idFilme)
                .Include(sf => sf.Sala)
                .Select(sf => sf.Sala)
                .ToListAsync();
        }
    }
}
