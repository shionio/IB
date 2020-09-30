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

namespace IBBAV.pages.IB.Favoritos
{
    public partial class MenuFavoritos : Principal
    {
        
        public bool PreguntasValidadas
        {
            get
            {
                bool flag;
                object item = this.Session["PreguntasValidadasMenuFavoritos"];
                flag = (item != null ? Convert.ToBoolean(item) : false);
                return flag;
            }
            set
            {
                this.Session["PreguntasValidadasMenuFavoritos"] = value;
            }
        }

        public MenuFavoritos()
        {
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
            FavoritosAfiliado favoritosAfiliado = (new JavaScriptSerializer()).Deserialize<FavoritosAfiliado>(value);
            if (HelperFavorito.AfiliadoFavoritosDelete(base.Afiliado.nAF_Id, favoritosAfiliado.TipoFavoritoID, favoritosAfiliado.NumeroInstrumento))
            {
                this.LogEliminacion(favoritosAfiliado, 1);
            }
            else
            {
                WebUtils.MessageBootstrap(this, "Error al eliminar un Favorito", null);
            }
        }

        protected void btnMod_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/IB/Favoritos/DatosFavorito.aspx?idFav=");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string value = this.hData.Value;
            FavoritosAfiliado favoritosAfiliado = (new JavaScriptSerializer()).Deserialize<FavoritosAfiliado>(value);
            this.LogEliminacion(favoritosAfiliado, 2);
            TipoTransaccionGenerica tipoTransaccionGenerica = new TipoTransaccionGenerica()
            {
                ObjetoTransaccion = new GAfiliacionFavorito(base.Afiliado, base.sCod)
                {
                    AfiliadoFavorito = HelperFavorito.AfiliadoFavoritosGet(base.Afiliado.nAF_Id, favoritosAfiliado.TipoFavoritoID, favoritosAfiliado.NumeroInstrumento),
                    Accion = EnumAccionAddUpdateAfiliadoFavoritos.Update
                }
            };
            this.Context.Items.Add("TipoTransaccionGenerica", tipoTransaccionGenerica);
            base.Server.Transfer(string.Concat("~/pages/IB/Favoritos/DatosFavorito.aspx?sCod=", base.sCod));
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Concat("~/pages/IB/Favoritos/DatosFavorito.aspx?sCod=", base.sCod));
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/pages/consolidada.aspx");
        }

        private bool LogEliminacion(FavoritosAfiliado item, int tipo)
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
                    dataLog.SCod_Trans = "AFBCE";
                    dataLog.SConcepto = string.Concat("Eliminación de Favorito: Tipo Favorito: ", item.TipoDescripcion, " Instrumento ", item.NumeroInstrumento);
                }
                else
                {
                    dataLog.SCod_Trans = "AFBCM";
                    dataLog.SConcepto = string.Concat("Modificación de Favorito: Tipo Favorito: ", item.TipoDescripcion, " Instrumento ", item.NumeroInstrumento);
                }
                dataLog.SAF_IP = base.Afiliado.sIP;
                dataLog.SBanco = string.Empty;
                dataLog.SCuenta_Origen = string.Empty;
                dataLog.SCuenta_Destino = string.Empty;
                dataLog.SMonto = string.Empty;
                dataLog.STipo_Tarjeta = string.Empty;
                dataLog.SBeneficiario = item.Beneficiario;
                dataLog.SCedula_Id_B = string.Empty;
                dataLog.SSerial_Chequera = string.Empty;
                dataLog.SCheques = string.Empty;
                dataLog.STitular = base.Afiliado.sCO_Nombres;
                dataLog.ICantidad = 0;
                dataLog.SReferencia = string.Empty;
                dataLog.SMotivo_Suspension = string.Empty;
                dataLog.SDir_Envio_Chequera = string.Empty;
                flag = HelperGlobal.LogTransAdd(dataLog);
            }
            catch (IBException bException)
            {
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.PreguntasValidadas)
            {
                this.panelValidacion.Visible = false;
                this.panelDatos.Visible = true;
            }
        }
    }
}