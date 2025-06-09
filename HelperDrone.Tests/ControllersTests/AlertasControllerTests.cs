using FluentAssertions;
using HelperDrone.Contracts.Repositories;
using HelperDrone.Controllers;
using HelperDrone.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperDrone.Tests.ControllersTests
{
    public class AlertasControllerTests
    {
        private readonly Mock<IAlertaRepository> _repoMock;
        private readonly Mock<SentimentAnalysis> _sentimentAnalysisMock;
        private readonly AlertasController _controller;

        public AlertasControllerTests()
        {
            _repoMock = new Mock<IAlertaRepository>();
            _sentimentAnalysisMock = new Mock<SentimentAnalysis>();
            _controller = new AlertasController(_repoMock.Object, _sentimentAnalysisMock.Object);
        }

        [Fact]
        public void ObterTodos_DeveRetornarOkComListaDeAlertas()
        {
            // Arrange
            var alertas = new List<Alerta> { new Alerta { IdAlerta = 1 } };
            _repoMock.Setup(r => r.ObterTodosAlertas()).Returns(alertas);

            // Act
            var result = _controller.ObterTodos();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(alertas);
        }

        [Fact]
        public void ObterPorId_QuandoAlertaExiste_DeveRetornarOk()
        {
            // Arrange
            var alerta = new Alerta { IdAlerta = 1 };
            _repoMock.Setup(r => r.ObterAlertaPorId(1)).Returns(alerta);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(alerta);
        }

        [Fact]
        public void ObterPorId_QuandoAlertaNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertaPorId(1)).Returns((Alerta?)null);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Alerta não encontrado." });
        }

        [Fact]
        public void AdicionarAlerta_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var alerta = new Alerta { IdAlerta = 1 };

            // Act
            var result = _controller.AdicionarAlerta(alerta);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(AlertasController.ObterPorId));
        }

        [Fact]
        public async Task CriarAlerta_QuandoFalha_DeveRetornar500()
        {
            // Arrange
            var alerta = new Alerta { IdAlerta = 1 };
            _repoMock.Setup(r => r.AdicionarAlerta(alerta)).Throws(new System.Exception("Erro simulado"));
        }

        [Fact]
        public void AtualizarAlerta_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertaPorId(1)).Returns((Alerta?)null);

            // Act
            var result = _controller.AtualizarAlerta(1, new Alerta());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Alerta não encontrado para atualização." });
        }

        [Fact]
        public void RemoverAlerta_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertaPorId(1)).Returns((Alerta?)null);

            // Act
            var result = _controller.RemoverAlerta(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Alerta não encontrado para remoção." });
        }

        [Fact]
        public void ObterPorArea_QuandoExistemAlertas_DeveRetornarOk()
        {
            // Arrange
            var alertas = new List<Alerta> { new Alerta { IdAlerta = 1 } };
            _repoMock.Setup(r => r.ObterAlertasPorAreaRisco(1)).Returns(alertas);

            // Act
            var result = _controller.ObterPorArea(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(alertas);
        }

        [Fact]
        public void ObterPorArea_QuandoNaoExistemAlertas_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertasPorAreaRisco(1)).Returns(new List<Alerta>());

            // Act
            var result = _controller.ObterPorArea(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Nenhum alerta encontrado para esta área de risco." });
        }

        [Fact]
        public void ObterPorDrone_QuandoExistemAlertas_DeveRetornarOk()
        {
            // Arrange
            var alertas = new List<Alerta> { new Alerta { IdAlerta = 1 } };
            _repoMock.Setup(r => r.ObterAlertasPorDrone(1)).Returns(alertas);

            // Act
            var result = _controller.ObterPorDrone(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(alertas);
        }

        [Fact]
        public void ObterPorDrone_QuandoNaoExistemAlertas_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertasPorDrone(1)).Returns(new List<Alerta>());

            // Act
            var result = _controller.ObterPorDrone(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Nenhum alerta encontrado para este drone." });
        }

        [Fact]
        public void ObterPorUsuario_QuandoExistemAlertas_DeveRetornarOk()
        {
            // Arrange
            var alertas = new List<Alerta> { new Alerta { IdAlerta = 1 } };
            _repoMock.Setup(r => r.ObterAlertasPorUsuario(1)).Returns(alertas);

            // Act
            var result = _controller.ObterPorUsuario(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(alertas);
        }

        [Fact]
        public void ObterPorUsuario_QuandoNaoExistemAlertas_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterAlertasPorUsuario(1)).Returns(new List<Alerta>());

            // Act
            var result = _controller.ObterPorUsuario(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { Mensagem = "Nenhum alerta encontrado para este usuário." });
        }
    }
}
