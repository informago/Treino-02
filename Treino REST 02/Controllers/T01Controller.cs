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
            UFs.Add(new UF { Id = 1, Nome = "RJ", Capital = "Rio de Janeiro" });
            UFs.Add(new UF { Id = 2, Nome = "SP", Capital = "São Paulo" });
            UFs.Add(new UF { Id = 3, Nome = "BA", Capital = "Salvador" });
        }

        [HttpGet(Name = "Divisão")]
        public IEnumerable<UF> ListaUF()
        {
            return UFs;
        }
    }
}
