using System;

namespace IBBAV.Entidades.TransaccionGenerica
{
	public enum EnumTransaccion
	{
		TransMismoTitularBAV,
		TransMismoTitularOB,
		TransTercerosBAV,
		TransTercerosOB,
		PagoTCMismoTitularBAV,
		PagoTCMismoTitularOB,
		PagoTCTercerosBAV,
		PagoTCTercerosOB,
		SuspensionChequera,
		SuspensionCheques,
		ReferenciaBancaria,
		BloqueoTD,
		AvanceEfectivo,
		AfiliacionFavorito,
		SmsAfiliacion,
		SmsDesafiliacion,
		SmsModificacion,
		SmsSuspension
	}
}