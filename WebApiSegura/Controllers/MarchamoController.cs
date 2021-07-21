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
    [RoutePrefix("api/Marchamo")]
    public class MarchamoController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Marchamo marchamo = new Marchamo();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Descripcion, Estado, CodigoCuenta, CodigoUsuario
                                                             FROM Marchamo
                                                             WHERE CodigoUsuario = @CodigoUsuario", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        marchamo.Codigo = sqlDataReader.GetInt32(0);
                        marchamo.Descripcion = sqlDataReader.GetString(1);
                        marchamo.Estado = sqlDataReader.GetString(2);
                        marchamo.CodigoCuenta = sqlDataReader.GetInt32(3);
                        marchamo.CodigoUsuario = sqlDataReader.GetInt32(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(marchamo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Marchamo> marchamos = new List<Marchamo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Descripcion, Estado, CodigoCuenta, CodigoUsuario
                                                             FROM Marchamo", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Marchamo marchamo = new Marchamo();
                        marchamo.Codigo = sqlDataReader.GetInt32(0);
                        marchamo.Descripcion = sqlDataReader.GetString(1);
                        marchamo.Estado = sqlDataReader.GetString(2);
                        marchamo.CodigoCuenta = sqlDataReader.GetInt32(3);
                        marchamo.CodigoUsuario = sqlDataReader.GetInt32(4);
                        marchamos.Add(marchamo);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(marchamos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Marchamo marchamo)
        {
            if (marchamo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Marchamo (Descripcion, Estado, CodigoCuenta, CodigoUsuario)
                                                             VALUES (@Descripcion, @Estado, @CodigoCuenta, @CodigoUsuario) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Descripcion", marchamo.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Estado", marchamo.Estado);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", marchamo.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", marchamo.CodigoUsuario);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(marchamo);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Marchamo marchamo)
        {
            if (marchamo == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Marchamo SET Descripcion = @Descripcion, Estado = @Estado, CodigoCuenta = @CodigoCuenta, CodigoUsuario = @CodigoUsuario
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", marchamo.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", marchamo.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Estado", marchamo.Estado);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", marchamo.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", marchamo.CodigoUsuario);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(marchamo);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Marchamo WHERE Codigo = @Codigo ", sqlConnection);

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
