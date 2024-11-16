using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/Dados")]
    [ApiController]
    public class DadosController : ControllerBase
    {

        public List<UF> UFs = new List<UF>();

        [HttpGet(Name = "ListaUF-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            return Ok(UFs);
        }

        [HttpGet("{IdUF:int}", Name = "MostraUF-Dados")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            return BadRequest();
        }

        [HttpPost(Name = "Adiciona-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            return Ok(ListaUF);
        }

        [HttpPut(Name = "Altera-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            return Ok(ListaUF);
        }

        [HttpPatch(Name = "MudaCapital-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            return Ok(ListaUF);
        }

        [HttpDelete(Name = "EliminaUF-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            return Ok(ListaUF);
        }
    }
}
