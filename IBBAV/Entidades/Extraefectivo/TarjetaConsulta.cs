using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsExtraefectivo;
using System;
using System.Data;

namespace IBBAV.Entidades.Extraefectivo
{
    [Serializable]
    public class TarjetaConsulta : Tarjeta
    {
        private string _key;
        private string _respuestaCod;
        private string _respuestaDesc;
        private string _disponible;
        private string _marca;
        private string _numeroTarjeta;       

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
                return this._respuestaCod;
            }
            set
            {
                this._respuestaCod = value;
            }
        }

        public string respuestaDesc
        {
            get
            {
                return this._respuestaDesc;
            }
            set
            {
                this._respuestaDesc = value;
            }
        }

        public string saldo
        {
            get
            {
                return this._disponible;
            }
            set
            {
                this._disponible = value;
            }
        }

        public string marcaTDC
        {
            get
            {
                return this._marca;
            }
            set
            {
                this._marca = value;
            }
        }

        public string numeroTDC
        {
            get
            {
                return this._numeroTarjeta;
            }
            set
            {
                this._numeroTarjeta = value;
            }
        }
                
        public TarjetaConsulta()
        {
        }

       /*
        public static TarjetaConsulta getNewTarjeta(Tarjeta tarjeta)
        {
            return new TarjetaConsulta()
            {
                respuestaCod = tarjeta.codRespuesta.Trim(),
                respuestaDesc = tarjeta.codRespuesta.Trim(),
                saldo = tarjeta.disponible.Trim(),
                marcaTDC = tarjeta.marca.Trim(),
                numeroTDC = tarjeta.numero.Trim(),
                Key = CryptoUtils.EncryptMD5(tarjeta.numero.Trim()),
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                //SPetro = obj.SPetro.Trim()
            };
        }*/
    }
}