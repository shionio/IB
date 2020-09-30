using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsExtraefectivo;
using System;
using System.Data;

namespace IBBAV.Entidades.Extraefectivo
{
    public class LineaExtracredito : Solicitud
    {

        private string _key;

        private string _codRespuestaField;

        private string _cuotasField;

        private string _descRespuetaField;

        private string _cuota6;

        private string _cuota12;

        private string _cuota24;

        private string _cuota36;

        private string[] _cuotas;

        private string _montoSolicitadoField;

        private string _numeroCuentaField;

        private string _numeroTarjetaField;

        private string _referenciaField;

        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }

        public string respuestaCod
        {
            get
            {
                return this._codRespuestaField;
            }
            set
            {
                this._codRespuestaField = value;
            }
        }

        public string respuestaDesc
        {
            get
            {
                return this._descRespuetaField;
            }
            set
            {
                this._descRespuetaField = value;
            }
        }

        public string cuenta
        {
            get
            {
                return this._numeroCuentaField;
            }
            set
            {
                this._numeroCuentaField = value;
            }
        }

        public string numeroTDC
        {
            get
            {
                return this._numeroTarjetaField;
            }
            set
            {
                this._numeroTarjetaField = value;
            }
        }

        public string cuota6
        {
            get
            {
                return this._cuota6;
            }
            set
            {
                this._cuota6 = value;
            }
        }

        public string cuota12
        {
            get
            {
                return this._cuota12;
            }
            set
            {
                this._cuota12 = value;
            }
        }

        public string cuota24
        {
            get
            {
                return this._cuota24;
            }
            set
            {
                this._cuota24 = value;
            }
        }

        public string cuota36
        {
            get
            {
                return this._cuota36;
            }
            set
            {
                this._cuota36 = value;
            }
        }


        public string[] cuotas
        {
            get
            {
                return this._cuotas;
            }
            set
            {
                this._cuotas = value;
            }
        }

        public string MontoSolicitado
        {
            get
            {
                return this._montoSolicitadoField;
            }
            set
            {
                this._montoSolicitadoField = value;
            }
        }

        public string referencia
        {
            get
            {
                return this._referenciaField;
            }
            set
            {
                this._referenciaField = value;
            }
        }
        
        public LineaExtracredito()
        {
        }

        public static LineaExtracredito getCuotas(LineaExtracredito obj)
        {
            return new LineaExtracredito()
            {
                respuestaCod = obj.respuestaCod.Trim(),
                respuestaDesc = obj.respuestaDesc.Trim(),
                cuota6 = obj.cuota6.Trim(),
                cuota12 = obj.cuota12.Trim(),
                cuota24 = obj.cuota24.Trim(),
                cuota36 = obj.cuota36.Trim(),                
                //Agregado 12 / 09 / 2018 por Liliana Guerra,
                //para mostrar Saldos Consolidados en PETRO
                //SPetro = obj.SPetro.Trim()
            };
        }

        public static LineaExtracredito getAprobacion(LineaExtracredito obj)
        {
            return new LineaExtracredito()
            {
                respuestaCod = obj.respuestaCod.Trim(),
                respuestaDesc = obj.respuestaDesc.Trim(),                
                referencia = obj.referencia.Trim()
                //Agregado 12 / 09 / 2018 por Liliana Guerra,
                //para mostrar Saldos Consolidados en PETRO
                //SPetro = obj.SPetro.Trim()
            };
        }
    }
}