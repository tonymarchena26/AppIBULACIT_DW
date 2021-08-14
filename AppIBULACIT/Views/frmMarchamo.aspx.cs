using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
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

        IEnumerable<Cuenta> cuentas = new ObservableCollection<Cuenta>();
        CuentaManager cuentaManager = new CuentaManager();

        public string labelsGraficoVistasGlobal = string.Empty;
        public string dataGraficoVistasGlobal = string.Empty;
        public string backgroundcolorsGraficoVistasGlobal = string.Empty;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    marchamos = await marchamoManager.ObtenerMarchamos(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                    InicializarControles();
                    ObtenerDatosGrafico();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                marchamos = await marchamoManager.ObtenerMarchamos(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                gvMarchamos.DataSource = marchamos.ToList();
                gvMarchamos.DataBind();
                ObtenerDatosGrafico();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de marchamos.";
                lblStatus.Visible = true;
            }
        }

        protected async void gvMarchamos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvMarchamos.Rows[index];

            switch (e.CommandName)
            {
                case "Pagar":

                    CuentaDrop.Items.Clear();

                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    lblCodigo.Text = row.Cells[0].Text.Trim();
                    txtDescripcion.Text = row.Cells[1].Text.Trim();
                    ltrCuenta.Visible = true;
                    ddlEstadoMant.SelectedValue = row.Cells[2].Text.Trim();
                    txtMontoMarchamo.Text = row.Cells[3].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ddlEstadoMant.Enabled = false;
                    CuentaDrop.Visible = true;

                    cuentas = await cuentaManager.ObtenerCuentas(Session["Token"].ToString());
                    foreach (Cuenta c in cuentas) {
                        if (c.CodigoUsuario.ToString() == Session["CodigoUsuario"].ToString()) {
                            string linea = Convert.ToString(c.Codigo)+ " - " + c.Saldo;
                            CuentaDrop.Items.Add(new ListItem(linea));
                        }
                    }
                    
                    ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);

                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    btnAceptarModal.Visible = true;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el marchamo # " + lblCodigoEliminar.Text + "?";
                    ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

            lblCodigo.Text = "";
            txtDescripcion.Text = "";
            ltrCuenta.Visible = false;
            CuentaDrop.Visible = false;
            ddlEstadoMant.SelectedValue = default;
            txtMontoMarchamo.Text = "";
            ddlEstadoMant.Enabled = false;

            ltrTituloMantenimiento.Text = "Nuevo marchamo";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            ddlEstadoMant.Enabled = false;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        private bool checkEmpty() {

            if (txtMontoMarchamo.Text == "" || txtMontoMarchamo.Text == null)
            {
                return true;
            }
            else {
                if (txtDescripcion.Text == "" || txtDescripcion.Text == null)
                {
                    return true;
                }
            } 

            return false;
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblCodigo.Text))//Insertar
                {
                    bool vacios = checkEmpty();
                    if (vacios == false)
                    {
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

                        if (marchamoIngresado.Descripcion != null)
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
                    else {
                        lblResultado.Text = "Revise que no haya campos vacios";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Orange;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }

                }
                else //Pagar
                {
                    if (ddlEstadoMant.SelectedValue == "N")
                    {
                        string a = CuentaDrop.SelectedItem.Text;
                        string[] cuentaSel = a.Split('-');
                        Cuenta cuentaSeleccionada = await cuentaManager.ObtenerCuenta(Session["Token"].ToString(), cuentaSel[0].Trim());
                        cuentaSeleccionada.Saldo = Convert.ToInt32(cuentaSeleccionada.Saldo) - Convert.ToInt32(txtMontoMarchamo.Text);

                        Cuenta cuentaModificada = await cuentaManager.Actualizar(cuentaSeleccionada, Session["Token"].ToString());

                        string cod = Session["CodigoUsuario"].ToString();
                        Marchamo marchamo = new Marchamo()
                        {
                            Codigo = Convert.ToInt32(lblCodigo.Text),
                            Descripcion = txtDescripcion.Text,
                            Estado = "P",
                            MontoMarchamo = Convert.ToInt32(txtMontoMarchamo.Text),
                            CodigoUsuario = Convert.ToInt32(cod)
                        };

                        Console.WriteLine(marchamo);

                        Marchamo marchamoModificado = await marchamoManager.Actualizar(marchamo, Session["Token"].ToString());

                        if (marchamoModificado.Descripcion != null)
                        {
                            lblResultado.Text = "Marchamo actualizado con exito";
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
                    else 
                    {
                        lblResultado.Text = "El marchamo ya se ha pagado";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Blue;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
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

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await marchamoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "Marchamo eliminado";
                    btnAceptarModal.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
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
                    Vista = "frmMarchamo.aspx",
                    Accion = "btnAceptarModal_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult.ToString(),
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }

        private void ObtenerDatosGrafico()
        {
            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();


            foreach (var marchamo in marchamos.GroupBy(info => info.MontoMarchamo).Select(group => new { Codigo = group.Key, Cantidad = group.Key }).OrderBy(x => x.Codigo))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", marchamo.Codigo)); // 'Correo', 'frmError'.
                //dataGraficoVistas.Append(string.Format("'{0}',", marchamo.Cantidad)); // '2', '3'.
                dataGraficoVistas.Append(string.Format("'{0}',", marchamo.Cantidad)); // '2', '3'.


                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal = backgroundcolorsGraficoVistas.ToString().Substring(0, backgroundcolorsGraficoVistas.Length - 1);
            }


        }
    }
}