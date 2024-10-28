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

        public async Task<IEnumerable<Sala>> GetAllSalasAsync()
        {
            return await _context.Salas.Include(s => s.SalaFilmes).ToListAsync();
        }

        public async Task<Sala?> GetSalaByIdAsync(int id)
        {
            return await _context.Salas
                .Include(s => s.SalaFilmes)
                .ThenInclude(sf => sf.Filme)
                .FirstOrDefaultAsync(s => s.IdSala == id);
        }

        public async Task<Sala> AddSalaAsync(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task<Sala?> UpdateSalaAsync(int id, Sala updatedSala)
        {
            var existingSala = await _context.Salas.FindAsync(id);
            if (existingSala == null)
                return null;

            existingSala.NumeroSala = updatedSala.NumeroSala;
            existingSala.Descricao = updatedSala.Descricao;

            _context.Salas.Update(existingSala);
            await _context.SaveChangesAsync();

            return existingSala;
        }

        public async Task<bool> DeleteSalaAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
                return false;

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
