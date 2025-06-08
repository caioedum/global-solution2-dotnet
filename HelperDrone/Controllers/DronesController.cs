using Microsoft.AspNetCore.Mvc;
using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;

namespace HelperDrone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DronesController : ControllerBase
    {
        private readonly IDroneRepository _droneRepository;

        public DronesController(IDroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }

        [HttpGet]
        public ActionResult<List<Drone>> ObterTodos()
        {
            var drones = _droneRepository.ObterTodos();
            return Ok(drones);
        }

        [HttpGet("{id}")]
        public ActionResult<Drone> ObterPorId(int id)
        {
            var drone = _droneRepository.ObterPorId(id);
            if (drone == null)
                return NotFound();
            return Ok(drone);
        }

        [HttpPost]
        public ActionResult AdicionarDrone([FromBody] Drone drone)
        {
            _droneRepository.AdicionarDrone(drone);
            return CreatedAtAction(nameof(ObterPorId), new { id = drone.IdDrone }, drone);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarDrone(int id, [FromBody] Drone drone)
        {
            var existente = _droneRepository.ObterPorId(id);
            if (existente == null)
                return NotFound();

            drone.IdDrone = id;
            _droneRepository.AtualizarDrone(drone);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverDrone(int id)
        {
            var existente = _droneRepository.ObterPorId(id);
            if (existente == null)
                return NotFound();

            _droneRepository.RemoverDrone(id);
            return NoContent();
        }

        [HttpGet("disponiveis")]
        public ActionResult<object> ObterDronesDisponiveis()
        {
            var drones = _droneRepository.ObterDronesDisponiveis();
            if (drones.Count == 0)
            {
                return NotFound("Nenhum drone disponível encontrado.");
            }
            return Ok(new { Drones = drones });
        }

        [HttpGet("em-missao")]
        public ActionResult<object> ObterDronesEmMissao()
        {
            var drones = _droneRepository.ObterDronesEmMissao();
            if (drones.Count == 0)
            {
                return NotFound("Nenhum drone em missão encontrado.");
            }
            return Ok(new { Drones = drones });
        }

    }
}
