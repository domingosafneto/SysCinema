using Xunit;
using Moq;
using WebApi_Cinema.Services;
using WebApi_Cinema.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi_Cinema.Data;


namespace WepApi_Cinema.Tests
{
    public class GerenciamentoServiceTests
    {
        private readonly GerenciamentoService _gerenciamentoService;
        private readonly Mock<Api_DbContext> _dbContextMock;

        public GerenciamentoServiceTests()
        {
            _dbContextMock = new Mock<Api_DbContext>();
            _gerenciamentoService = new GerenciamentoService(_dbContextMock.Object);
        }

        [Fact]
        public async Task AdicionarFilmeASalaAsync_DeveAdicionarFilmeCorretamente()
        {
            // Arrange
            int idSala = 1;
            int idFilme = 1;

            var sala = new Sala { IdSala = idSala, NumeroSala = 10, Descricao = "Sala 10" };
            var filme = new Filme { IdFilme = idFilme, Nome = "Filme Teste" };

            _dbContextMock.Setup(db => db.Salas.FindAsync(idSala)).ReturnsAsync(sala);
            _dbContextMock.Setup(db => db.Filmes.FindAsync(idFilme)).ReturnsAsync(filme);
            _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _gerenciamentoService.AdicionarFilmeASalaAsync(idSala, idFilme);

            // Assert
            Assert.True(result);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }


        [Fact]
        public async Task RemoverFilmeDaSalaAsync_DeveRemoverFilmeCorretamente()
        {            
            int idSala = 1;
            int idFilme = 1;

            var sala = new Sala
            {
                IdSala = idSala,
                NumeroSala = 10,
                Descricao = "Sala 10",
            };
            
            _dbContextMock.Setup(db => db.Salas.FindAsync(idSala)).ReturnsAsync(sala);
            _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            var filmeSala = new SalaFilme { IdFilme = idFilme, IdSala = idSala };

            var result = await _gerenciamentoService.RemoverFilmeDaSalaAsync(idSala, idFilme);

            Assert.True(result);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }


    }
}