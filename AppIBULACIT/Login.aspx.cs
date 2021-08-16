using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppIBULACIT.Models;
using AppIBULACIT.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Security;

namespace AppIBULACIT
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                try
                {
                    LoginRequest loginRequest = new LoginRequest() { Username = txtUsername.Text, Password = txtPassword.Text };
                    UsuarioManager usuarioManager = new UsuarioManager();
                    Usuario usuario = new Usuario();
                    usuario = await usuarioManager.Autenticar(loginRequest);

                    if (usuario != null)
                    {
                        JwtSecurityToken jwtSecurityToken;
                        var jwtHandler = new JwtSecurityTokenHandler();
                        jwtSecurityToken = jwtHandler.ReadJwtToken(usuario.Token);

                        Session["CodigoUsuario"] = usuario.Codigo;
                        Session["Identificacion"] = usuario.Identificacion;
                        Session["Nombre"] = usuario.Nombre;
                        Session["Email"] = usuario.Email;
                        Session["Estado"] = usuario.Estado;
                        Session["Token"] = usuario.Token;

                        SesionManager sesionManager = new SesionManager();
                        Sesion sesion = new Sesion()
                        {
                            CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"]),
                            FechaInicio = DateTime.Now,
                            FechaExpiracion = DateTime.Now,
                            Estado = "A"
                        };
                        Sesion sesionIngresada = await sesionManager.Ingresar(sesion, Convert.ToString(Session["Token"]));

                        EstadisticaManager estadisticaManager = new EstadisticaManager();
                        Estadistica estadistica = new Estadistica()
                        {
                            CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"]),
                            FechaHora = DateTime.Now,
                            Navegador = HttpContext.Current.Request.Browser.Browser,
                            PlataformaDispositivo = Environment.OSVersion.ToString(),
                            FabricanteDispositivo = "-",
                            Vista = "Login.aspx",
                            Accion = "btnAceptar_Click"                            
                        };
                        Estadistica estadisticaIngresada = await estadisticaManager.Ingresar(estadistica, Convert.ToString(Session["Token"]));

                        FormsAuthentication.RedirectFromLoginPage(usuario.Username, false);
                    }
                    else
                    {
                        lblStatus.Text = "Credenciales invalidas";
                        lblStatus.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager errorManager = new ErrorManager();
                    Error error = new Error()
                    {
                        CodigoUsuario = 0,
                        FechaHora = DateTime.Now,
                        Vista = "Login.aspx",
                        Accion = "btnAceptar_Click",
                        Fuente = ex.Source,
                        Numero = ex.HResult.ToString(),
                        Descripcion = ex.Message
                    };

                    Error errorIngresado = await errorManager.Ingresar(error);

                    lblStatus.Text = "Hubo un error al iniciar sesion. Contacte al administrador del sistema";
                    lblStatus.Visible = true;
                }
            }
        }
    }
}