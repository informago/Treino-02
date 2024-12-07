using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    /// <summary>
    /// Exemplo de operações CRUD com Entity Framework
    /// </summary>
    [Route("UFs/EF")]
    [ApiController]
    public class EFMetodoController : ControllerBase
    {
        /// <summary>
        /// Config manipula as configurações em appsettings.json
        /// </summary>
        public readonly IConfiguration Config;

        /// <summary>
        /// UFs é uma lista de objetos do tipo UF
        /// </summary>
        public List<UF> UFs = new List<UF>();
        UFdbContext dbUF = new UFdbContext();

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="_config"></param>
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

        /// <summary>
        /// Lista todas as UFs do banco de dados
        /// </summary>
        /// <remarks>
        /// Também deve ser utilizado para verificar se a conexão está ativa.
        /// </remarks>
        /// <returns></returns>
        [HttpGet("ListaUF", Name = "ListaUF-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            if (!dbUF.Database.CanConnect()) 
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
            return Ok(dbUF.UFs.ToList());
        }

        /// <summary>
        /// Mostra dados de uma UF baseado no id
        /// </summary>
        /// <param name="IdUF">Id da UF</param>
        /// <returns></returns>
        [HttpGet("MostraUF/{IdUF:int}", Name = "MostraUF-EF")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            var res = dbUF.UFs.Where(x => x.Id == IdUF).ToList();
            if (res == null || res.Count ==0)
            {
                return NotFound();
            }
            UF ret = res[0];
            return Ok(ret);
        }

        /// <summary>
        /// Adiciona uma nova UF ao banco de dados
        /// </summary>
        /// <param name="NovaUF"></param>
        /// <returns></returns>
        [HttpPost("AdicionaUF", Name = "Adiciona-EF")]
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

        /// <summary>
        /// Altera todos os dados de uma UF baseado em um id
        /// </summary>
        /// <param name="IdUF">Id da UF que será alterada</param>
        /// <param name="NomeUF">Novo nome (sigla) da UF</param>
        /// <param name="CapitalUF">Novo nome da capital</param>
        /// <returns></returns>
        [HttpPut("AtualizaUF", Name = "Altera-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            var res = dbUF.UFs.Where(x => x.Id == IdUF).ToList();
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }
            UF ret = res[0];
            ret.Nome = NomeUF;
            ret.Capital = CapitalUF;
            dbUF.UFs.Update(ret);
            dbUF.SaveChanges();
            return ListaUF();
        }

        /// <summary>
        /// Muda apenas a capital de uma UF baseado no nome da UF
        /// </summary>
        /// <param name="NomeUF">Nome da UF (sigla)</param>
        /// <param name="NovaCapital">Nome da nova capital</param>
        /// <returns></returns>
        [HttpPatch("MudaCapital", Name = "MudaCapital-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            var res = dbUF.UFs.Where(x => x.Nome == NomeUF).ToList();
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }
            foreach (UF uf in res)
            {
                uf.Capital = NovaCapital;
                dbUF.UFs.Update(uf);
            }
            dbUF.SaveChanges();
            return ListaUF();
        }

        /// <summary>
        /// Elimina uma UF baseado no id
        /// </summary>
        /// <param name="DelId">Id da UF que será eliminada</param>
        /// <returns></returns>
        [HttpDelete("EliminaUF", Name = "EliminaUF-EF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            var res = dbUF.UFs.Where(x => x.Id == DelId).ToList();
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }
            UF ret = res[0];
            dbUF.UFs.Remove(ret);
            dbUF.SaveChanges();
            return ListaUF();
        }
    }
}
