
using Microsoft.AspNetCore.Mvc;
using Shoalace.Domain.Interfaces.Commands;

namespace Shoalace.API.Controllers
{
    /// <summary>
    /// Controller Base
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Construtor do BaseController
        /// </summary>
        public BaseController()
        {
        }

        /// <summary>
        /// Este método é utilizado para dar um retorno verificando se tem alguma notificação.
        /// </summary>
        /// <returns>Ou retorna um BadRequest ou Ok</returns>
        protected IActionResult RetornoController(IResultadoCommand comando)
        {
            if (comando.Invalido())
                return BadRequest(comando);

            return Ok(comando);
        }
    }
}
