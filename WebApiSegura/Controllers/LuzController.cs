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
    [RoutePrefix("api/Luz")]
    public class LuzController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            List<Luz> luzFacturas = new List<Luz>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT * 
                                                             FROM Luz
                                                             WHERE CodigoUsuario = @CodigoUsuario", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Luz luzFactura = new Luz();
                        luzFactura.Codigo = sqlDataReader.GetInt32(0);
                        luzFactura.CodigoUsuario = sqlDataReader.GetInt32(1);
                        luzFactura.Descripcion = sqlDataReader.GetString(2);
                        luzFactura.Estado = sqlDataReader.GetString(3);
                        luzFactura.MontoEnergia = sqlDataReader.GetInt32(4);
                        luzFactura.MontoVariable = sqlDataReader.GetInt32(5);
                        luzFactura.MontoAlumbrado = sqlDataReader.GetInt32(6);
                        luzFacturas.Add(luzFactura);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(luzFacturas);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Luz> luzFacturas = new List<Luz>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT *
                                                             FROM Luz", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Luz luzFactura = new Luz();
                        luzFactura.Codigo = sqlDataReader.GetInt32(0);
                        luzFactura.CodigoUsuario = sqlDataReader.GetInt32(1);
                        luzFactura.Descripcion = sqlDataReader.GetString(2);
                        luzFactura.Estado = sqlDataReader.GetString(3);
                        luzFactura.MontoEnergia = sqlDataReader.GetInt32(4);
                        luzFactura.MontoVariable = sqlDataReader.GetInt32(5);
                        luzFactura.MontoAlumbrado = sqlDataReader.GetInt32(6);
                        luzFacturas.Add(luzFactura);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(luzFacturas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Luz luzFactura)
        {
            if (luzFactura == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Luz (CodigoUsuario, Descripcion, Estado, MontoEnergia, MontoVariable, MontoAlumbrado)
                                                             VALUES (@CodigoUsuario, @Descripcion, @Estado, @MontoEnergia, @MontoVariable, @MontoAlumbrado) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", luzFactura.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", luzFactura.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Estado", luzFactura.Estado);
                    sqlCommand.Parameters.AddWithValue("@MontoEnergia", luzFactura.MontoEnergia);
                    sqlCommand.Parameters.AddWithValue("@MontoVariable", luzFactura.MontoVariable);
                    sqlCommand.Parameters.AddWithValue("@MontoAlumbrado", luzFactura.MontoAlumbrado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(luzFactura);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Luz luzFactura)
        {
            if (luzFactura == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Luz 
                                                             SET CodigoUsuario = @CodigoUsuario, 
                                                                 Descripcion = @Descripcion, 
                                                                 Estado = @Estado, 
                                                                 MontoEnergia = @MontoEnergia, 
                                                                 MontoVariable = @MontoVariable, 
                                                                 MontoAlumbrado = @MontoAlumbrado
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", luzFactura.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", luzFactura.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", luzFactura.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Estado", luzFactura.Estado);
                    sqlCommand.Parameters.AddWithValue("@MontoEnergia", luzFactura.MontoEnergia);
                    sqlCommand.Parameters.AddWithValue("@MontoVariable", luzFactura.MontoVariable);
                    sqlCommand.Parameters.AddWithValue("@MontoAlumbrado", luzFactura.MontoAlumbrado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(luzFactura);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Luz WHERE Codigo = @Codigo ", sqlConnection);

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
