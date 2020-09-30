using IBBAV.Entidades;
using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	internal interface IObjetoGenerico
	{
		IBBAV.Entidades.Afiliado Afiliado
		{
			get;
			set;
		}

		bool EnviaCorreo
		{
			get;
			set;
		}

		string MensajeSatisfactorio
		{
			get;
			set;
		}

		string PaginaAnterior
		{
			get;
			set;
		}

		string PaginaSiguiente
		{
			get;
			set;
		}

		int sCod
		{
			get;
			set;
		}

		string EjecutarAccion();

        string AddNotificacion();
    }
}