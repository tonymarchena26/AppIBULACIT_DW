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
    [RoutePrefix("api/Sesion")]
    public class SesionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Sesion sesion = new Sesion();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, FechaInicio, FechaExpiracion, Estado
                                                             FROM Sesion
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        sesion.Codigo = sqlDataReader.GetInt32(0);
                        sesion.CodigoUsuario = sqlDataReader.GetInt32(1);
                        sesion.FechaInicio = sqlDataReader.GetDateTime(2);
                        sesion.FechaExpiracion = sqlDataReader.GetDateTime(3);
                        sesion.Estado = sqlDataReader.GetString(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(sesion);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Sesion> sesiones = new List<Sesion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, FechaInicio, FechaExpiracion, Estado
                                                             FROM Sesion", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Sesion sesion = new Sesion();
                        sesion.Codigo = sqlDataReader.GetInt32(0);
                        sesion.CodigoUsuario = sqlDataReader.GetInt32(1);
                        sesion.FechaInicio = sqlDataReader.GetDateTime(2);
                        sesion.FechaExpiracion = sqlDataReader.GetDateTime(3);
                        sesion.Estado = sqlDataReader.GetString(4);
                        sesiones.Add(sesion);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(sesiones);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Sesion sesion)
        {
            if (sesion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Sesion (CodigoUsuario, FechaInicio, FechaExpiracion, Estado)
                                                             VALUES (@CodigoUsuario, @FechaInicio, @FechaExpiracion, @Estado) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", sesion.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", sesion.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@FechaExpiracion", sesion.FechaExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Estado", sesion.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(sesion);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Sesion sesion)
        {
            if (sesion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Sesion SET CodigoUsuario = @CodigoUsuario,
                                                                             FechaInicio = @FechaInicio,
                                                                             FechaExpiracion = @FechaExpiracion,
                                                                             Estado = @Estado
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", sesion.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", sesion.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", sesion.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@FechaExpiracion", sesion.FechaExpiracion);
                    sqlCommand.Parameters.AddWithValue("@Estado", sesion.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(sesion);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Sesion WHERE Codigo = @Codigo ", sqlConnection);

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