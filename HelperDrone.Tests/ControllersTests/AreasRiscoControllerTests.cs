using HelperDrone.Contracts.Repositories;
using HelperDrone.Controllers;
using HelperDrone.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperDrone.Tests.ControllersTests
{
    public class AreasRiscoControllerTests
    {
        private readonly Mock<IAreaRiscoRepository> _repoMock;
        private readonly AreasRiscoController _controller;

        public AreasRiscoControllerTests()
        {
            _repoMock = new Mock<IAreaRiscoRepository>();
            _controller = new AreasRiscoController(_repoMock.Object);
        }

        [Fact]
        public void ObterTodas_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<AreaRisco>
            {
                new AreaRisco { IdArea = 1, NomeArea = "Zona Norte" }
            };
            _repoMock.Setup(r => r.ObterTodasAreasRisco()).Returns(lista);

            // Act
            var result = _controller.ObterTodas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<List<AreaRisco>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public void ObterPorId_QuandoExiste_DeveRetornarOk()
        {
            // Arrange
            var area = new AreaRisco { IdArea = 1, NomeArea = "Zona Norte" };
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns(area);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<AreaRisco>(okResult.Value);
            Assert.Equal(1, value.IdArea);
        }

        [Fact]
        public void ObterPorId_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns((AreaRisco?)null);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void AdicionarAreaRisco_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var area = new AreaRisco { IdArea = 1, NomeArea = "Zona Sul" };

            // Act
            var result = _controller.AdicionarAreaRisco(area);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var value = Assert.IsType<AreaRisco>(createdResult.Value);
            Assert.Equal(1, value.IdArea);
        }

        [Fact]
        public void AtualizarAreaRisco_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns((AreaRisco?)null);

            // Act
            var result = _controller.AtualizarAreaRisco(1, new AreaRisco());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void AtualizarAreaRisco_QuandoExiste_DeveRetornarNoContent()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns(new AreaRisco { IdArea = 1 });

            // Act
            var result = _controller.AtualizarAreaRisco(1, new AreaRisco());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RemoverAreaRisco_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns((AreaRisco?)null);

            // Act
            var result = _controller.RemoverAreaRisco(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void RemoverAreaRisco_QuandoExiste_DeveRetornarNoContent()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAreaRiscoPorId(1)).Returns(new AreaRisco { IdArea = 1 });

            // Act
            var result = _controller.RemoverAreaRisco(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
