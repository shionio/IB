using Functions;
using IBBAV.Entidades;
using IBBAV.Entidades.Notificaciones;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.NotificacionViajes
{
    public partial class NotificacionNueva : Principal
    {
        private EncabezadoTransaccion encabezado;

        public bool PreguntasValidadas
        {
            get
            {
                bool flag;
                object item = this.Session["PreguntasValidadasNotificacion"];
                flag = (item != null ? Convert.ToBoolean(item) : false);
                return flag;
            }
            set
            {
                this.Session["PreguntasValidadasNotificacion"] = value;
            }
        }

        private TipoTransaccionGenerica tg
        {
            get
            {
                TipoTransaccionGenerica tipoTransaccionGenerica;
                object item = this.ViewState["TipoTransaccionGenerica"];
                if (item != null)
                {
                    tipoTransaccionGenerica = (TipoTransaccionGenerica)item;
                }
                else
                {
                    tipoTransaccionGenerica = null;
                }
                return tipoTransaccionGenerica;
            }
            set
            {
                this.ViewState["TipoTransaccionGenerica"] = value;
            }
        }


        public NotificacionNueva()
        {
            base.NombrePantalla = "Nueva Notificación";
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string nPaisDestino = this.ListaDestino.Text;
            string nPeriodoInicio = this.FD.Text;
            string nPeriodoFin = this.FH.Text;
            try
            {
                this.ValidarCampos(nPaisDestino, nPeriodoInicio, nPeriodoFin);


                this.encabezado = new EncabezadoTransaccion();
                this.encabezado.AddEncabezado("Destino: " + nPaisDestino, " Salida: " + nPeriodoInicio + " Retorno: " + nPeriodoFin);
                //GAddNotificacion gAddNotificacion = new GAddNotificacion(base.Afiliado, base.sCod);
                GAddNotificacion gAddNotificacion = (this.tg != null ? (GAddNotificacion)this.tg.ObjetoTransaccion : new GAddNotificacion(base.Afiliado, base.sCod));
                //Condicional	x ? y : z	Se evalúa como y si x es true y como z si x es false

                this.tg = new TipoTransaccionGenerica();

                // ****** Captura el tipo y numero de instrumento para agregar a la notificacion ***//
                RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(base.Afiliado.AF_CodCliente.ToString(), base.Afiliado.sAF_Rif, TipoConsultaCuentasIBS.Todas);
                List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
                ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
                IbaConsDet dataItem = ibaConsDets.Find((IbaConsDet x) => !x.STipoFirma.Contains("N"));
                gAddNotificacion.TipoInstrumento = dataItem.STipocuenta;
                gAddNotificacion.NumInstrumento = dataItem.SNroCuenta;
                //***********************************************************************************//

                gAddNotificacion.Destino = nPaisDestino;
                gAddNotificacion.FechaInicio = nPeriodoInicio;
                gAddNotificacion.FechaFin = nPeriodoFin;
                gAddNotificacion.Opcion = "Notificacion";
                gAddNotificacion.PaginaAnterior = string.Concat("~/pages/NotificacionViajes/NotificacionNueva.aspx");
                gAddNotificacion.PaginaSiguiente = "~/pages/NotificacionViajes/DetalleNotificacion.aspx";
                this.tg.EncabezadoTransaccion = this.encabezado;
                this.tg.ObjetoTransaccion = gAddNotificacion;
                this.tg.Titulo = "Nueva Notificación de Viaje";
                this.tg.Nota = "";
                this.tg.Nota2 = "";
                flag = true;

                if (flag)
                {
                    this.Context.Items.Add("TipoTransaccionGenerica", this.tg);
                    base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
                }
            }
            catch (IBException bException)
            {
                WebUtils.MessageBox(this, bException.IBMessage);
            }
            catch (Exception exception)
            {
                WebUtils.MessageBox(this, exception.Message);
            }

        }

        protected void btnAceptarPreguntas_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                flag = this.PreguntasDesafio.ValidarPreguntasRespuestas();
            }
            catch (IBException bException1)
            {
                IBException bException = bException1;
                if (bException.ReturnCode == "1003")
                {
                    base.Afiliado.nES_Id = (long)6;
                }
                WebUtils.MessageBootstrap(this, bException.IBMessage, null);
                return;
            }
            catch (Exception exception)
            {
                WebUtils.MessageBootstrap(this, exception.Message, null);
                return;
            }
            this.PreguntasValidadas = flag;
            if (!flag)
            {
                WebUtils.MessageBootstrap(this, "Las respuestas no son correctas, por favor recuerde que al tercer intento, su usuario sera bloqueado por seguridad", null);
            }
            else
            {
                this.panelValidacion.Visible = false;
                this.panelDatos.Visible = true;
            }
        }

        protected void btnCancelarPreguntas_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/NotificacionViajes/DetalleNotificacion.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.PreguntasValidadas)
            {
                this.panelValidacion.Visible = false;
                this.panelDatos.Visible = true;
            }

            if (HelperNotificacionIBP.VerificaNotificacion(base.Afiliado.nAF_Id))
            {
                this.btnAceptar.Visible = false;
                WebUtils.MessageBootstrap(this, string.Concat("Ya posee una notificación activa. Si desea agregar otros destinos a esta notificación o eliminarla  presione <a href=\"", base.ResolveUrl("~/pages/NotificacionViajes/DetalleNotificacion.aspx"), "\">aquí</a>"), null);
            }

            if (!base.IsPostBack)
            {                
                this.ListaDestino.HasTextoInicial = true;
                this.ListaDestino.TextoInicial = "Seleccione el país";
                this.ListaDestino.GetLista();
                base.Afiliado = HelperAfiliado.AfiliadosGet(base.Afiliado.nAF_Id, EnumTipoCodigo.AF_ID);
                this.Session["Afiliado"] = base.Afiliado;
            }
        }
               
        
        

        private void ValidarCampos(string destino, string inicio, string fin)
        {
            if ((destino == "") || (destino == null))
            {
                throw new Exception("Debe selecionar un destino");
            }
            if ((inicio == null) || (Convert.ToDateTime(inicio) < DateTime.Today) || (Convert.ToDateTime(inicio) > Convert.ToDateTime(fin)))
            {
                throw new Exception("La fecha de salida no puede ser anterior a la fecha actual, ni superior a la fecha de retorno. Selecione un fecha valida para salida");
            }
            if ((fin == null) || (Convert.ToDateTime(fin) <= DateTime.Today))
            {
                throw new Exception("La fecha de retorno no puede ser anterior a la fecha actual, ni anterior a la fecha de salida. Selecione un fecha valida para retorno");
            }

            if ((FechasCalc.DiferenciaDias(Convert.ToDateTime(inicio),Convert.ToDateTime(fin)) >= 183))
            {
                throw new Exception("La notificación supera el periodo máximo de 6 meses.");
            }
        }
    }
}