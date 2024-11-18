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

    [Route("api/ListaUF")]
    [ApiController]
    public class ListaUFController : ControllerBase
    {

        static List<UF> UFs = new List<UF>();
    
        public ListaUFController()
        {
            if (UFs.Count == 0)
            {
                UFs.Add(new UF { Id = 1, Nome = "RJ", Capital = "Rio de Janeiro" });
                UFs.Add(new UF { Id = 2, Nome = "SP", Capital = "São Paulo" });
                UFs.Add(new UF { Id = 3, Nome = "BA", Capital = "Salvador" });
            }
        }

        [HttpGet(Name = "Listagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            return Ok(UFs);
        }

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
