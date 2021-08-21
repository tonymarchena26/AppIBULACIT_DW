using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class frmReporteSesion : System.Web.UI.Page
    {
        IEnumerable<Sesion> sesiones = new ObservableCollection<Sesion>();
        SesionManager sesionManager = new SesionManager();

        public string labelsGraficoVistasGlobal = string.Empty;
        public string dataGraficoVistasGlobal = string.Empty;
        public string backgroundcolorsGraficoVistasGlobal = string.Empty;

        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    sesiones = await sesionManager.ObtenerSesiones(Session["Token"].ToString());
                InicializarControles();
                ObtenerDatosGrafico();
            }
        }

        private void InicializarControles()
        {
            try
            {
                //errores = await errorManager.ObtenerErrores(Session["Token"].ToString());
                gvSesion.DataSource = sesiones.ToList();
                gvSesion.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de sesiones.";
                lblStatus.Visible = true;
            }
        }

        protected void gvSesion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void ObtenerDatosGrafico()
        {
            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();

            foreach (var sesion in sesiones.GroupBy(info => info.CodigoUsuario).Select(group => new { Vista = group.Key, Cantidad = group.Count() }).OrderBy(x => x.Vista))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", sesion.Vista)); // 'Correo', 'frmError'.
                dataGraficoVistas.Append(string.Format("'{0}',", sesion.Cantidad)); // '2', '3'.
                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal = backgroundcolorsGraficoVistas.ToString().Substring(0, backgroundcolorsGraficoVistas.Length - 1);
            }


        }
    }
}