using Microsoft.EntityFrameworkCore;
using WebApi_Cinema.Data;
using WebApi_Cinema.Models;

namespace WebApi_Cinema.Services
{
    public class FilmeService
    {
        private readonly Api_DbContext _context;

        public FilmeService(Api_DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filme>> GetFilmesAsync()
        {
            return await _context.Filmes.ToListAsync();
        }

        public async Task<Filme> GetFilmeByIdAsync(int id)
        {
            return await _context.Filmes.FindAsync(id);
        }

        public async Task<Filme> CreateFilmeAsync(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task UpdateFilmeAsync(Filme filme)
        {
            _context.Entry(filme).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFilmeAsync(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
            }
        }

        // traz as salas de determinado filme
        public async Task<IEnumerable<Sala>> GetSalasByFilmeIdAsync(int filmeId)
        {
            return await _context.SalaFilmes
                .Where(sf => sf.IdFilme == filmeId)
                .Select(sf => sf.Sala)
                .ToListAsync();
        }
    }
}
