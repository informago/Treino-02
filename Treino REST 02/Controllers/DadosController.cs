using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/Dados")]
    [ApiController]
    public class DadosController : ControllerBase
    {

        IConfiguration Config;
        public List<UF> UFs = new List<UF>();

        public DadosController(IConfiguration _config) 
        {
            this.Config = _config;
        }

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
            return ListaUF();
        }

        [HttpPut(Name = "Altera-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            return ListaUF();
        }

        [HttpPatch(Name = "MudaCapital-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            return ListaUF();
        }

        [HttpDelete(Name = "EliminaUF-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            return ListaUF();
        }
    }
}
