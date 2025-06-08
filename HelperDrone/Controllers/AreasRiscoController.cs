using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelperDrone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasRiscoController : Controller
    {
        private readonly IAreaRiscoRepository _areaRiscoRepository;

        public AreasRiscoController(IAreaRiscoRepository areaRiscoRepository)
        {
            _areaRiscoRepository = areaRiscoRepository;
        }

        [HttpGet]
        public ActionResult<List<AreaRisco>> ObterTodas()
        {
            var areas = _areaRiscoRepository.ObterTodasAreasRisco();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<AreaRisco> ObterPorId(int id)
        {
            var area = _areaRiscoRepository.ObterAreaRiscoPorId(id);
            if (area == null)
                return NotFound(new { Mensagem = "Área de risco não encontrada." });
            return Ok(area);
        }

        [HttpPost]
        public ActionResult AdicionarAreaRisco([FromBody] AreaRisco areaRisco)
        {
            _areaRiscoRepository.AdicionarAreaRisco(areaRisco);
            return CreatedAtAction(nameof(ObterPorId), new { id = areaRisco.IdArea }, areaRisco);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarAreaRisco(int id, [FromBody] AreaRisco areaRisco)
        {
            var existente = _areaRiscoRepository.ObterAreaRiscoPorId(id);
            if (existente == null)
                return NotFound(new { Mensagem = "Área de risco não encontrada para atualização." });

            areaRisco.IdArea = id;
            _areaRiscoRepository.AtualizarAreaRisco(areaRisco);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoverAreaRisco(int id)
        {
            var existente = _areaRiscoRepository.ObterAreaRiscoPorId(id);
            if (existente == null)
                return NotFound(new { Mensagem = "Área de risco não encontrada para remoção." });

            _areaRiscoRepository.RemoverAreaRisco(id);
            return NoContent();
        }
    }
}
