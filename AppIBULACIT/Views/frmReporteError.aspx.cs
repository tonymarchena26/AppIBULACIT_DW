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
    public partial class frmReporteError : System.Web.UI.Page
    {
        IEnumerable<Error> errores = new ObservableCollection<Error>();
        ErrorManager errorManager = new ErrorManager();

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
                    errores = await errorManager.ObtenerErrores(Session["Token"].ToString());
                InicializarControles();
                    ObtenerDatosGrafico();
            }
        }

        private void InicializarControles()
        {
            try
            {
                //errores = await errorManager.ObtenerErrores(Session["Token"].ToString());
                gvErrores.DataSource = errores.ToList();
                gvErrores.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios.";
                lblStatus.Visible = true;
            }
        }

        protected void gvErrores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void ObtenerDatosGrafico() 
        {
            StringBuilder script = new StringBuilder();
            StringBuilder labelsGraficoVistas = new StringBuilder();
            StringBuilder dataGraficoVistas = new StringBuilder();
            StringBuilder backgroundcolorsGraficoVistas = new StringBuilder();

            var random = new Random();

            foreach (var error in errores.GroupBy(info => info.Vista).Select(group => new { Vista = group.Key, Cantidad = group.Count()}).OrderBy(x => x.Vista)) 
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labelsGraficoVistas.Append(string.Format("'{0}',",error.Vista)); // 'Correo', 'frmError'.
                dataGraficoVistas.Append(string.Format("'{0}',", error.Cantidad)); // '2', '3'.
                backgroundcolorsGraficoVistas.Append(string.Format("'{0}',", color));

                labelsGraficoVistasGlobal = labelsGraficoVistas.ToString().Substring(0, labelsGraficoVistas.Length - 1);
                dataGraficoVistasGlobal = dataGraficoVistas.ToString().Substring(0, dataGraficoVistas.Length - 1);
                backgroundcolorsGraficoVistasGlobal = backgroundcolorsGraficoVistas.ToString().Substring(0, backgroundcolorsGraficoVistas.Length - 1);
            }


        }
    }
}