using Microsoft.AspNetCore.Mvc;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/Operacao")]
    [ApiController]
    public class SomaController : ControllerBase
    {

        [HttpPost(Name = "Soma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> Soma(int A, int B) 
        {
            return Ok(A + B);
        }

    }

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
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            UFs.Add(NovaUF);
            return Ok(UFs);
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
