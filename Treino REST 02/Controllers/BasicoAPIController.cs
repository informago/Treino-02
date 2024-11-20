using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.Json;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{

    /// <summary>
    /// Exemplo de operações com objeto em memória.
    /// </summary>
    [Route("api/ListaUF")]
    [ApiController]
    public class ListaUFController : ControllerBase
    {

        static List<UF> UFs = new List<UF>();
    
        /// <summary>
        /// Construtor
        /// </summary>
        public ListaUFController()
        {
            if (UFs.Count == 0)
            {
                UFs.Add(new UF { Id = 1, Nome = "RJ", Capital = "Rio de Janeiro" });
                UFs.Add(new UF { Id = 2, Nome = "SP", Capital = "São Paulo" });
                UFs.Add(new UF { Id = 3, Nome = "BA", Capital = "Salvador" });
            }
        }

        /// <summary>
        /// Relação de todas as UFs com suas capitais
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Listagem")]
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
        [HttpGet("{IdUF:int}", Name = "MostraUF")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            if (IdUF == 0)
            {
                return BadRequest();
            }
            UF result = UFs.Find(x => x.Id == IdUF);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Adiciona uma nova UF à lista
        /// </summary>
        /// <param name="NovaUF"></param>
        /// <returns></returns>
        [HttpPost(Name ="Adiciona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            if (NovaUF.Id > 0)
            {
                return AtualizaUF(NovaUF.Id, NovaUF.Nome, NovaUF.Capital);
            }
            UFs.Add(NovaUF);
            return Ok(UFs);
        }

        /// <summary>
        /// Altera todos os dados de uma UF baseado em um id
        /// </summary>
        /// <param name="IdUF">Id da UF que será alterada</param>
        /// <param name="NomeUF">Novo nome (sigla) da UF</param>
        /// <param name="CapitalUF">Novo nome da capital</param>
        /// <returns></returns>
        [HttpPut(Name = "Altera")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            if (IdUF == 0)
            {
                return BadRequest();
            }
            UF result = UFs.Find(x => x.Id == IdUF);
            if (result == null)
            {
                return NotFound();
            }
            result.Nome = NomeUF;
            result.Capital = CapitalUF;
            return Ok(result);
        }

        /// <summary>
        /// Muda apenas a capital de uma UF baseado no nome da UF
        /// </summary>
        /// <param name="NomeUF">Nome da UF (sigla)</param>
        /// <param name="NovaCapital">Nome da nova capital</param>
        /// <returns></returns>
        [HttpPatch(Name = "MudaCapital")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            if (NomeUF == "")
            {
                return BadRequest();
            }
            UF result = UFs.Find(x => x.Nome == NomeUF);
            if (result == null)
            {
                return NotFound();
            }
            result.Capital = NovaCapital;
            return Ok(result);
        }

        /// <summary>
        /// Elimina uma UF baseado no id
        /// </summary>
        /// <param name="DelId">Id da UF que será eliminada</param>
        /// <returns></returns>
        [HttpDelete(Name ="EliminaUF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            if (DelId == 0)
            {
                return BadRequest();
            }
            UF result = UFs.Find(x => x.Id == DelId);
            if (result == null)
            {
                return NotFound();
            }
            UFs.Remove(result);
            return Ok(UFs);
        }
    }

}
