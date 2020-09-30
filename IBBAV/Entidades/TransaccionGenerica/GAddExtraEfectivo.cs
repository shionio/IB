using IBBAV;
using System.Data;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using IBBAV.Entidades.Extraefectivo;

namespace IBBAV.Entidades.TransaccionGenerica
{
    [Serializable]
    public class GAddExtraEfectivo : GBase
    {
        private string _CedulaBeneficiario;

        private string _CtaAcreditar;

        private string _CtaDebitar;

        private string _montoSolic;

        private string _cantCuotas;

        private string _montoCuota;

        private string SCod_Trans;

        private string mensaje = string.Empty;

        LineaExtracredito solicitud = new LineaExtracredito();

        public string CedulaBeneficiario
        {
            get
            {
                return this._CedulaBeneficiario;
            }
            set
            {
                this._CedulaBeneficiario = value;
            }
        }
        public string CtaAcreditar
        {
            get
            {
                return this._CtaAcreditar;
            }
            set
            {
                this._CtaAcreditar = value;
            }
        }
        public string CtaDebitar
        {
            get
            {
                return this._CtaDebitar;
            }
            set
            {
                this._CtaDebitar = value;
            }
        }
        public string montoSolic
        {
            get
            {
                return this._montoSolic;
            }
            set
            {
                this._montoSolic = value;
            }
        }
        public string cantCuotas
        {
            get
            {
                return this._cantCuotas;
            }
            set
            {
                this._cantCuotas = value;
            }
        }
        public string montoCuota
        {
            get
            {
                return this._montoCuota;
            }
            set
            {
                this._montoCuota = value;
            }
        }

        public GAddExtraEfectivo(IBBAV.Entidades.Afiliado afi, int sCod)
        {
            base.Afiliado = afi;
            base.sCod = sCod;
        }

        public override string EjecutarAccion()
        {
            string empty = string.Empty;

            this.SCod_Trans = "EETDC";
            
            //RespuestaIntrfdsjv respuestaIntrfdsjv3 = HelperIbs.ibsTransfPgoBAV(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.CtaDebitar, base.Afiliado.sAF_Rif, this.CtaAcreditar, monto, 0, DateTime.Now, TransferenciaTipoOperacion.Transferencia, string.Empty, TipoServicio.NoAplica);
            solicitud = HelperExtracredito.solicAprobacion(CedulaBeneficiario, CtaAcreditar, CtaDebitar, montoSolic, cantCuotas, montoCuota);

            if (!this.solicitud.respuestaCod.Equals("000"))
            {
                throw new IBException(this.solicitud.respuestaDesc);
            }
            this.mensaje = solicitud.referencia;
            if (!string.IsNullOrEmpty(this.mensaje))
            {
                this.mensaje = string.Concat("IB", this.mensaje.Trim().PadLeft(10, '0'));
                this.LogExtraEfectivo();
            }

            return empty;
        }

        private string LogExtraEfectivo()
        {

            //HelperEnvioCorreo.BuscarCamposCorreo(base.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, new decimal(0), string.Empty, this._Notificacion.FechaInicio, string.Empty, string.Empty, string.Empty, this._Notificacion.PaisNombre, string.Empty, this._Notificacion.FechaFin, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            //HelperEnvioCorreo.BuscarCamposCorreo(this.sCod, base.Afiliado.sCO_Nombres, base.Afiliado.CO_Email, montoSolic, (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), this.mensaje, (string.IsNullOrEmpty("Mismo Titular en BAV") ? string.Empty : "Mismo Titular en BAV"), (string.IsNullOrEmpty(this.CtaAcreditar) ? string.Empty : this.CtaAcreditar), string.Empty, (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar), string.Empty, string.Empty, (string.IsNullOrEmpty("Extra Efectivo") ? string.Empty : "Extra Efectivo"), (string.IsNullOrEmpty(this.CtaDebitar) ? string.Empty : this.CtaDebitar), (string.IsNullOrEmpty(base.Afiliado.sCO_Nombres) ? string.Empty : base.Afiliado.sCO_Nombres), string.Empty, (string.IsNullOrEmpty(this.Email) ? string.Empty : this.Email));
            string empty = string.Empty;

            DataLog dataLog = new DataLog()
            {
                NAF_Id = base.Afiliado.nAF_Id,
                SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
                DtFecha_Trans = DateTime.Now.Date,
                STime_Trans = DateTime.Now.ToLongTimeString(),
                SCod_Trans = this.SCod_Trans,
                SAF_IP = base.Afiliado.sIP,
                SBanco = string.Empty,
                SCuenta_Origen = this.CtaDebitar,
                SCuenta_Destino = this.CtaAcreditar,
                SMonto = Formatos.formatoMonto(montoSolic),
                STipo_Tarjeta = string.Empty,
                SBeneficiario = string.Empty,
                SCedula_Id_B = (string.IsNullOrEmpty(this.CedulaBeneficiario) ? string.Empty : this.CedulaBeneficiario),
                SSerial_Chequera = string.Empty,
                SCheques = string.Empty,
                STitular = base.Afiliado.sCO_Nombres,
                ICantidad = 0,
                SReferencia = this.mensaje,
                SConcepto = "Extra Efectivo",
                SMotivo_Suspension = string.Empty,
                SDir_Envio_Chequera = string.Empty
            };
            try
            {
                HelperGlobal.LogTransAdd(dataLog);
            }
            catch (IBException bException)
            {
                empty = "Error al Grabar en LogTran";
            }
            catch (Exception exception)
            {
                empty = "Error al Grabar en LogTran";
            }
            return empty;
        }
    }
}