using IBBAV.Entidades;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	[Serializable]
	public class GBase : IObjetoGenerico
	{
		private string paginaAnterior;

		private string paginaSiguiente;

		private string mensajeSatisfactorio;

		private IBBAV.Entidades.Afiliado afiliado;

		private int scod;

		private bool enviaCorreo;

		public IBBAV.Entidades.Afiliado Afiliado
		{
			get
			{
				return this.afiliado;
			}
			set
			{
				this.afiliado = value;
			}
		}

		public bool EnviaCorreo
		{
			get
			{
				return this.enviaCorreo;
			}
			set
			{
				this.enviaCorreo = value;
			}
		}

		public string MensajeSatisfactorio
		{
			get
			{
				return this.mensajeSatisfactorio;
			}
			set
			{
				this.mensajeSatisfactorio = value;
			}
		}

		public string PaginaAnterior
		{
			get
			{
				return this.paginaAnterior;
			}
			set
			{
				this.paginaAnterior = value;
			}
		}

		public string PaginaSiguiente
		{
			get
			{
				return this.paginaSiguiente;
			}
			set
			{
				this.paginaSiguiente = value;
			}
		}

		public int sCod
		{
			get
			{
				return this.scod;
			}
			set
			{
				this.scod = value;
			}
		}

		public GBase()
		{
		}

		public virtual string EjecutarAccion()
		{
			return string.Empty;
		}

        public virtual string AddNotificacion()
        {
            return string.Empty;
        }
    }
}