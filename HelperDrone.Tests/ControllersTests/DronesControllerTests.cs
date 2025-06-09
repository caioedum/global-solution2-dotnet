using FluentAssertions;
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
    public class DronesControllerTests
    {
        private readonly Mock<IDroneRepository> _repoMock;
        private readonly DronesController _controller;

        public DronesControllerTests()
        {
            _repoMock = new Mock<IDroneRepository>();
            _controller = new DronesController(_repoMock.Object);
        }

        [Fact]
        public void ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var drones = new List<Drone>
            {
                new Drone { IdDrone = 1, Nome = "Drone Alpha" }
            };
            _repoMock.Setup(r => r.ObterTodos()).Returns(drones);

            // Act
            var result = _controller.ObterTodos();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(drones);
        }

        [Fact]
        public void ObterPorId_QuandoExiste_DeveRetornarOk()
        {
            // Arrange
            var drone = new Drone { IdDrone = 1, Nome = "Drone Alpha" };
            _repoMock.Setup(r => r.ObterPorId(1)).Returns(drone);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(drone);
        }

        [Fact]
        public void ObterPorId_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns((Drone?)null);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void AdicionarDrone_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var drone = new Drone { IdDrone = 1, Nome = "Novo Drone" };

            // Act
            var result = _controller.AdicionarDrone(drone);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(DronesController.ObterPorId));
        }

        [Fact]
        public void AtualizarDrone_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns((Drone?)null);

            // Act
            var result = _controller.AtualizarDrone(1, new Drone());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void AtualizarDrone_QuandoExiste_DeveRetornarNoContent()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns(new Drone());

            // Act
            var result = _controller.AtualizarDrone(1, new Drone());

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void RemoverDrone_QuandoNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns((Drone?)null);

            // Act
            var result = _controller.RemoverDrone(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void RemoverDrone_QuandoExiste_DeveRetornarNoContent()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns(new Drone());

            // Act
            var result = _controller.RemoverDrone(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
