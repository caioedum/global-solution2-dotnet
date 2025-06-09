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
    public class UsuariosControllerTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly UsuariosController _controller;

        public UsuariosControllerTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _controller = new UsuariosController(_repoMock.Object);
        }

        [Fact]
        public void ObterTodos_DeveRetornarOkComListaDeUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, Nome = "João Silva" },
                new Usuario { IdUsuario = 2, Nome = "Maria Souza" }
            };
            _repoMock.Setup(r => r.ObterTodos()).Returns(usuarios);

            // Act
            var result = _controller.ObterTodos();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(usuarios);
        }

        [Fact]
        public void ObterPorId_QuandoUsuarioExiste_DeveRetornarUsuario()
        {
            // Arrange
            var usuario = new Usuario { IdUsuario = 1, Nome = "João Silva" };
            _repoMock.Setup(r => r.ObterPorId(1)).Returns(usuario);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public void ObterPorId_QuandoUsuarioNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns((Usuario?)null);

            // Act
            var result = _controller.ObterPorId(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void AdicionarUsuario_DeveRetornarCreatedAtActionComUsuario()
        {
            // Arrange
            var usuario = new Usuario { IdUsuario = 1, Nome = "Novo Usuário" };

            // Act
            var result = _controller.AdicionarUsuario(usuario);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>()
                .Which.ActionName.Should().Be(nameof(UsuariosController.ObterPorId));

            result.As<CreatedAtActionResult>().Value.Should().BeEquivalentTo(usuario);
        }

        [Fact]
        public void AtualizarUsuario_QuandoUsuarioNaoExiste_DeveRetornarNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.ObterPorId(1)).Returns((Usuario?)null);
            var usuarioAtualizado = new Usuario { IdUsuario = 1, Nome = "João Atualizado" };

            // Act
            var result = _controller.AtualizarUsuario(1, usuarioAtualizado);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void AtualizarUsuario_QuandoUsuarioExiste_DeveRetornarNoContent()
        {
            // Arrange
            var usuarioExistente = new Usuario { IdUsuario = 1, Nome = "João Original" };
            var usuarioAtualizado = new Usuario { IdUsuario = 1, Nome = "João Atualizado" };

            _repoMock.Setup(r => r.ObterPorId(1)).Returns(usuarioExistente);

            // Act
            var result = _controller.AtualizarUsuario(1, usuarioAtualizado);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _repoMock.Verify(r => r.AtualizarUsuario(usuarioAtualizado), Times.Once);
        }
    }
}
