using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Treino_REST_02.Controllers
{
    /// <summary>
    /// Cálculo do fator de vencimento de boletos bancários
    /// </summary>
    [Route("Utils/FatorVencimentoFEBRABAN")]
    [ApiController]
    public class FebrabanFatorVencimento : ControllerBase
    {

        DateTime dtBase = new DateTime(1997, 10, 07);

        /// <summary>
        /// Transforma uma data em fator de vencimento de boleto bancário
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet("FatorVencimento/{data:datetime}", Name = "FatorVencimento")]
        [ProducesResponseType(200)]
        public ActionResult<int> FatorVencimento(DateTime data)
        {
            int ret = 0;
            ret = (int)(data - dtBase).TotalDays;
            // Verifica se a data é válida
            if (ret < 0)
            {
                return BadRequest(ret);
            }
            // Ajuste para datas após 22/02/2025
            if (ret > 9999)
            {
                ret -= 9000;
            }
            return Ok(ret);
        }
    }
}
