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
    public partial class DestinoNuevo : Principal
    {
        private EncabezadoTransaccion encabezado;

        private Notificacion UltDestino;
        private Notificacion PriDestino;

        private DateTime FechaMin;
        private DateTime FechaMax;

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


        public DestinoNuevo()
        {
            base.NombrePantalla = "Nuevo Destino";
        }

        protected void btnCancelarPreguntas_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/NotificacionViajes/DetalleNotificacion.aspx");
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string nPaisDestino = this.ListaDestino.Text;
            string nPeriodoInicio = this.FD.Text;
            string nPeriodoFin = this.FH.Text;

            this.encabezado = new EncabezadoTransaccion();
            this.encabezado.AddEncabezado("Destino: " + nPaisDestino, " Salida: " + nPeriodoInicio + " Retorno: " + nPeriodoFin);

            GAddNotificacion gAddDestino = (this.tg != null ? (GAddNotificacion)this.tg.ObjetoTransaccion : new GAddNotificacion(base.Afiliado, base.sCod));
            //Condicional	x ? y : z	Se evalúa como y si x es true y como z si x es false

            this.tg = new TipoTransaccionGenerica();

            gAddDestino.Destino = nPaisDestino;
            gAddDestino.FechaInicio = nPeriodoInicio;
            gAddDestino.FechaFin = nPeriodoFin;
            gAddDestino.Opcion = "Destino";
            gAddDestino.PaginaAnterior = string.Concat("~/pages/NotificacionViajes/DestinoNuevo.aspx");
            gAddDestino.PaginaSiguiente = "~/pages/NotificacionViajes/DetalleNotificacion.aspx";
            this.tg.EncabezadoTransaccion = this.encabezado;
            this.tg.ObjetoTransaccion = gAddDestino;
            this.tg.Titulo = "Nuevo Destino ";
            this.tg.Nota = "";
            this.tg.Nota2 = "";
            flag = true;

            if (flag)
            {
                this.Context.Items.Add("TipoTransaccionGenerica", this.tg);
                base.Server.Transfer(string.Concat("~/pages/Confirmacion.aspx?sCod=", base.sCod));
            }
        }
        
        protected void btnRegresar_Click(object sender, EventArgs e)
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

            if (!HelperNotificacionIBP.VerificaNotificacion(base.Afiliado.nAF_Id))
            {
                this.btnAceptar.Visible = false;
                WebUtils.MessageBootstrap(this, string.Concat("Para usar esta opción debe tener una notificación activa. Si desea crear una notificación presione <a href=\"", base.ResolveUrl("~/pages/NotificacionViajes/NotificacionNueva.aspx"), "\">aquí</a>"), null);
            }
            else if (!HelperNotificacionIBP.ValidaDestinos(base.Afiliado.nAF_Id))
            {
                this.btnAceptar.Visible = false;
                WebUtils.MessageBootstrap(this, string.Concat("No cumple los requisitos para agregar un nuevo destino a esta notificación.<br/>Le recordamos que cada notificación tiene un máximo de 3 lugares como destino en un lapso de 6 meses.</br> presione <a href=\"", base.ResolveUrl("~/pages/NotificacionViajes/DetalleNotificacion.aspx"), "\">aquí para regresar a la consulta</a>"), null); ;
            }
            else
            {
                if (!base.IsPostBack)
                {
                    this.ListaDestino.HasTextoInicial = true;
                    this.ListaDestino.TextoInicial = "Seleccione el país";
                    this.ListaDestino.GetLista();
                    base.Afiliado = HelperAfiliado.AfiliadosGet(base.Afiliado.nAF_Id, EnumTipoCodigo.AF_ID);
                    this.Session["Afiliado"] = base.Afiliado;
                }

                UltDestino = Notificacion.UltimoDestino(base.Afiliado.nAF_Id);
                FechaMin = Convert.ToDateTime(UltDestino.FechaFin);
                this.FD.Text = FechaMin.ToString("dd/MM/yyyy");

                PriDestino = Notificacion.PrimerDestino(base.Afiliado.nAF_Id);
                FechaMax = Convert.ToDateTime(PriDestino.FechaInicio);
                this.maximo.Value = FechaMax.ToString("dd/MM/yyyy");
            }
            
        }

        private void ValidarCampos(string destino, string inicio, string fin)
        {
            if ((destino == "") || (destino == null))
            {
                throw new Exception("Debe selecionar un destino");
            }
            if ((inicio == null) || (Convert.ToDateTime(inicio) < DateTime.Today) || (Convert.ToDateTime(inicio) > Convert.ToDateTime(fin)) || (Convert.ToDateTime(inicio) < FechaMin))
            {
                throw new Exception("La fecha de salida no puede ser anterior a la fecha actual, ni superior a la fecha de retorno. Selecione un fecha valida para salida");
            }
            if ((fin == null) || (Convert.ToDateTime(fin) <= DateTime.Today))
            {
                throw new Exception("La fecha de retorno no puede ser anterior a la fecha actual, ni anterior a la fecha de salida. Selecione un fecha valida para retorno");
            }

            if ((FechasCalc.DiferenciaDias(FechaMax, Convert.ToDateTime(fin)) >= 183))
            {
                throw new Exception("La notificación supera el periodo máximo de 6 meses.");
            }
        }
    }
}