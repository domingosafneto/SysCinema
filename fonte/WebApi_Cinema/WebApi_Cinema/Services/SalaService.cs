using WebApi_Cinema.Data;
using WebApi_Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Cinema.Services
{
    public class SalaService
    {
        private readonly Api_DbContext _context;

        public SalaService(Api_DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sala>> GetSalasAsync()
        {
            return await _context.Salas.ToListAsync();
        }

        public async Task<Sala> GetSalaByIdAsync(int id)
        {
            return await _context.Salas.FindAsync(id);
        }

        public async Task<Sala> CreateSalaAsync(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task UpdateSalaAsync(Sala sala)
        {
            _context.Entry(sala).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalaAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();
            }
        }

        // traz os filmes de determinada sala
        public async Task<IEnumerable<Filme>> GetFilmesBySalaIdAsync(int salaId)
        {
            return await _context.SalaFilmes
                .Where(sf => sf.IdSala == salaId)
                .Select(sf => sf.Filme)
                .ToListAsync();
        }
    }
}
