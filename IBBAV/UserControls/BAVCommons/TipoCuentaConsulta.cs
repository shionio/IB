using System;

namespace IBBAV.UserControls.BAVCommons
{
	[Serializable]
	public enum TipoCuentaConsulta
	{
		Todas,
		TodasCorrientes,
		CuentaCorrienteSinIntereses,
		CuentaCorrienteInteresLimitado,
		CuentaCorrienteIntereses,
		CuentaAhorro,
		CuentaAhorroCorriente
	}
}