using System;
using System.Collections.Generic;
using System.Data;

namespace IBBAV.Entidades
{
	public class BAVMenuOpcion : EntidadBase
	{
		private int _posicion;

		private int _codigo;

		private string _nombre;

		private string _pagina;

		private int _codigoPadre;

		private string _manejaDinero;

		private List<BAVMenuOpcion> _opciones;

		public int Codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				this._codigo = value;
			}
		}

		public int CodigoPadre
		{
			get
			{
				return this._codigoPadre;
			}
			set
			{
				this._codigoPadre = value;
			}
		}

		public string ManejaDinero
		{
			get
			{
				return this._manejaDinero;
			}
			set
			{
				this._manejaDinero = value;
			}
		}

		public string Nombre
		{
			get
			{
				return this._nombre;
			}
			set
			{
				this._nombre = value;
			}
		}

		public List<BAVMenuOpcion> Opciones
		{
			get
			{
				return this._opciones;
			}
			set
			{
				this._opciones = value;
			}
		}

		public string Pagina
		{
			get
			{
				return this._pagina;
			}
			set
			{
				this._pagina = value;
			}
		}

		public int Posicion
		{
			get
			{
				return this._posicion;
			}
			set
			{
				this._posicion = value;
			}
		}

		public BAVMenuOpcion()
		{
			this._opciones = new List<BAVMenuOpcion>();
		}

		public static BAVMenuOpcion getNewOpcion(DataRow dr)
		{
			return new BAVMenuOpcion()
			{
				Codigo = EntidadBase.IsInt(dr, "MD_Cod"),
				Nombre = EntidadBase.IsString(dr, "MD_Nombre"),
				CodigoPadre = EntidadBase.IsInt(dr, "MD_Cod_Padre"),
				Posicion = EntidadBase.IsInt(dr, "MD_Pos_Mostrar"),
				ManejaDinero = EntidadBase.IsString(dr, "MD_Man_Dinero"),
				Pagina = EntidadBase.IsString(dr, "MD_Pagina")
			};
		}
	}
}