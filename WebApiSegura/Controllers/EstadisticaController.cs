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
    [RoutePrefix("api/Estadistica")]
    public class EstadisticaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Estadistica estadistica = new Estadistica();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, FechaHora, Navegador, PlataformaDispositivo, FabricanteDispositivo, Vista, Accion
                                                             FROM Estadistica
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        estadistica.Codigo = sqlDataReader.GetInt32(0);
                        estadistica.CodigoUsuario = sqlDataReader.GetInt32(1);
                        estadistica.FechaHora = sqlDataReader.GetDateTime(2);
                        estadistica.Navegador = sqlDataReader.GetString(3);
                        estadistica.PlataformaDispositivo = sqlDataReader.GetString(4);
                        estadistica.FabricanteDispositivo = sqlDataReader.GetString(5);
                        estadistica.Vista = sqlDataReader.GetString(6);
                        estadistica.Accion = sqlDataReader.GetString(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(estadistica);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Estadistica> estadisticas = new List<Estadistica>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, FechaHora, Navegador, PlataformaDispositivo, FabricanteDispositivo, Vista, Accion
                                                             FROM Estadistica", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Estadistica estadistica = new Estadistica();
                        estadistica.Codigo = sqlDataReader.GetInt32(0);
                        estadistica.CodigoUsuario = sqlDataReader.GetInt32(1);
                        estadistica.FechaHora = sqlDataReader.GetDateTime(2);
                        estadistica.Navegador = sqlDataReader.GetString(3);
                        estadistica.PlataformaDispositivo = sqlDataReader.GetString(4);
                        estadistica.FabricanteDispositivo = sqlDataReader.GetString(5);
                        estadistica.Vista = sqlDataReader.GetString(6);
                        estadistica.Accion = sqlDataReader.GetString(7);
                        estadisticas.Add(estadistica);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(estadisticas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Estadistica estadistica)
        {
            if (estadistica == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Estadistica (CodigoUsuario, FechaHora, Navegador, PlataformaDispositivo, FabricanteDispositivo, Vista, Accion)
                                                             VALUES (@CodigoUsuario, @FechaHora, @Navegador, @PlataformaDispositivo, @FabricanteDispositivo, @Vista, @Accion) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", estadistica.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaHora", estadistica.FechaHora);
                    sqlCommand.Parameters.AddWithValue("@Navegador", estadistica.Navegador);
                    sqlCommand.Parameters.AddWithValue("@PlataformaDispositivo", estadistica.PlataformaDispositivo);
                    sqlCommand.Parameters.AddWithValue("@FabricanteDispositivo", estadistica.FabricanteDispositivo);
                    sqlCommand.Parameters.AddWithValue("@Vista", estadistica.Vista);
                    sqlCommand.Parameters.AddWithValue("@Accion", estadistica.Accion);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(estadistica);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Estadistica estadistica)
        {
            if (estadistica == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Estadistica SET CodigoUsuario = @CodigoUsuario,
                                                                             FechaHora = @FechaHora,
                                                                             Navegador = @Navegador,
                                                                             PlataformaDispositivo = @PlataformaDispositivo,
                                                                             FabricanteDispositivo = @FabricanteDispositivo,
                                                                             Vista = @Vista,
                                                                             Accion = @Accion
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", estadistica.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", estadistica.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaHora", estadistica.FechaHora);
                    sqlCommand.Parameters.AddWithValue("@Navegador", estadistica.Navegador);
                    sqlCommand.Parameters.AddWithValue("@PlataformaDispositivo", estadistica.PlataformaDispositivo);
                    sqlCommand.Parameters.AddWithValue("@FabricanteDispositivo", estadistica.FabricanteDispositivo);
                    sqlCommand.Parameters.AddWithValue("@Vista", estadistica.Vista);
                    sqlCommand.Parameters.AddWithValue("@Accion", estadistica.Accion);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(estadistica);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Estadistica WHERE Codigo = @Codigo ", sqlConnection);

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
