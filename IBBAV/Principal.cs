using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace IBBAV
{
	public class Principal : PrincipalNormal
	{
		public bool sesionnovalida;

		public string Path = string.Empty;

		private System.Web.UI.ScriptManager manager;

		private string path = string.Empty;

		public IBBAV.Entidades.Afiliado Afiliado
		{
			get
			{
				return (IBBAV.Entidades.Afiliado)this.ViewState["Afiliado"];
			}
			set
			{
				this.ViewState["Afiliado"] = value;
			}
		}

		public string CuentaAdministrativa1
		{
			get
			{
				string str;
				str = (this.ViewState["CuentaAdministrativa1"] != null ? this.ViewState["CuentaAdministrativa1"].ToString() : string.Empty);
				return str;
			}
			set
			{
				this.ViewState["CuentaAdministrativa1"] = value;
			}
		}

		public string CuentaAdministrativa2
		{
			get
			{
				string str;
				str = (this.ViewState["CuentaAdministrativa2"] != null ? this.ViewState["CuentaAdministrativa2"].ToString() : string.Empty);
				return str;
			}
			set
			{
				this.ViewState["CuentaAdministrativa2"] = value;
			}
		}

		public decimal LimiteDiario
		{
			get
			{
				decimal num;
				num = (this.ViewState["LimiteDiario"] != null ? decimal.Parse(this.ViewState["LimiteDiario"].ToString()) : new decimal(0));
				return num;
			}
			set
			{
				this.ViewState["LimiteDiario"] = value;
			}
		}

		public decimal Max
		{
			get
			{
				decimal num;
				num = (this.ViewState["Max"] != null ? decimal.Parse(this.ViewState["Max"].ToString()) : new decimal(0));
				return num;
			}
			set
			{
				this.ViewState["Max"] = value;
			}
		}

		public decimal Min
		{
			get
			{
				decimal num;
				num = (this.ViewState["Min"] != null ? decimal.Parse(this.ViewState["Min"].ToString()) : new decimal(0));
				return num;
			}
			set
			{
				this.ViewState["Min"] = value;
			}
		}

        //Agregado 21/07/2018 por Liliana Guerra
        public decimal MtoLimiteTrans
        {
            get
            {
                decimal num;
                num = (this.ViewState["MtoLimiteTrans"] != null ? decimal.Parse(this.ViewState["MtoLimiteTrans"].ToString()) : new decimal(0));
                return num;
            }
            set
            {
                this.ViewState["MtoLimiteTrans"] = value;
            }
        }

        public decimal MontoComision
		{
			get
			{
				decimal num;
				num = (this.ViewState["MontoComision"] != null ? decimal.Parse(this.ViewState["MontoComision"].ToString()) : new decimal(0));
				return num;
			}
			set
			{
				this.ViewState["MontoComision"] = value;
			}
		}

		public string NombrePantalla
		{
			get
			{
				string str;
				str = (this.ViewState["NombrePantalla"] != null ? this.ViewState["NombrePantalla"].ToString() : string.Empty);
				return str;
			}
			set
			{
				this.ViewState["NombrePantalla"] = value;
			}
		}

		public int sCod
		{
			get
			{
				int num;
				num = (this.ViewState["sCod"] != null ? int.Parse(this.ViewState["sCod"].ToString()) : 0);
				return num;
			}
			set
			{
				this.ViewState["sCod"] = value;
			}
		}

		public string SessionId
		{
			get
			{
				string str;
				str = (this.ViewState["SessionId"] != null ? this.ViewState["SessionId"].ToString() : string.Empty);
				return str;
			}
			set
			{
				this.ViewState["SessionId"] = value;
			}
		}

		public int Tipo_Seguridad
		{
			get
			{
				return Convert.ToInt32(this.ViewState["Tipo_Seguridad"]);
			}
			set
			{
				this.ViewState["Tipo_Seguridad"] = value;
			}
		}

		public Principal()
		{
		}

		protected override void OnLoad(EventArgs e)
		{
			this.path = base.ResolveUrl("~");
			if ((!this.path.Equals("") || base.IsCallback ? false : this.Afiliado == null))
			{
				this.sesionnovalida = true;
				base.Response.Redirect("~/login.aspx?msg=1");
			}
			else
			{
				base.OnLoad(e);
			}
		}

		protected override void OnPreLoad(EventArgs e)
		{
			this.sCod = (string.IsNullOrEmpty(base.Request.QueryString["sCod"]) ? 0 : int.Parse(base.Request.QueryString["sCod"]));
			try
			{
				TransMaxMin maxMinTransaccion = HelperMenu.getMaxMinTransaccion(this.sCod, 0);
				if (maxMinTransaccion != null)
				{
					this.Max = maxMinTransaccion.MD_Mto_Max_Bco;
					this.Min = maxMinTransaccion.MD_Mto_Min_Bco;
					// Agregado 21/07/2018 por Liliana Guerra Limite por transacción
                    this.MtoLimiteTrans = maxMinTransaccion.MD_Mto_Limite_Trans;
                    this.LimiteDiario = maxMinTransaccion.MD_Mto_Limite_Diario;
					if (string.IsNullOrEmpty(this.NombrePantalla))
					{
						this.NombrePantalla = maxMinTransaccion.MD_Desc_Trans;
					}
					this.MontoComision = maxMinTransaccion.MD_Mto_Comision;
					this.CuentaAdministrativa1 = maxMinTransaccion.MD_Cta_Administrativa1;
					this.CuentaAdministrativa2 = maxMinTransaccion.MD_Cta_administrativa2;
					this.Tipo_Seguridad = maxMinTransaccion.MD_Tipo_Seguridad;
				}
			}
			catch (IBException bException)
			{
			}
			this.path = base.ResolveUrl("~");
			if (!base.IsCallback)
			{
				this.SessionId = base.Request.QueryString["SessionId"];
				if (string.IsNullOrEmpty(this.SessionId))
				{
					this.SessionId = this.Session["SessionId"] as string;
				}
				HelperSession.ValidateSession(this.SessionId);
				this.Afiliado = this.Session["Afiliado"] as IBBAV.Entidades.Afiliado;
				if (this.Afiliado == null)
				{
					base.Response.Redirect("~/Login.aspx?msg=2");
				}
				else if (this.Afiliado.nES_Id == (long)4)
				{
					this.sesionnovalida = true;
					base.Response.Redirect("~/Login.aspx?msg=4");
				}
				else if (this.Afiliado.nES_Id != (long)6)
				{
					if (this.Afiliado.nES_Id == (long)8)
					{
						this.sesionnovalida = true;
						base.Response.Redirect("~/Login.aspx?msg=6");
						return;
					}
					if (this.Context.Items["Afiliado"] == null)
					{
						this.Context.Items.Add("Afiliado", this.Afiliado);
					}
					else
					{
						this.Context.Items["Afiliado"] = this.Afiliado;
					}
					if (!base.Request.RawUrl.Contains("CambioClave.aspx"))
					{
						if (this.Afiliado.AF_FechaPassword == new DateTime(2000, 1, 1))
						{
							base.Response.Redirect(string.Concat("~/pages/IB/Claves/CambioClave.aspx?sCod=20&Type=0&SessionId=", this.SessionId));
						}
						else if ((this.Afiliado.AF_DiasPassword == 0 ? false : DateAndTime.DateDiff(DateInterval.Day, this.Afiliado.AF_FechaPassword, DateTime.Now) >= (long)this.Afiliado.AF_DiasPassword))
						{
							base.Response.Redirect(string.Concat("~/pages/IB/Claves/CambioClave.aspx?sCod=20&Type=1&SessionId=", this.SessionId));
						}
					}
					base.OnPreLoad(e);
					return;
				}
				else
				{
					this.sesionnovalida = true;
					base.Response.Redirect("~/Login.aspx?msg=5");
				}
			}
			else
			{
				base.OnPreLoad(e);
				return;
			}
		}
	}
}