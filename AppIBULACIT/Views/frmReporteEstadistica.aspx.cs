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
    public partial class frmReporteEstadistica : System.Web.UI.Page
    {

        IEnumerable<Estadistica> estadisticas = new ObservableCollection<Estadistica>();
        EstadisticaManager estadisticaManager = new EstadisticaManager();

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
                    estadisticas = await estadisticaManager.ObtenerEstadisticas(Session["Token"].ToString());
                InicializarControles();
                ObtenerDatosGrafico();
            }
        }

        private void InicializarControles()
        {
            try
            {
                //errores = await errorManager.ObtenerErrores(Session["Token"].ToString());
                gvEstadistica.DataSource = estadisticas.ToList();
                gvEstadistica.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de estadisticas.";
                lblStatus.Visible = true;
            }
        }

        protected void gvEstadistica_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void ObtenerDatosGrafico()
        {
            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();

            foreach (var estadistica in estadisticas.GroupBy(info => info.PlataformaDispositivo).Select(group => new { Vista = group.Key, Cantidad = group.Count() }).OrderBy(x => x.Vista))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',", estadistica.Vista)); // 'Correo', 'frmError'.
                dataGraficoVistas.Append(string.Format("'{0}',", estadistica.Cantidad)); // '2', '3'.
                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal = backgroundcolorsGraficoVistas.ToString().Substring(0, backgroundcolorsGraficoVistas.Length - 1);
            }


        }
    }
}