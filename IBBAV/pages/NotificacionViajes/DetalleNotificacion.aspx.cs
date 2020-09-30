using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.TransaccionGenerica;
using IBBAV.Helpers;
using IBBAV.UserControls;
using IBBAV.ws;
using System;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.pages.NotificacionViajes
{
    public partial class DetalleNotificacion : Principal
    {
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

        string sesion;

        public DetalleNotificacion()
        {
            base.NombrePantalla = "Detalles de Notificación";
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string value = this.hData.Value;
            DetallesViaje detalles = (new JavaScriptSerializer()).Deserialize<DetallesViaje>(value);
            if (HelperNotificacionIBP.DestinosDel(base.Afiliado.nAF_Id, detalles.DestinoId))
            {
                this.LogEliminacion(detalles, 1);
            }
            else
            {
                WebUtils.MessageBootstrap(this, "Error al eliminar un Destino", null);
            }
        }

        protected void btnMod_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/IB/NotificacionViajes/Favoritos/ModificarDestino.aspx?idFav=");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string value = this.hData.Value;
            DetallesViaje detalles = (new JavaScriptSerializer()).Deserialize<DetallesViaje>(value);
            this.LogEliminacion(detalles, 2);
            TipoTransaccionGenerica tipoTransaccionGenerica = new TipoTransaccionGenerica()
            {
                ObjetoTransaccion = new GAfiliacionFavorito(base.Afiliado, base.sCod)
                {
                    AfiliadoFavorito = HelperFavorito.AfiliadoFavoritosGet(base.Afiliado.nAF_Id, Convert.ToInt32(detalles.DestinoId), detalles.País),
                    Accion = EnumAccionAddUpdateAfiliadoFavoritos.Update
                }
            };
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/IB/Favoritos/DatosFavorito.aspx?sCod=", base.sCod));
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Concat("~/pages/NotificacionViajes/DestinoNuevo.aspx?sCod=", base.sCod));
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        private bool LogEliminacion(DetallesViaje item, int tipo)
        {
            bool flag = false;
            try
            {
                DataLog dataLog = new DataLog()
                {
                    NAF_Id = base.Afiliado.nAF_Id,
                    SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
                    DtFecha_Trans = DateTime.Now.Date,
                    STime_Trans = DateTime.Now.ToLongTimeString()
                };
                if (tipo == 1)
                {
                    dataLog.SCod_Trans = "ANOVE";
                    dataLog.SConcepto = string.Concat("Eliminación de Destino: País: ", item.País, " Fecha ", item.FechaInicio);
                }
                else
                {
                    dataLog.SCod_Trans = "ANOVM";
                    dataLog.SConcepto = string.Concat("Modificación de Destino: País: ", item.País, " Fecha ", item.FechaInicio);
                }
                dataLog.SAF_IP = base.Afiliado.sIP;
                dataLog.SBanco = string.Concat("Salida: ", item.FechaInicio);
                dataLog.SCuenta_Origen = string.Concat("Notificación ID: ", item.NotificacionId);
                dataLog.SCuenta_Destino = string.Concat("Destino ID: ", item.DestinoId);
                dataLog.SMonto = string.Empty;
                dataLog.STipo_Tarjeta = string.Concat("Retorno: ", item.FechaFin);
                dataLog.SBeneficiario = string.Empty;
                dataLog.SCedula_Id_B = string.Empty;
                dataLog.SSerial_Chequera = string.Empty;
                dataLog.SCheques = string.Empty;
                dataLog.STitular = base.Afiliado.sCO_Nombres;
                dataLog.ICantidad = 0;
                dataLog.SReferencia = string.Empty;
                dataLog.SMotivo_Suspension = item.NotificacionId.ToString(); 
                dataLog.SDir_Envio_Chequera = item.DestinoId.ToString();

                flag = HelperGlobal.LogTransAdd(dataLog);
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sesion = this.SessionId;
            this.SesionField.Value = this.SessionId;

            if (this.PreguntasValidadas)
            {
                this.panelValidacion.Visible = false;
                this.panelDatos.Visible = true;
            }
        }

    }
    
}