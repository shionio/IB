using System;

namespace IBBAV.Helpers
{
	public enum EnumTipoFavorito
	{
		Default = 0,
		PagoServicioElectricidadCaracas = 1,
		PagoServicioCANTV = 2,
		PagoServicioCANTVNET = 4,
		PagoServicioMovilnet = 5,
        ExtraEfectivoTDC = 98,
        TransferenciaMismoTitularBAV = 99,
		TransferenciaTercerosBAV = 100,
		TransferenciaOtrosBancosMismoTitular = 110,
		TransferenciaOtrosBancosTerceros = 111,
		AdelantodeEfectivoTDC = 118,
		PagoTarjetaCreditoMismoTitularBAV = 119,
		PagoTarjetaCreditoTercerosBAV = 120,
		PagoTarjetaCreditoOtrosBancosMismoTitular = 130,
		PagoTarjetaCreditoOtrosBancosTerceros = 140,
		ServicioBonosBancoIntenet = 150,
		PagoExtraFinanciamiento = 160,
		PagoHidrocapital = 180
	}
}