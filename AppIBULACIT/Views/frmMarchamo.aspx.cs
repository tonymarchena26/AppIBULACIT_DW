using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
namespace AppIBULACIT.Views
{
    public partial class frmMarchamo : System.Web.UI.Page
    {
        IEnumerable<Marchamo> marchamos = new ObservableCollection<Marchamo>();
        MarchamoManager marchamoManager = new MarchamoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    InicializarControles();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                marchamos = await marchamoManager.ObtenerMarchamos(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                gvMarchamos.DataSource = marchamos.ToList();
                gvMarchamos.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de marchamos.";
                lblStatus.Visible = true;
            }
        }

        protected void gvMarchamos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvMarchamos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    
                    break;
                case "Eliminar":
                    
                    break;
                default:
                    break;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo marchamo";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            ddlEstadoMant.Enabled = false;
            txtDescripcion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblCodigo.Text))//Insertar
                {
                    Console.WriteLine("insertar");
                    string cod = Session["CodigoUsuario"].ToString();
                    Marchamo marchamo = new Marchamo()
                    {
                        Descripcion = txtDescripcion.Text,
                        Estado = ddlEstadoMant.SelectedValue,
                        CodigoUsuario = Convert.ToInt32(cod),
                        MontoMarchamo = Convert.ToInt32(txtMontoMarchamo.Text)
                    };

                    Marchamo marchamoIngresado = await marchamoManager.Ingresar(marchamo, Session["Token"].ToString());
                    Console.WriteLine(marchamoIngresado);

                    if (marchamoIngresado != null)
                    {
                        lblResultado.Text = "Marchamo ingresado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
                else //Modificar
                {
                    Marchamo marchamo = new Marchamo()
                    {
                        Descripcion = txtDescripcion.Text,
                        Estado = ddlEstadoMant.SelectedValue
                    };

                    Marchamo marchamoModificado = await marchamoManager.Actualizar(marchamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(marchamoModificado.Descripcion))
                    {
                        lblResultado.Text = "Servicio actualizado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmServicio.aspx",
                    Accion = "btnAceptarModal_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult.ToString(),
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }
    }
}