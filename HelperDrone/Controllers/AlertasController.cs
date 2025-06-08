using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HelperDrone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly IAlertaRepository _alertaRepository;
        private readonly IConnection _rabbitConnection;

        public AlertasController(IAlertaRepository alertaRepository, IConnection rabbitConnection)
        {
            _alertaRepository = alertaRepository;
            _rabbitConnection = rabbitConnection;
        }

        [HttpGet]
        public ActionResult<List<Alerta>> ObterTodos()
        {
            var alertas = _alertaRepository.ObterTodosAlertas();
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        public ActionResult<Alerta> ObterPorId(int id)
        {
            var alerta = _alertaRepository.ObterAlertaPorId(id);
            if (alerta == null)
                return NotFound(new { Mensagem = "Alerta não encontrado." });
            return Ok(alerta);
        }

        [HttpPost]
        public ActionResult AdicionarAlerta([FromBody] Alerta alerta)
        {
            _alertaRepository.AdicionarAlerta(alerta);
            return CreatedAtAction(nameof(ObterPorId), new { id = alerta.IdAlerta }, alerta);
        }

        [HttpPost("enviar-alertas")]
        public async Task<ActionResult> CriarAlerta([FromBody] Alerta alerta)
        {
            try
            {
                _alertaRepository.AdicionarAlerta(alerta);

                using var channel = _rabbitConnection.CreateModel();
                channel.QueueDeclare(queue: "alertas", durable: true, exclusive: false, autoDelete: false);

                var mensagem = JsonSerializer.Serialize(alerta);
                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "alertas",
                    basicProperties: null,
                    body: body);

                return CreatedAtAction(nameof(ObterPorId), new { id = alerta.IdAlerta }, alerta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao processar alerta.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarAlerta(int id, [FromBody] Alerta alerta)
        {
            var existente = _alertaRepository.ObterAlertaPorId(id);
            if (existente == null)
                return NotFound(new { Mensagem = "Alerta não encontrado para atualização." });

            alerta.IdAlerta = id;
            _alertaRepository.AtualizarAlerta(alerta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverAlerta(int id)
        {
            var existente = _alertaRepository.ObterAlertaPorId(id);
            if (existente == null)
                return NotFound(new { Mensagem = "Alerta não encontrado para remoção." });

            _alertaRepository.RemoverAlerta(id);
            return NoContent();
        }

        [HttpGet("por-area/{areaId}")]
        public ActionResult<List<Alerta>> ObterPorArea(int areaId)
        {
            var alertas = _alertaRepository.ObterAlertasPorAreaRisco(areaId);
            if (alertas == null || alertas.Count == 0)
                return NotFound(new { Mensagem = "Nenhum alerta encontrado para esta área de risco." });
            return Ok(alertas);
        }

        [HttpGet("por-drone/{droneId}")]
        public ActionResult<List<Alerta>> ObterPorDrone(int droneId)
        {
            var alertas = _alertaRepository.ObterAlertasPorDrone(droneId);
            if (alertas == null || alertas.Count == 0)
                return NotFound(new { Mensagem = "Nenhum alerta encontrado para este drone." });
            return Ok(alertas);
        }

        [HttpGet("por-usuario/{usuarioId}")]
        public ActionResult<List<Alerta>> ObterPorUsuario(int usuarioId)
        {
            var alertas = _alertaRepository.ObterAlertasPorUsuario(usuarioId);
            if (alertas == null || alertas.Count == 0)
                return NotFound(new { Mensagem = "Nenhum alerta encontrado para este usuário." });
            return Ok(alertas);
        }
    }
}
