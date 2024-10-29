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

        [HttpGet(Name = "Divisão")]
        public IEnumerable<UF> ListaUF()
        {
            return new List<UF>
            {
                new UF {Id = 1, Nome = "RJ", Capital = "Rio de Janeiro" },
                new UF {Id = 2, Nome = "SP", Capital = "São Paulo" },
                new UF {Id = 3, Nome = "BA", Capital = "Salvador" }
            };
        }
    }
}
