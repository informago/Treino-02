using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFMetodoController : ControllerBase
    {

        public readonly IConfiguration Config;

        public List<UF> UFs = new List<UF>();
        UFdbContext dbUF = new UFdbContext();

        public EFMetodoController(IConfiguration _config) 
        {
            Config = _config;

            dbUF.Database.EnsureCreated();

            if(dbUF.UFs.Count() == 0)
            {
                var UF1 = new UF() { Nome = "AM", Capital = "Manaus" };
                var UF2 = new UF() { Nome = "PA", Capital = "Belém" };
                var UF3 = new UF() { Nome = "PI", Capital = "Teresina" };
                dbUF.UFs.Add(UF1);
                dbUF.UFs.Add(UF2);
                dbUF.UFs.Add(UF3);
                dbUF.SaveChanges();

            }
        }

        [HttpGet(Name = "ListaUF-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            return Ok(dbUF.UFs.ToList());
        }

        [HttpGet("{IdUF:int}", Name = "MostraUF-EF")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            var ret = dbUF.UFs.Where(x => x.Id == IdUF).ToList();
            if (ret == null)
            {
                return NotFound();
            }
            return Ok(ret);
        }

        [HttpPost(Name = "Adiciona-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            if (NovaUF.Id > 0)
            {
                return AtualizaUF(NovaUF.Id, NovaUF.Nome, NovaUF.Capital);
            }
            dbUF.UFs.Add(NovaUF);
            dbUF.SaveChanges();
            return ListaUF();
        }

        [HttpPut(Name = "Altera-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            return ListaUF();
        }

        [HttpPatch(Name = "MudaCapital-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            return ListaUF();
        }

        [HttpDelete(Name = "EliminaUF-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            return ListaUF();
        }
    }
}
