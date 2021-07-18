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
    [AllowAnonymous]
    [RoutePrefix("api/Transferencia")]
    public class TransferenciaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Transferencia transferencia = new Transferencia();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CuentaOrigen, CuentaDestino, FechaHora, Descripcion, Monto, Estado
                                                             FROM Transferencia
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        transferencia.Codigo = sqlDataReader.GetInt32(0);
                        transferencia.CuentaOrigen = sqlDataReader.GetInt32(1);
                        transferencia.CuentaDestino = sqlDataReader.GetInt32(2);
                        transferencia.FechaHora = sqlDataReader.GetDateTime(3);
                        transferencia.Descripcion = sqlDataReader.GetString(4);
                        transferencia.Monto = sqlDataReader.GetDecimal(5);
                        transferencia.Estado = sqlDataReader.GetString(6);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(transferencia);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Transferencia> transferencias = new List<Transferencia>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT  Codigo, CuentaOrigen, CuentaDestino, FechaHora, Descripcion, Monto, Estado
                                                             FROM Transferencia", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Transferencia transferencia = new Transferencia();
                        transferencia.Codigo = sqlDataReader.GetInt32(0);
                        transferencia.CuentaOrigen = sqlDataReader.GetInt32(1);
                        transferencia.CuentaDestino = sqlDataReader.GetInt32(2);
                        transferencia.FechaHora = sqlDataReader.GetDateTime(3);
                        transferencia.Descripcion = sqlDataReader.GetString(4);
                        transferencia.Monto = sqlDataReader.GetDecimal(5);
                        transferencia.Estado = sqlDataReader.GetString(6);
                        transferencias.Add(transferencia);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(transferencias);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Transferencia transferencia)
        {
            if (transferencia == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Transferencia (CuentaOrigen, CuentaDestino, FechaHora, Descripcion, Monto, Estado)
                                                             VALUES (@CuentaOrigen, @CuentaDestino, @FechaHora, @Descripcion, @Monto, @Estado) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CuentaOrigen", transferencia.CuentaOrigen);
                    sqlCommand.Parameters.AddWithValue("@CuentaDestino", transferencia.CuentaDestino);
                    sqlCommand.Parameters.AddWithValue("@FechaHora", transferencia.FechaHora);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", transferencia.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Monto", transferencia.Monto);
                    sqlCommand.Parameters.AddWithValue("@Estado", transferencia.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(transferencia);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Transferencia transferencia)
        {
            if (transferencia == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Transferencia SET CuentaOrigen = @CuentaOrigen,
                                                                                      CuentaDestino = @CuentaDestino,
                                                                                      FechaHora = @FechaHora,
                                                                                      Descripcion = @Descripcion,
                                                                                      Monto = @Monto,
                                                                                      Estado = @Estado

                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", transferencia.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CuentaOrigen", transferencia.CuentaOrigen);
                    sqlCommand.Parameters.AddWithValue("@CuentaDestino", transferencia.CuentaDestino);
                    sqlCommand.Parameters.AddWithValue("@FechaHora", transferencia.FechaHora);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", transferencia.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Monto", transferencia.Monto);
                    sqlCommand.Parameters.AddWithValue("@Estado", transferencia.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(transferencia);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Transferencia WHERE Codigo = @Codigo ", sqlConnection);

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