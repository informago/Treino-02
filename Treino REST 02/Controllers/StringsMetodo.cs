using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Treino_REST_02.Controllers
{
    /// <summary>
    /// Teste de vários métodos para manipular strings
    /// </summary>
    [Route("api/StringsMetodo")]
    [ApiController]
    public class StringsMetodo : ControllerBase
    {
        /// <summary>
        /// Métodos de manipulação de multi-line strings
        /// </summary>
        /// <remarks>
        /// O parâmetro método deve ser preenchido com:<br/>
        /// 0: Padrão<br/>
        /// 1: Verbatim<br/>
        /// 2: Raw<br/>
        /// 3: Verbatim+Raw<br/>
        /// 4: Interpolation<br/>
        /// 5: Interpolation+Verbatim<br/>
        /// </remarks>
        /// <param name="Metodo"></param>
        /// <returns></returns>
        [HttpGet(Name = "Multi-line")]
        [ProducesResponseType(200)]
        public ActionResult<string> Multiline(string Metodo)
        {
            string ret = string.Empty;
            switch (Metodo)
            {
                case "0": // Padrão
                    ret = StringMetodoPadrao();
                    break;
                case "1": // Verbatim
                    ret = StringMetodoVerbatim();
                    break;
                case "2": // Raw
                    ret = StringMetodoRaw();
                    break;
                case "3": // Verbatim + Raw
                    ret = StringMetodoVerbatimRaw();
                    break;
                case "4": // Interpolation
                    ret = StringMetodoInterpolationRaw();
                    break;
                case "5": // Interpolation + Verbatim
                    ret = StringMetodoInterpolationVerbatim();
                    break;
            }
            return Ok(ret);
        }

        private string StringMetodoPadrao()
        {
            string temp = "SELECT Nome, Endereco, Telefone " +
                "FROM Cadastro " +
                "WHERE Id = 10 ";
            return temp;
        }

        private string StringMetodoVerbatim()
        {
            string temp = @"SELECT Nome, Endereco, Telefone
FROM Cadastro
WHERE Id = 10";
            return temp;
        }

        private string StringMetodoRaw()
        {
            string temp = """
                SELECT Nome, Endereco, Telefone 
                FROM Cadastro 
                WHERE Id = 10
                """;
            return temp;
        }

        private string StringMetodoVerbatimRaw()
        {
            return string.Empty;
        }

        private string StringMetodoInterpolationRaw()
        {
            string id = "10";
            string temp = $"""
                SELECT Nome, Endereco, Telefone
                FROM Cadastro
                WHERE Id = {id}
                """;
            string temp2 = $$"""
                SELECT Nome, Endereco, Telefone
                FROM Cadastro 
                WHERE id = {{id}}
                """;
            string temp3 = $""""
                SELECT Nome, Endereco, Telefone 
                FROM Cadastro
                WHERE id = {id}
                """";
            return temp + "\n" + temp2 + "\n" + temp3;
        }

        private string StringMetodoInterpolationVerbatim()
        {
            return string.Empty;
        }
    }
}
