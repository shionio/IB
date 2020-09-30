using IBBAV;
using IBBAV.Entidades;
using IBBAV.Entidades.IBS;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.WsIbsService;
using IBBAV.WsExtraefectivo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using IBBAV.Entidades.Extraefectivo;

namespace IBBAV.UserControls.BAVCommons
{
	[ToolboxBitmap(typeof(DropDownList))]
	[ToolboxData("<{0}:DropDownListCuentas runat=server></{0}:DropDownListCuentas>")]
	public class DropDownListCuentas : DropDownList
	{
		private string setValue;

		private bool hasTextoInicial;

		private string textoInicial;

		private IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato tipoComboCuentaFormato;

		private IBBAV.UserControls.BAVCommons.TipoCombo tipoCombo;

		private IBBAV.UserControls.BAVCommons.TipoCuentaConsulta tipoCuentaConsulta;

		private IBBAV.Helpers.TipoConsultaCuentasIBS tipoConsultaCuentasIBS;

		private EnumTipoFavorito tipoCuentaFavoritos;

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(true)]
		[Localizable(false)]
		public bool HasTextoInicial
		{
			get
			{
				return this.hasTextoInicial;
			}
			set
			{
				this.hasTextoInicial = value;
			}
		}

		[Bindable(false)]
		[Localizable(false)]
		public List<AfiliadoFavorito> ListaAfiliadoFavoritos
		{
			get
			{
				return this.ViewState["dataAfiliadoFavorito"] as List<AfiliadoFavorito>;
			}
			set
			{
				this.ViewState["dataAfiliadoFavorito"] = value;
			}
		}

		[Bindable(false)]
		[Localizable(false)]
		public List<CuentaIBS> ListaCuentas
		{
			get
			{
				return this.ViewState["dataCuentas"] as List<CuentaIBS>;
			}
			set
			{
				this.ViewState["dataCuentas"] = value;
			}
		}

        [Bindable(false)]
        [Localizable(false)]
        public List<TarjetaConsulta> ListaExtraEfectivo
        {
            get
            {
                return this.ViewState["dataCuentas"] as List<TarjetaConsulta>;
            }
            set
            {
                this.ViewState["dataCuentas"] = value;
            }
        }

        [Bindable(false)]
		[Localizable(false)]
		public List<TipoFavorito> ListaTipoFavorito
		{
			get
			{
				return this.ViewState["dataTipoFavorito"] as List<TipoFavorito>;
			}
			set
			{
				this.ViewState["dataTipoFavorito"] = value;
			}
		}

