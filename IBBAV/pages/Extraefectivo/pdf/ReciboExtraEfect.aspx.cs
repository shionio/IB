using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Extraefectivo.pdf
{
    public partial class ReciboExtraEfect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
                this.liTextoReferencia.Text = "Número de Referencia: ";
                this.liReferencia.Text = Session["tdc_numReferencia"].ToString();
                this.liNota.Visible = true;
                this.liNota3.Visible = true;
                this.lblFechaRecibo.Text = Session["tdc_fechaRecibo"].ToString();
                this.lblNombreUsuarioRecibo.Text = Session["tdc_nombreUsuarioRecibo"].ToString();
                //this.liNota.Text = "se realizó exitósamente";
                //this.liNota3.Text = "Te recomendamos Imprimir este recibo para tu control y constancia de tu operación";
                
                this.liDebito.Text = " Tarjeta de Crédito: ";
                this.liValordebito.Text = Session["tdc_tdc"].ToString();
                this.liCredito.Text = "Abonado en Cuenta:";
                this.liValorcredito.Text = Session["tdc_cuentaAbono"].ToString();
                this.liConcepto.Text = "Concepto:";
                this.liValorConcepto.Text = "Extra Efectivo";
                this.liMonto.Text = "Monto Abonado:";
                this.liValormonto.Text = Session["tdc_monto"].ToString();
                this.liTotalcuotas.Text = "Cantidad de cuotas a pagar:";
                this.liValorcuotas.Text = Session["tdc_cuotas"].ToString();
                this.liMontocuota.Text = "Monto mensual:";
                this.liValormontocuota.Text = Session["tdc_cuotaMes"].ToString();
           
        }
    }
}