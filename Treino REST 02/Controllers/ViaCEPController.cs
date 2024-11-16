using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaCEPController : ControllerBase
    {

        [Route("api/ConsultaCEP")]
        [ApiController]
        public class ConsultaCEPController : ControllerBase
        {

            [HttpGet(Name = "Consulta")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<CEP> Consulta(string Codigo)
            {
                string endPoint = "https://viacep.com.br/ws/" + Codigo + "/json";
                string ResponseValue = string.Empty;

                ResponseValue = RequisitaAPI(endPoint);
                if (ResponseValue.Contains("errorMessages"))
                {
                    return BadRequest(ResponseValue);
                }
                ViaCEP dto = JsonSerializer.Deserialize<ViaCEP>(ResponseValue);
                if (dto.cep == null)
                {
                    return NotFound();
                }
                CEP ret = new CEP();
                ret.Codigo = dto.cep;
                ret.Logradouro = dto.logradouro;
                ret.LogradouroTipo = dto.logradouro.Substring(0, dto.logradouro.IndexOf(" "));
                ret.LogradouroNome = dto.logradouro.Substring(dto.logradouro.IndexOf(" ") + 1);
                ret.Bairro = dto.bairro;
                ret.Cidade = dto.localidade;
                ret.UF = dto.uf;
                ret.Estado = dto.estado;
                ret.DDD = dto.ddd;
                ret.Regiao = dto.regiao;
                return Ok(ret);
            }

            private string RequisitaAPI(string endPoint)
            {
                return RequisitaAPI(endPoint, HttpMethod.Get);
            }

            private string RequisitaAPI(string endPoint, HttpMethod Verbo)
            {
                string ret = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create(endPoint);
                request.Method = Verbo.ToString();
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                ret = reader.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ret = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                }
                finally
                {
                    if (response != null)
                    {
                        ((IDisposable)response).Dispose();
                    }
                }
                return ret;
            }

        }
    }
}
