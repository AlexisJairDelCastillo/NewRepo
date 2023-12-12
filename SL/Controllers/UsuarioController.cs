using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("loginusuario/{email}")]
        public IActionResult LoginUsuario(string email)
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.Login(email);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("getbyid/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetById(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPut]
        [Route("update/{idUsuario}")]
        public IActionResult Update(int idUsuario, [FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Update(usuario);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete]
        [Route("delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = Convert.ToInt32(idUsuario);
            ML.Result result = BL.Usuario.Delete(usuario);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
