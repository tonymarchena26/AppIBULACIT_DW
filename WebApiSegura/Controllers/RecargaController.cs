using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/Recarga")]
    public class RecargaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Recarga recarga = new Recarga();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Propietario, Telefono, Monto
                                                             FROM Recarga
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        recarga.Codigo = sqlDataReader.GetInt32(0);
                        recarga.Propietario = sqlDataReader.GetString(1);
                        recarga.Telefono = sqlDataReader.GetInt32(2);
                        recarga.Monto = sqlDataReader.GetDecimal(3);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(recarga);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Recarga> recargas = new List<Recarga>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Propietario, Telefono, Monto
                                                             FROM Recarga", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Recarga recarga = new Recarga();
                        recarga.Codigo = sqlDataReader.GetInt32(0);
                        recarga.Propietario = sqlDataReader.GetString(1);
                        recarga.Telefono = sqlDataReader.GetInt32(2);
                        recarga.Monto = sqlDataReader.GetDecimal(3);
                        recargas.Add(recarga);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(recargas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Recarga recarga)
        {
            if (recarga == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Recarga (Propietario, Telefono, Monto)
                                                             VALUES (@Propietario, @Telefono, @Monto) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Propietario", recarga.Propietario);
                    sqlCommand.Parameters.AddWithValue("@Telefono", recarga.Telefono);
                    sqlCommand.Parameters.AddWithValue("@Monto", recarga.Monto);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(recarga);
        }
        [HttpPut]
        public IHttpActionResult Actualizar(Recarga recarga)
        {
            if (recarga == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Recarga SET Propietario = @Propietario,
                                                                                Telefono = @Telefono,
                                                                                Monto = @Monto
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", recarga.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Propietario", recarga.Propietario);
                    sqlCommand.Parameters.AddWithValue("@Telefono", recarga.Telefono);
                    sqlCommand.Parameters.AddWithValue("@Monto", recarga.Monto);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(recarga);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Recarga WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(id);
        }
    }
}
