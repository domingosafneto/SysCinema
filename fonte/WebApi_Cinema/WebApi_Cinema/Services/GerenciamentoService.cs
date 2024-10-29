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

        // Método para adicionar um filme a uma sala
        public async Task<bool> AdicionarFilmeASalaAsync(int salaId, int filmeId)
        {
            // Verifica se a sala existe
            var sala = await _context.Salas.FindAsync(salaId);
            if (sala == null)
            {
                throw new ArgumentException("Sala não encontrada.");
            }

            // Verifica se o filme existe
            var filme = await _context.Filmes.FindAsync(filmeId);
            if (filme == null)
            {
                throw new ArgumentException("Filme não encontrado.");
            }

            // Verifica se a relação já existe para evitar duplicidade
            var salaFilmeExistente = await _context.SalaFilmes
                .AnyAsync(sf => sf.IdSala == salaId && sf.IdFilme == filmeId);
            if (salaFilmeExistente)
            {
                throw new InvalidOperationException("Esse filme já está vinculado a esta sala.");
            }

            // Cria a relação entre a sala e o filme
            var salaFilme = new SalaFilme
            {
                IdSala = salaId,
                IdFilme = filmeId
            };

            // Adiciona e salva a nova relação
            _context.SalaFilmes.Add(salaFilme);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
