using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using HelperDrone.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HelperDrone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> ObterTodos()
        {
            var usuarios = _usuarioRepository.ObterTodos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> ObterPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult AdicionarUsuario([FromBody] Usuario usuario)
        {
            _usuarioRepository.AdicionarUsuario(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            var usuarioExistente = _usuarioRepository.ObterPorId(id);
            if (usuarioExistente == null)
                return NotFound();

            usuario.IdUsuario = id;
            _usuarioRepository.AtualizarUsuario(usuario);
            return NoContent();
        }
    }
}
