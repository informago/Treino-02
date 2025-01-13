using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    /// <summary>
    /// Esqueleto para realizar operações CRUD.
    /// </summary>
    [Route("UFs/Dados")]
    [ApiController]
    public class DadosController : ControllerBase
    {

        IConfiguration Config;
        public List<UF> UFs = new List<UF>();

        /// <summary>
        /// Construtor inicia objeto Config
        /// </summary>
        /// <param name="_config"></param>
        public DadosController(IConfiguration _config) 
        {
            this.Config = _config;
        }

        /// <summary>
        /// Relação de todas as UFs com suas capitais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListaUF", Name = "ListaUF-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            return Ok(UFs);
        }

        /// <summary>
        /// Mostra os dados de uma UF específica
        /// </summary>
        /// <remarks>
        /// Caso o id não exista, retorna 404 Not Found. Caso id seja zero retorna Bad Request.
        /// </remarks>
        /// <param name="IdUF">Id da UF que será mostrada</param>
        /// <returns>Um JSON contendo os dados da UF selecionada.</returns>
        [HttpGet("MostraUF/{IdUF:int}", Name = "MostraUF-Dados")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            return BadRequest();
        }

        /// <summary>
        /// Adiciona uma nova UF à lista
        /// </summary>
        /// <param name="NovaUF"></param>
        /// <returns></returns>
        [HttpPost("AdicionaUF", Name = "Adiciona-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            return ListaUF();
        }

        /// <summary>
        /// Altera todos os dados de uma UF baseado em um id
        /// </summary>
        /// <param name="IdUF">Id da UF que será alterada</param>
        /// <param name="NomeUF">Novo nome (sigla) da UF</param>
        /// <param name="CapitalUF">Novo nome da capital</param>
        /// <returns></returns>
        [HttpPut("AtualizaUF", Name = "Altera-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            return ListaUF();
        }

        /// <summary>
        /// Muda apenas a capital de uma UF baseado no nome da UF
        /// </summary>
        /// <param name="NomeUF">Nome da UF (sigla)</param>
        /// <param name="NovaCapital">Nome da nova capital</param>
        /// <returns></returns>
        [HttpPatch("MudaCapital", Name = "MudaCapital-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            return ListaUF();
        }

        /// <summary>
        /// Elimina uma UF baseado no id
        /// </summary>
        /// <param name="DelId">Id da UF que será eliminada</param>
        /// <returns></returns>
        [HttpDelete("EliminaUF", Name = "EliminaUF-Dados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            return ListaUF();
        }
    }
}
