using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text;
using System.Text.Json;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    /// <summary>
    /// Ambiente de testes para manipulãção de WhatsApp
    /// </summary>
    [Route("WhatsApp")]
    [ApiController]
    public class WhatsApp : ControllerBase
    {

        IConfiguration Config;
        SqlConnection Cn = new SqlConnection();
        SqlCommand Cm = new SqlCommand();
        SqlDataAdapter DaG = new SqlDataAdapter();

        /// <summary>
        /// Construtor - Inicia os objetos de acesso a dados
        /// </summary>
        /// <param name="_config"></param>
        public WhatsApp(IConfiguration _config)
        {
            Config = _config;
            Cn.ConnectionString = Config.GetConnectionString("cnExt");
            Cm.Connection = Cn;
            DaG.SelectCommand = Cm;
        }

        /// <summary>
        /// Envia mensagem WhatsApp
        /// </summary>
        /// <param name="TemplateName">Nome do template.  Se deixar em branco entra Hello World.</param>
        /// <returns></returns>
        [HttpPost("EnviaTemplate", Name = "EnviaTemplate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<WhatsAppSendMessageResponse> EnviaTemplate(string TemplateName)
        {
            WhatsAppSendMessageResponse ret = new();
            string Token = "EAANWDBUky7oBO7JDmTbaocE68wFKfzrhxK6Fjw0D1SzZChM0brwUxA2JZCcrm42ZADwa7Q7ZAi0Fc5GZCph0jYJcpb1GjRuXBbXS0us5I5P4lfM0o5kMOm4IMiQ0ShJKoDB6UYKU5hqxRnZAwlJfQP6q5X5HBPFncAWdRH6sNsB5ZCIF1m7ZBqqHKgZBHGOr1ZBmO49EWkbU9nlCSW5ermFa1gLoIY38ZC2S39smknYfFZBs";
            string PhoneNumberId = "553845751139295";
            string Destination = "5521982725777";

            if (TemplateName == "-" ) { TemplateName = "hello_world"; }
            string endPoint = $"https://graph.facebook.com/v21.0/{PhoneNumberId}/messages";
            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = HttpMethod.Post.ToString();
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Token);
            request.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var postData = "messaging_product=" + Uri.EscapeDataString("whatsapp");
            postData += "&to=" + Uri.EscapeDataString(Destination);
            postData += "&type=" + Uri.EscapeDataString("template");
            postData += "&template=" + Uri.EscapeDataString("{ \"name\": \"" + TemplateName + "\", \"language\": { \"code\": \"en_US\" } }");
            var data = Encoding.ASCII.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

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
                            ret =JsonSerializer.Deserialize<WhatsAppSendMessageResponse>(reader.ReadToEnd());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string res = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                return BadRequest(res);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return Ok(ret);
        }

        /// <summary>
        /// Verifica número
        /// </summary>
        /// <returns></returns>
        [HttpPost("Verify", Name = "Verify")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Verify()
        {
            string ret = string.Empty;
            string PhoneNumberId = "513021285231408";
            string Destination = "5521982725777";
            string Cert = "CnEKLQiFg+fv6rnSAhIGZW50OndhIhRMYXJzb2Z0IEluZm9ybcOhZ2ljYVDCo8G7BhpA5676z0ZFAbo4DHVzmdfOiJBcCyhk+PAmA65A+P8dbBu8Bg37ZzwlyBhVX7/QTOTU0ym5BVxUYjKN3qRLyk0uBBIvbVVv+7qbp1/gRYu3mq1kIJNc5uddxPAFhG9A84sc/IDT0NFRS+0gaLlWSSOujIA=";

            string endPoint = $"https://graph.facebook.com/v1/account";
            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = HttpMethod.Post.ToString();
            request.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var postData = "cc=55";
            postData += "&phone_number=21996622110";
            postData += "&method=sms";
            postData += "&cert=" + Uri.EscapeDataString(Cert);
            var data = Encoding.ASCII.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

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
                string res = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                return BadRequest(res);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return Ok(ret);
        }

        /// <summary>
        /// Webhook para respostas do WhatsApp
        /// </summary>
        /// <param name="Conteudo"></param>
        /// <returns></returns>
        [HttpGet("Recebe/{Conteudo:required}", Name = "Recebe")]
        public ActionResult<string> Recebe(string Conteudo)
        {
            Cm.Parameters.Add("@Conteudo",System.Data.SqlDbType.NVarChar).Value = Conteudo;
            Cm.CommandText = """
                INSERT INTO waWebHook (Conteudo) VALUES (@Conteudo)
                """;
            Cn.Open();
            Cm.ExecuteNonQuery();
            Cn.Close();
            return Ok(Conteudo);
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
