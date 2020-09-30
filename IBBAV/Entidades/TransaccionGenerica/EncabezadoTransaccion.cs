using System;
using System.Data;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class EncabezadoTransaccion
	{
		private DataTable lista;

		public DataTable Lista
		{
			get
			{
				return this.lista;
			}
		}

		public EncabezadoTransaccion()
		{
			this.lista = new DataTable();
			this.lista.Columns.Add(new DataColumn("TituloConfirmacion", typeof(string)));
			this.lista.Columns.Add(new DataColumn("TituloRecibo", typeof(string)));
			this.lista.Columns.Add(new DataColumn("Valor", typeof(string)));
		}

		public void AddEncabezado(string TituloConfirmacion, string TituloRecibo, string Valor)
		{
			DataRow tituloConfirmacion = this.lista.NewRow();
			tituloConfirmacion["TituloConfirmacion"] = TituloConfirmacion;
			tituloConfirmacion["TituloRecibo"] = TituloRecibo;
			tituloConfirmacion["Valor"] = Valor;
			this.lista.Rows.Add(tituloConfirmacion);
		}

		public void AddEncabezado(string TituloConfirmacion, string Valor)
		{
			this.AddEncabezado(TituloConfirmacion, TituloConfirmacion, Valor);
		}
	}
}