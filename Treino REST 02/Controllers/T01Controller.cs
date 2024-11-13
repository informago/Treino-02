using Microsoft.AspNetCore.Mvc;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/Operacao")]
    [ApiController]
    public class SomaController : ControllerBase
    {

        [HttpPost(Name = "Soma")]
        public int Soma(int A, int B) 
        {
            return A + B;
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
        public IEnumerable<UF> ListaUF()
        {
            return UFs;
        }

        [HttpPost(Name ="Adiciona")]
        public IEnumerable<UF> AdicionaUF(UF NovaUF)
        {
            UFs.Add(NovaUF);
            return UFs;
        }

        [HttpDelete(Name ="EliminaUF")]
        public IEnumerable<UF> EliminaUF(int DelId)
        {
            UF result = UFs.Find(x => x.Id == DelId);
            UFs.Remove(result);
            return UFs;
        }
    }
}
