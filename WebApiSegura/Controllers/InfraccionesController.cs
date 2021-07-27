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
    public class InfraccionesController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            List<Infraccione> marchamos = new List<Infraccione>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Cedula, TipoInfraccion, Monto, Descripcion
                                                             FROM Infracciones
                                                             WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Infraccione marchamo = new Infraccione();
                        marchamo.Codigo = sqlDataReader.GetInt32(0);
                        marchamo.Cedula = sqlDataReader.GetInt32(1);
                        marchamo.TipoInfraccion = sqlDataReader.GetString(2);
                        marchamo.Monto = sqlDataReader.GetInt32(3);
                        marchamo.Descripcion = sqlDataReader.GetString(4);
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

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Infraccione> marchamos = new List<Infraccione>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Cedula, TipoInfraccion, Monto, Descripcion
                                                             FROM Infracciones", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Infraccione infraccion = new Infraccione();
                        infraccion.Codigo = sqlDataReader.GetInt32(0);
                        infraccion.Cedula = sqlDataReader.GetInt32(1);
                        infraccion.TipoInfraccion = sqlDataReader.GetString(2);
                        infraccion.Monto = sqlDataReader.GetInt32(3);
                        infraccion.Descripcion = sqlDataReader.GetString(4);
                        marchamos.Add(infraccion);
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
        public IHttpActionResult Ingresar(Infraccione NewInfraccion)
        {
            if (NewInfraccion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Infracciones (Codigo, Cedula, TipoInfraccion, Monto,Descripcion)
                                                             VALUES (@Codigo, @Cedula, @TipoInfraccion, @Monto,@Descripcion) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", NewInfraccion.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Cedula", NewInfraccion.Cedula);
                    sqlCommand.Parameters.AddWithValue("@TipoInfraccion", NewInfraccion.TipoInfraccion);
                    sqlCommand.Parameters.AddWithValue("@Monto", NewInfraccion.Monto);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", NewInfraccion.Descripcion);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(NewInfraccion);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Infraccione UpdateInfraccion)
        {
            if (UpdateInfraccion == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Infracciones SET Descripcion = @Descripcion, Cedula = @Cedula, TipoInfraccion = @TipoInfraccion,Monto = @Monto CodigoUsuario = @CodigoUsuario
                                                             WHERE Codigo = @Codigo ", sqlConnection);


                    sqlCommand.Parameters.AddWithValue("@Codigo", UpdateInfraccion.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Cedula", UpdateInfraccion.Cedula);
                    sqlCommand.Parameters.AddWithValue("@TipoInfraccion", UpdateInfraccion.TipoInfraccion);
                    sqlCommand.Parameters.AddWithValue("@Monto", UpdateInfraccion.Monto);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", UpdateInfraccion.Descripcion);

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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Infracciones WHERE Codigo = @Codigo ", sqlConnection);

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
