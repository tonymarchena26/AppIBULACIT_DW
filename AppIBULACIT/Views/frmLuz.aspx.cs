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
    public partial class frmLuz : System.Web.UI.Page
    {
        IEnumerable<Luz> luzFacturas = new ObservableCollection<Luz>();
        LuzManager luzManager = new LuzManager();

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
                    InicializarControles();
                    luzFacturas = await luzManager.ObtenerLuzFacturas(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                    ObtenerDatosGrafico();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                luzFacturas = await luzManager.ObtenerLuzFacturas(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                gvLuzFacturas.DataSource = luzFacturas.ToList();
                gvLuzFacturas.DataBind();
                ObtenerDatosGrafico();

                foreach (GridViewRow r in gvLuzFacturas.Rows)
                {
                    r.Cells[7].Text += Convert.ToInt32(r.Cells[4].Text) + Convert.ToInt32(r.Cells[5].Text) + Convert.ToInt32(r.Cells[6].Text);
                }

            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de marchamos.";
                lblStatus.Visible = true;
            }
        }

        protected async void gvLuzFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvLuzFacturas.Rows[index];

            switch (e.CommandName)
            {
                case "Pagar":

                    CuentaDrop.Items.Clear();

                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    lblCodigo.Text = row.Cells[0].Text.Trim();
                    txtDescripcion.Text = row.Cells[2].Text.Trim();
                    ltrCuenta.Visible = true;
                    ddlEstadoMant.SelectedValue = row.Cells[3].Text.Trim();
                    txtMontoEnergia.Text = row.Cells[4].Text.Trim();
                    txtMontoVariable.Text = row.Cells[5].Text.Trim();
                    txtMontoAlumbrado.Text = row.Cells[6].Text.Trim();
                    txtMontoTotal.Text = row.Cells[7].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ddlEstadoMant.Enabled = false;
                    CuentaDrop.Visible = true;

                    cuentas = await cuentaManager.ObtenerCuentas(Session["Token"].ToString());
                    foreach (Cuenta c in cuentas)
                    {
                        if (c.CodigoUsuario.ToString() == Session["CodigoUsuario"].ToString())
                        {
                            string linea = Convert.ToString(c.Codigo) + " - " + c.Saldo;
                            CuentaDrop.Items.Add(new ListItem(linea));
                        }
                    }

                    ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "openModalMantenimiento();", true);
                    
                    break;
                case "Eliminar":
                    btnAceptarModal.Visible = true;
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el marchamo # " + lblCodigoEliminar.Text + "?";
                    ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "openModal();", true);
                    
                    break;
                default:
                    break;
            }
        }

        private bool checkEmpty()
        {

            if (txtDescripcion.Text == "" || txtDescripcion.Text == null)
            {
                return true;
            }
            else
            {
                if (txtMontoAlumbrado.Text == "" || txtMontoAlumbrado.Text == null)
                {
                    return true;
                }
                else 
                {
                    if (txtMontoEnergia.Text == "" || txtMontoEnergia.Text == null)
                    {
                        return true;
                    }
                    else 
                    {
                        if (txtMontoVariable.Text == "" || txtMontoVariable.Text == null)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = "";
            txtDescripcion.Text = "";
            ltrCuenta.Visible = false;
            CuentaDrop.Visible = false;
            ddlEstadoMant.SelectedValue = default;
            txtMontoEnergia.Text = "";
            txtMontoVariable.Text = "";
            txtMontoAlumbrado.Text = "";

            ltrTituloMantenimiento.Text = "Nuevo Pago de Luz";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            ddlEstadoMant.Enabled = false;
            txtDescripcion.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "openModalMantenimiento();", true);
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
                        Luz luzFactura = new Luz()
                        {
                            CodigoUsuario = Convert.ToInt32(cod),
                            Descripcion = txtDescripcion.Text,
                            Estado = ddlEstadoMant.SelectedValue,
                            MontoEnergia = Convert.ToInt32(txtMontoEnergia.Text),
                            MontoVariable = Convert.ToInt32(txtMontoVariable.Text),
                            MontoAlumbrado = Convert.ToInt32(txtMontoAlumbrado.Text)
                        };

                        Luz luzFacturaIngreada = await luzManager.Ingresar(luzFactura, Session["Token"].ToString());
                        Console.WriteLine(luzFacturaIngreada);

                        if (luzFacturaIngreada.Descripcion != null)
                        {
                            lblResultado.Text = "Factura de Luz ingresada con exito";
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
                        cuentaSeleccionada.Saldo = Convert.ToInt32(cuentaSeleccionada.Saldo) - Convert.ToInt32(txtMontoTotal.Text);

                        Cuenta cuentaModificada = await cuentaManager.Actualizar(cuentaSeleccionada, Session["Token"].ToString());

                        string cod = Session["CodigoUsuario"].ToString();
                        Luz luzFactura = new Luz()
                        {
                            Codigo = Convert.ToInt32(lblCodigo.Text),
                            CodigoUsuario = Convert.ToInt32(cod),
                            Descripcion = txtDescripcion.Text,
                            Estado = "P",
                            MontoEnergia = Convert.ToInt32(txtMontoEnergia.Text),
                            MontoVariable = Convert.ToInt32(txtMontoVariable.Text),
                            MontoAlumbrado = Convert.ToInt32(txtMontoAlumbrado.Text)
                        };

                        Luz luzFacturaModificada = await luzManager.Actualizar(luzFactura, Session["Token"].ToString());

                        if (luzFacturaModificada.Descripcion != null)
                        {
                            lblResultado.Text = "Factura de Luz actualizada con exito";
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
                        lblResultado.Text = "Factura de Luz ya fue pagada";
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
                    Vista = "frmLuz.aspx",
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
                resultado = await luzManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    lblCodigoEliminar.Text = string.Empty;
                    ltrModalMensaje.Text = "Factura de Luz eliminada";
                    btnAceptarModal.Visible = false;

                    lblResultado.Text = "Factura de Luz fue eliminada";
                    lblResultado.ForeColor = Color.Green;
                    InicializarControles();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "openModal();", true);
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
                    Vista = "frmLuz.aspx",
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


            foreach (var luz in luzFacturas.GroupBy(info => info.MontoEnergia).Select(group => new { Codigo = group.Key, Cantidad = group.Key }).OrderBy(x => x.Codigo))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", luz.Codigo)); // 'Correo', 'frmError'.
                dataGraficoVistas.Append(string.Format("'{0}',", luz.Cantidad)); // '2', '3'.


                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal = backgroundcolorsGraficoVistas.ToString().Substring(0, backgroundcolorsGraficoVistas.Length - 1);
            }


        }

    }
}