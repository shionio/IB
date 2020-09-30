using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class TipoTransaccionGenerica
	{
		private object objetoTransaccion;

		private IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion encabezadoTransaccion;

		private string titulo;

		private string nota;

		private string nota2;

		private int iOpRep;

		public IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion EncabezadoTransaccion
		{
			get
			{
				IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion encabezadoTransaccion;
				encabezadoTransaccion = (this.encabezadoTransaccion != null ? this.encabezadoTransaccion : new IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion());
				return encabezadoTransaccion;
			}
			set
			{
				this.encabezadoTransaccion = value;
			}
		}

		public int IOpRep
		{
			get
			{
				return this.iOpRep;
			}
			set
			{
				this.iOpRep = value;
			}
		}

		public string Nota
		{
			get
			{
				string str;
				str = (this.nota != null ? this.nota : string.Empty);
				return str;
			}
			set
			{
				this.nota = value;
			}
		}

		public string Nota2
		{
			get
			{
				string str;
				str = (this.nota2 != null ? this.nota2 : string.Empty);
				return str;
			}
			set
			{
				this.nota2 = value;
			}
		}

		public object ObjetoTransaccion
		{
			get
			{
				return this.objetoTransaccion;
			}
			set
			{
				this.objetoTransaccion = value;
			}
		}

		public string Titulo
		{
			get
			{
				string str;
				str = (this.titulo != null ? this.titulo : string.Empty);
				return str;
			}
			set
			{
				this.titulo = value;
			}
		}

		public TipoTransaccionGenerica()
		{
			this.titulo = string.Empty;
			this.nota = string.Empty;
			this.nota2 = string.Empty;
			this.encabezadoTransaccion = new IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion();
		}

		public TipoTransaccionGenerica(object objetoTransaccion, IBBAV.Entidades.TransaccionGenerica.EncabezadoTransaccion encabezadoTransaccion)
		{
			this.titulo = string.Empty;
			this.nota = string.Empty;
			this.nota2 = string.Empty;
			this.objetoTransaccion = objetoTransaccion;
			this.encabezadoTransaccion = encabezadoTransaccion;
		}
	}
}