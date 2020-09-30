using IBBAV.Functions;
using IBBAV.WsIbsService;
using IBBAV.WsExtraefectivo;
using System;

namespace IBBAV.Entidades.IBS
{
	[Serializable]
	public class CuentaIBS : IbaConsDet
	{
		private string key;       

		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}
        

		public CuentaIBS()
		{
		}

		public static CuentaIBS getNewCuentaIBS(IbaConsDet obj)
		{           
			return new CuentaIBS()
			{
				SClasecuenta = obj.SClasecuenta.Trim(),
				SCodMoneda = obj.SCodMoneda.Trim(),
				SContable = obj.SContable.Trim(),
				SDescipcionproducto = obj.SDescipcionproducto.Trim(),
				SDisponible = obj.SDisponible.Trim(),
				SIndicadorDeb = obj.SIndicadorDeb.Trim(),
				SNickName = obj.SNickName.Trim(),
				SNroCuenta = obj.SNroCuenta.Trim(),
				STipocuenta = obj.STipocuenta.Trim(),
				STipoFirma = obj.STipoFirma.Trim(),
				SUserId = obj.SUserId.Trim(),
				Key = CryptoUtils.EncryptMD5(obj.SNroCuenta.Trim()),
				SBloqueado = obj.SBloqueado.Trim(),
				SDiferido = obj.SDiferido.Trim(),
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                //SPetro = obj.SPetro.Trim()
            };
		}

        public static CuentaIBS getNewExtraefectivo(IbaConsDet obj)
        {
            return new CuentaIBS()
            {
                SClasecuenta = obj.SClasecuenta.Trim(),
                SCodMoneda = obj.SCodMoneda.Trim(),
                SContable = obj.SContable.Trim(),
                SDescipcionproducto = obj.SDescipcionproducto.Trim(),
                SDisponible = obj.SDisponible.Trim(),
                SIndicadorDeb = obj.SIndicadorDeb.Trim(),
                SNickName = obj.SNickName.Trim(),
                SNroCuenta = obj.SNroCuenta.Trim(),
                STipocuenta = obj.STipocuenta.Trim(),
                STipoFirma = obj.STipoFirma.Trim(),
                SUserId = obj.SUserId.Trim(),
                Key = CryptoUtils.EncryptMD5(obj.SNroCuenta.Trim()),
                SBloqueado = obj.SBloqueado.Trim(),
                SDiferido = obj.SDiferido.Trim(),
                //Agregado 12/09/2018 por Liliana Guerra, para mostrar Saldos Consolidados en PETRO
                //SPetro = obj.SPetro.Trim()
            };
        }
    }
}