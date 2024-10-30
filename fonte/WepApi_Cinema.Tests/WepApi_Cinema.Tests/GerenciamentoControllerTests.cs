using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_Cinema.Controllers;
using WebApi_Cinema.Services;

namespace WepApi_Cinema.Tests
{
    public class GerenciamentoControllerTests
    {
        private readonly Mock<GerenciamentoService> _gerenciamentoServiceMock;
        private readonly GerenciamentoController _gerenciamentoController;

        public GerenciamentoControllerTests()
        {
            _gerenciamentoServiceMock = new Mock<GerenciamentoService>();
            _gerenciamentoController = new GerenciamentoController(_gerenciamentoServiceMock.Object);
        }

        [Fact]
        public async Task AdicionarFilmeNaSalaAsync_DeveRetornarOkQuandoAdicionado()
        {            
            int idSala = 1;
            int idFilme = 1;
            
            // a implementar
        }

        [Fact]
        public async Task RemoverFilmeDaSalaAsync_DeveRetornarOkQuandoRemovido()
        {
            int idSala = 1;
            int idFilme = 1;
            _gerenciamentoServiceMock.Setup(s => s.RemoverFilmeDaSalaAsync(idSala, idFilme)).ReturnsAsync(true);

            // a implementar
          
        }
    }
}
