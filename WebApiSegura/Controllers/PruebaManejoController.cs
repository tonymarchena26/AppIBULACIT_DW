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
    public class PruebaManejoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            List<PruebasManejo> marchamos = new List<PruebasManejo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Monto, Descripcion, DerechoMatricula
                                                             FROM PruebasManejo
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        PruebasManejo prueba = new PruebasManejo();
                        prueba.Codigo = sqlDataReader.GetInt32(0);
                        prueba.Monto = sqlDataReader.GetInt32(1);
                        prueba.Descripcion = sqlDataReader.GetString(2);
                        prueba.DerechoMatricula = sqlDataReader.GetString(3);
                      
                        marchamos.Add(prueba);
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

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<PruebasManejo> marchamos = new List<PruebasManejo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Monto, Descripcion, DerechoMatricula
                                                             FROM PruebasManejo", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        PruebasManejo prueba = new PruebasManejo();
                        
                        prueba.Codigo = sqlDataReader.GetInt32(0);
                        prueba.Monto = sqlDataReader.GetInt32(1);
                        prueba.Descripcion = sqlDataReader.GetString(2);
                        prueba.DerechoMatricula = sqlDataReader.GetString(3);
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
        public IHttpActionResult Ingresar(PruebasManejo NewPrueba)
        {
            if (NewPrueba == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PruebasManejo (Codigo, Monto, Descripcion, DerechoMatricula)
                                                             VALUES (@Codigo, @Monto, @Descripcion,@DerechoMatricula) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", NewPrueba.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Cedula", NewPrueba.Monto);
                    sqlCommand.Parameters.AddWithValue("@TipoInfraccion", NewPrueba.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Monto", NewPrueba.DerechoMatricula);
                    

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(NewPrueba);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(PruebasManejo UpdateInfraccion)
        {
            if (UpdateInfraccion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE PruebasManejo SET Codigo = @Codigo, Monto = @Monto, Descripcion = @Descripcion,DerechoMatricula = @DerechoMatricula
                                                             WHERE Codigo = @Codigo ", sqlConnection);


                    sqlCommand.Parameters.AddWithValue("@Codigo", UpdateInfraccion.Codigo);
                    sqlCommand.Parameters.AddWithValue("@Monto", UpdateInfraccion.Monto);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", UpdateInfraccion.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@DerechoMatricula", UpdateInfraccion.DerechoMatricula);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(UpdateInfraccion);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE PruebasManejo WHERE Codigo = @Codigo ", sqlConnection);

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
