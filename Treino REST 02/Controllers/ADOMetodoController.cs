using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography;
using Treino_REST_02.Models;

namespace Treino_REST_02.Controllers
{
    [Route("api/ADO")]
    [ApiController]
    public class ADOMetodoController : ControllerBase
    {
        public List<UF> UFs = new List<UF>();
        SqlConnection Cn = new SqlConnection();
        SqlCommand Cm = new SqlCommand();
        SqlDataAdapter DaG = new SqlDataAdapter();

        public ADOMetodoController()
        {
            Cn.ConnectionString = "Server=localhost;Database=Treino01;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
            Cm.Connection = Cn;
            DaG.SelectCommand = Cm;
        }

        [HttpGet(Name = "ListaUF-ADO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> ListaUF()
        {
            List<UF> ret = new List<UF>();
            Cm.CommandText = "SELECT Id, UF, Capital FROM Capitais ORDER BY Id";
            Cn.Open();
            SqlDataReader rd = Cm.ExecuteReader();
            while (rd.Read())
            {
                UF vUF = new UF { Id = 0, Nome = "", Capital = ""};
                vUF.Id = Convert.ToInt32(rd["Id"]);
                vUF.Nome = rd["UF"].ToString();
                vUF.Capital = rd["Capital"].ToString();
                ret.Add(vUF);
            }
            Cn.Close();

            return Ok(ret);
        }

        [HttpGet("{IdUF:int}", Name = "MostraUF-ADO")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UF> MostraUF(int IdUF)
        {
            if (IdUF < 0)
            {
                return BadRequest();
            }
            UF vUF = new UF { Id = 0, Nome = "", Capital = "" };
            Cm.Parameters.Clear();
            Cm.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = IdUF;
            Cm.CommandText = "SELECT Id, UF, Capital FROM Capitais WHERE Id = @Id";
            DataTable dt = new DataTable();
            DaG.Fill(dt);
            if(dt.Rows.Count == 0)
            {
                return NotFound();
            }
            DataRow rd = dt.Rows[0];
            vUF.Id = Convert.ToInt32(rd["Id"]);
            vUF.Nome = rd["UF"].ToString();
            vUF.Capital = rd["Capital"].ToString();
            return Ok(vUF);
        }

        [HttpPost(Name = "Adiciona-ADO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AdicionaUF(UF NovaUF)
        {
            Cm.Parameters.Clear();
            Cm.Parameters.Add("@Nome", SqlDbType.Char, 2).Value = NovaUF.Nome;
            Cm.Parameters.Add("@Capital",SqlDbType.VarChar,50).Value = NovaUF.Capital;
            Cm.CommandText = "INSERT INTO Capitais (UF, Capital) VALUES (@Nome, @Capital) ";
            try
            {
                Cn.Open();
                Cm.ExecuteNonQuery();
                Cn.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return ListaUF();
        }

        [HttpPut(Name = "Altera-ADO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> AtualizaUF(int IdUF, string NomeUF, string CapitalUF)
        {
            Cm.Parameters.Clear();
            Cm.Parameters.Add("@Id", SqlDbType.Int, 2).Value = IdUF;
            Cm.Parameters.Add("@Nome", SqlDbType.Char, 2).Value = NomeUF;
            Cm.Parameters.Add("@Capital", SqlDbType.VarChar, 50).Value = CapitalUF;
            Cm.CommandText = "UPDATE Capitais SET UF = @Nome, Capital = @Capital WHERE Id = @Id ";
            try
            {
                Cn.Open();
                Cm.ExecuteNonQuery();
                Cn.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return ListaUF();
        }

        [HttpPatch(Name = "MudaCapital-ADO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UF>> MudaCapital(string NomeUF, string NovaCapital)
        {
            Cm.Parameters.Clear();
            Cm.Parameters.Add("@Nome", SqlDbType.Char, 2).Value = NomeUF;
            Cm.Parameters.Add("@Capital", SqlDbType.VarChar, 50).Value = NovaCapital;
            Cm.CommandText = "UPDATE Capitais SET Capital = @Capital WHERE UF = @Nome ";
            try
            {
                Cn.Open();
                Cm.ExecuteNonQuery();
                Cn.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return ListaUF();
        }

        [HttpDelete(Name = "EliminaUF-ADO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UF>> EliminaUF(int DelId)
        {
            Cm.Parameters.Clear();
            Cm.Parameters.Add("@Id", SqlDbType.Int, 2).Value = DelId;
            Cm.CommandText = "DELETE FROM Capitais WHERE Id = @Id ";
            try
            {
                Cn.Open();
                Cm.ExecuteNonQuery();
                Cn.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return ListaUF();
        }

    }
}