		[Bindable(false)]
		[Localizable(false)]
		public List<Servicios> ListaTiposServicios
		{
			get
			{
				return this.ViewState["dataServicios"] as List<Servicios>;
			}
			set
			{
				this.ViewState["dataServicios"] = value;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(true)]
		[Localizable(false)]
		public string SetValue
		{
			get
			{
				string str;
				str = (this.setValue != null ? this.setValue : string.Empty);
				return str;
			}
			set
			{
				this.setValue = value;
			}
		}

		[Bindable(true)]
		[Browsable(true)]
		[Category("BAV")]
		[DefaultValue("Seleccione")]
		[Localizable(true)]
		public string TextoInicial
		{
			get
			{
				return this.textoInicial;
			}
			set
			{
				this.textoInicial = value;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(IBBAV.UserControls.BAVCommons.TipoCombo.CuentasCliente)]
		[Localizable(false)]
		public IBBAV.UserControls.BAVCommons.TipoCombo TipoCombo
		{
			get
			{
				return this.tipoCombo;
			}
			set
			{
				this.tipoCombo = value;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.Cuenta)]
		[Localizable(false)]
		public IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato TipoComboCuentaFormato
		{
			get
			{
				return this.tipoComboCuentaFormato;
			}
			set
			{
				this.tipoComboCuentaFormato = value;
			}
		}

		public IBBAV.Helpers.TipoConsultaCuentasIBS TipoConsultaCuentasIBS
		{
			get
			{
				return this.tipoConsultaCuentasIBS;
			}
			set
			{
				this.tipoConsultaCuentasIBS = value;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.Todas)]
		[Localizable(false)]
		public IBBAV.UserControls.BAVCommons.TipoCuentaConsulta TipoCuentaConsulta
		{
			get
			{
				return this.tipoCuentaConsulta;
			}
			set
			{
				this.tipoCuentaConsulta = value;
			}
		}

		[Bindable(true)]
		[Category("BAV")]
		[DefaultValue(EnumTipoFavorito.TransferenciaTercerosBAV)]
		[Localizable(false)]
		public EnumTipoFavorito TipoCuentaFavoritos
		{
			get
			{
				return this.tipoCuentaFavoritos;
			}
			set
			{
				this.tipoCuentaFavoritos = value;
			}
		}

		public DropDownListCuentas()
		{
		}

		public void BindCombo()
		{
			Afiliado afiliado = ((Principal)this.Page).Afiliado;
			if (afiliado != null)
			{
				this.Items.Clear();
				if (this.HasTextoInicial)
				{
					this.Items.Add(new ListItem(this.TextoInicial, "0"));
				}
				switch (this.TipoCombo)
				{
					case IBBAV.UserControls.BAVCommons.TipoCombo.CuentasCliente:
					{
						long aFCodCliente = afiliado.AF_CodCliente;
						RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(aFCodCliente.ToString(), afiliado.sAF_Rif, this.TipoConsultaCuentasIBS);
						List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
						ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
                        //Agregado 02/05/2019 por Liliana Guerra para ocultar las cuentas JURIDICA en la sesión natural
                        ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => !x.STipoFirma.Contains("N"));

                            switch (this.TipoCuentaConsulta)
						    {
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.TodasCorrientes:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => ((x.STipocuenta.Equals("DDA") ? false : !x.STipocuenta.Equals("MMK")) ? x.STipocuenta.Equals("NOW") : true));
								break;
							}
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.CuentaCorrienteSinIntereses:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => x.STipocuenta.Equals("DDA"));
								break;
							}
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.CuentaCorrienteInteresLimitado:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => x.STipocuenta.Equals("MMK"));
								break;
							}
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.CuentaCorrienteIntereses:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => x.STipocuenta.Equals("NOW"));
								break;
							}
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.CuentaAhorro:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => x.STipocuenta.Equals("SAV"));
								break;
							}
							case IBBAV.UserControls.BAVCommons.TipoCuentaConsulta.CuentaAhorroCorriente:
							{
								ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => ((x.STipocuenta.Equals("SAV") || x.STipocuenta.Equals("DDA") ? false : !x.STipocuenta.Equals("MMK")) ? x.STipocuenta.Equals("NOW") : true));
								break;
							}
						}
						this.ListaCuentas = new List<CuentaIBS>();
						List<IbaConsDet>.Enumerator enumerator = ibaConsDets.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								IbaConsDet current = enumerator.Current;
								CuentaIBS newCuentaIBS = CuentaIBS.getNewCuentaIBS(current);
								ListItem listItem = new ListItem();
								switch (this.TipoComboCuentaFormato)
								{
									case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.Cuenta:
									{
										listItem.Text = Formatos.formatoCuenta(current.SNroCuenta);
										break;
									}
									case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponible:
									{
										listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.SDisponible)));
										break;
									}
									case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponibleDescripcion:
									{
										listItem.Text = string.Concat(new string[] { Formatos.formatoCuenta(current.SNroCuenta), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.SDisponible)), " ", current.SDescipcionproducto.Trim().Substring(0, 2) });
										break;
									}
									case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcion:
									{
										listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " - ", current.SDescipcionproducto.Trim().Substring(0, 2));
										break;
									}
									case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcionCompleto:
									{
										listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " ", current.SDescipcionproducto);
										break;
									}
								}
								listItem.Value = newCuentaIBS.Key;
								this.ListaCuentas.Add(newCuentaIBS);
								this.Items.Add(listItem);
							}
							break;
						}
						finally
						{
							((IDisposable)(object)enumerator).Dispose();
						}
						break;
					}
                    case IBBAV.UserControls.BAVCommons.TipoCombo.ExtraEfectivoCte:
                    {
                        String cedula = afiliado.sCedula;
                        cedula = 'V' + cedula.PadLeft(9, '0');
                        TarjetaConsulta[] consultaExtraefectivo = HelperExtracredito.consulta(cedula);
                        List<TarjetaConsulta> tarjeta = new List<TarjetaConsulta>();
                        tarjeta.AddRange(consultaExtraefectivo);
                        this.ListaExtraEfectivo = new List<TarjetaConsulta>();
                        List<TarjetaConsulta>.Enumerator enumerator = tarjeta.GetEnumerator();

                        try
                        {
                            while (enumerator.MoveNext())
                            {
                                TarjetaConsulta current = enumerator.Current;
                                TarjetaConsulta newExtraEfect = current;
                                ListItem listItem = new ListItem();
                                    //Luis: Original
                                    //listItem.Text = string.Concat(Formatos.formatoCuenta(current.numero), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.disponible)));
                                    //listItem.Text = string.Concat(current.marca, " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.disponible)));
                                    switch (this.TipoComboCuentaFormato)
                                    {
                                        case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponible:
                                            {
                                                listItem.Text = string.Concat(Formatos.formatoCuenta(current.numero), " Disponible ", current.disponible);
                                                break;
                                            }
                                        case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponibleDescripcion:
                                            {
                                                listItem.Text = string.Concat(new string[] { Formatos.formatoCuenta(current.numero), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.disponible)), " ", current.marca.Trim().Substring(0, 2) });
                                                break;
                                            }
                                        case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcion:
                                            {
                                                listItem.Text = string.Concat(Formatos.formatoCuenta(current.numero), " - ", current.marca.Trim().Substring(0, 2));
                                                break;
                                            }
                                        case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcionCompleto:
                                            {
                                                listItem.Text = string.Concat(Formatos.formatoCuenta(current.numero), " ", current.marca);
                                                break;
                                            }
                                    }
                                    listItem.Value = newExtraEfect.Key;
                                this.ListaExtraEfectivo.Add(newExtraEfect);
                                this.Items.Add(listItem);
                            }
                            break;
                        }
                        finally
                        {
                            ((IDisposable)(object)enumerator).Dispose();
                        }
                    }
                    case IBBAV.UserControls.BAVCommons.TipoCombo.TarjetasCliente:
                    {
                        long aFCodCliente = afiliado.AF_CodCliente;
                        RespuestaIbaCons respuestaIbaCon = HelperIbs.ibsConsultaCtas(aFCodCliente.ToString(), afiliado.sAF_Rif, this.TipoConsultaCuentasIBS);
                        List<IbaConsDet> ibaConsDets = new List<IbaConsDet>();
                        ibaConsDets.AddRange(respuestaIbaCon.sdjvCuentas.sdsjvDetalle);
                        // Agregado 02/05/2019 por Liliana Guerra para ocultar las cuentas JURIDICA en la sesión natural
                        ibaConsDets = ibaConsDets.FindAll((IbaConsDet x) => x.STipocuenta.Equals("TDC"));

                        this.ListaCuentas = new List<CuentaIBS>();
                        List<IbaConsDet>.Enumerator enumerator = ibaConsDets.GetEnumerator();
                        try
                        {
                            while (enumerator.MoveNext())
                            {
                                IbaConsDet current = enumerator.Current;
                                CuentaIBS newCuentaIBS = CuentaIBS.getNewCuentaIBS(current);
                                ListItem listItem = new ListItem();
                                switch (this.TipoComboCuentaFormato)
                                {
                                    case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.Cuenta:
                                        {
                                            listItem.Text = Formatos.formatoCuenta(current.SNroCuenta);
                                            break;
                                        }
                                    case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponible:
                                        {
                                            listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.SDisponible)));
                                            break;
                                        }
                                    case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaConDisponibleDescripcion:
                                        {
                                            listItem.Text = string.Concat(new string[] { Formatos.formatoCuenta(current.SNroCuenta), " Disponible ", Formatos.formatoMonto(Formatos.ISOToDecimal(current.SDisponible)), " ", current.SDescipcionproducto.Trim().Substring(0, 2) });
                                            break;
                                        }
                                    case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcion:
                                        {
                                            listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " - ", current.SDescipcionproducto.Trim().Substring(0, 2));
                                            break;
                                        }
                                    case IBBAV.UserControls.BAVCommons.TipoComboCuentaFormato.CuentaDescripcionCompleto:
                                        {
                                            listItem.Text = string.Concat(Formatos.formatoCuenta(current.SNroCuenta), " ", current.SDescipcionproducto);
                                            break;
                                        }
                                }
                                listItem.Value = newCuentaIBS.Key;
                                this.ListaCuentas.Add(newCuentaIBS);
                                this.Items.Add(listItem);
                            }
                            break;
                        }
                        finally
                        {
                            ((IDisposable)(object)enumerator).Dispose();
                        }
                        break;
                    }
                    case IBBAV.UserControls.BAVCommons.TipoCombo.CuentasFavoritos:
					{
						this.ListaAfiliadoFavoritos = new List<AfiliadoFavorito>();
						List<AfiliadoFavorito> afiliadoFavoritos = HelperFavorito.AfiliadoFavoritosGrupoGetByAfiliado(afiliado.nAF_Id, this.TipoCuentaFavoritos);
						List<AfiliadoFavorito>.Enumerator enumerator1 = afiliadoFavoritos.GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								AfiliadoFavorito afiliadoFavorito = enumerator1.Current;
								ListItem key = new ListItem()
								{
									Text = string.Concat(new string[] { afiliadoFavorito.NumeroInstrumento, " | ", afiliadoFavorito.Beneficiario, " | ", afiliadoFavorito.Descripcion })
								};
								afiliadoFavorito.Key = CryptoUtils.EncryptMD5(afiliadoFavorito.NumeroInstrumento);
								key.Value = afiliadoFavorito.Key;
								this.ListaAfiliadoFavoritos.Add(afiliadoFavorito);
								this.Items.Add(key);
							}
							break;
						}
						finally
						{
							((IDisposable)(object)enumerator1).Dispose();
						}
						break;
					}
					case IBBAV.UserControls.BAVCommons.TipoCombo.TiposServicios:
					{
						this.ListaTiposServicios = new List<Servicios>();
						List<Servicios>.Enumerator enumerator2 = HelperServicios.BDI_ServiciosGet(0).GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								Servicios servicio = enumerator2.Current;
								ListItem listItem1 = new ListItem()
								{
									Text = servicio.SE_Nombre,
									Value = servicio.Key
								};
								this.ListaTiposServicios.Add(servicio);
								this.Items.Add(listItem1);
							}
							break;
						}
						finally
						{
							((IDisposable)(object)enumerator2).Dispose();
						}
						break;
					}
					case IBBAV.UserControls.BAVCommons.TipoCombo.TiposFavoritos:
					{
						this.ListaTipoFavorito = new List<TipoFavorito>();
						List<TipoFavorito>.Enumerator enumerator3 = HelperFavorito.TipoFavoritoGetOne(0).GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								TipoFavorito tipoFavorito = enumerator3.Current;
								ListItem listItem2 = new ListItem()
								{
									Text = tipoFavorito.Descripcion,
									Value = tipoFavorito.Key
								};
								this.ListaTipoFavorito.Add(tipoFavorito);
								this.Items.Add(listItem2);
							}
							break;
						}
						finally
						{
							((IDisposable)(object)enumerator3).Dispose();
						}
						break;
					}
					case IBBAV.UserControls.BAVCommons.TipoCombo.TiposFavoritosEdc:
					{
						this.ListaTiposServicios = new List<Servicios>();
						List<Servicios>.Enumerator enumerator4 = HelperServicios.BDI_ServiciosGet(1).GetEnumerator();
						try
						{
							while (enumerator4.MoveNext())
							{
								Servicios current1 = enumerator4.Current;
								ListItem listItem3 = new ListItem()
								{
									Text = current1.SE_Nombre,
									Value = current1.Key
								};
								this.ListaTiposServicios.Add(current1);
								this.Items.Add(listItem3);
							}
							break;
						}
						finally
						{
							((IDisposable)(object)enumerator4).Dispose();
						}
						break;
					}
				}
				if (this.SetValue != string.Empty)
				{
					this.ClearSelection();
					ListItem listItem4 = this.Items.FindByValue(this.SetValue);
					if (listItem4 != null)
					{
						listItem4.Selected = true;
					}
					this.SetValue = string.Empty;
				}
			}
		}

        [Bindable(false)]
        [Localizable(false)]
        public TarjetaConsulta getExtraEfectivo(ListItem item)
        {
            TarjetaConsulta extEfectivo = this.ListaExtraEfectivo.Find((TarjetaConsulta obj) => obj.Key.Equals(item.Value));
            return extEfectivo;
        }

        [Localizable(false)]
        public TarjetaConsulta getExtraEfectivo()
        {
            TarjetaConsulta extEfectivo = null;
            if (this.SelectedItem != null)
            {
                extEfectivo = this.getExtraEfectivo(this.SelectedItem);
            }
            return extEfectivo;
        }

        [Bindable(false)]
		[Localizable(false)]
		public AfiliadoFavorito getAfiliadoFavorito(ListItem item)
		{
			AfiliadoFavorito afiliadoFavorito = this.ListaAfiliadoFavoritos.Find((AfiliadoFavorito obj) => obj.Key.Equals(item.Value));
			return afiliadoFavorito;
		}

		[Localizable(false)]
		public AfiliadoFavorito getAfiliadoFavorito()
		{
			return this.getAfiliadoFavorito(this.SelectedItem);
		}

		[Bindable(false)]
		[Localizable(false)]
		public CuentaIBS getCuenta(ListItem item)
		{
			CuentaIBS cuentaIB = this.ListaCuentas.Find((CuentaIBS obj) => obj.Key.Equals(item.Value));
			return cuentaIB;
		}

		[Localizable(false)]
		public CuentaIBS getCuenta()
		{
			CuentaIBS cuenta = null;
			if (this.SelectedItem != null)
			{
				cuenta = this.getCuenta(this.SelectedItem);
			}
			return cuenta;
		}

		[Bindable(false)]
		[Localizable(false)]
		public Servicios getServicio(ListItem item)
		{
			Servicios servicio = this.ListaTiposServicios.Find((Servicios obj) => obj.Key.Equals(item.Value));
			return servicio;
		}

		[Localizable(false)]
		public Servicios getServicio()
		{
			return this.getServicio(this.SelectedItem);
		}

		[Bindable(false)]
		[Localizable(false)]
		public TipoFavorito getTipoFavorito(ListItem item)
		{
			TipoFavorito tipoFavorito = this.ListaTipoFavorito.Find((TipoFavorito obj) => obj.Key.Equals(item.Value));
			return tipoFavorito;
		}

		[Localizable(false)]
		public TipoFavorito getTipoFavorito()
		{
			return this.getTipoFavorito(this.SelectedItem);
		}
	}
}