using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

}
