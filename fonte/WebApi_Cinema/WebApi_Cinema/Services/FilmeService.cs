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

        public async Task<IEnumerable<Filme>> GetAllFilmesAsync()
        {
            return await _context.Filmes.Include(f => f.SalaFilmes).ToListAsync();
        }

        public async Task<Filme?> GetFilmeByIdAsync(int id)
        {
            return await _context.Filmes
                .Include(f => f.SalaFilmes)
                .ThenInclude(sf => sf.Sala)
                .FirstOrDefaultAsync(f => f.IdFilme == id);
        }

        public async Task<Filme> AddFilmeAsync(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme?> UpdateFilmeAsync(int id, Filme updatedFilme)
        {
            var existingFilme = await _context.Filmes.FindAsync(id);
            if (existingFilme == null)
                return null;

            existingFilme.Nome = updatedFilme.Nome;
            existingFilme.Diretor = updatedFilme.Diretor;
            existingFilme.Duracao = updatedFilme.Duracao;

            _context.Filmes.Update(existingFilme);
            await _context.SaveChangesAsync();

            return existingFilme;
        }

        public async Task<bool> DeleteFilmeAsync(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
                return false;

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
