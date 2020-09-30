using Functions;
using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using IBBAV.UserControls.BAVCommons;
using IBBAV.WsIbsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IBBAV.pages.Consultas.Cuentas
{
	public class EstadoCuentaBusqueda : Principal
	{
		private const int regpagina1 = 45;

		private const int maxreg = 60;

		private int totalpaginas = 1;

		private StringBuilder sb;

		protected Image imgGrid;

		protected DropDownListCuentas ddlCuenta;

		protected DropDownList ddlMes;

		protected UpdateProgress PageUpdateProgress;

		protected UpdatePanel UpdatePanel2;

		protected Panel panelBotones;

		protected Button btnImprimir;

		protected Button btnRegresar;

		protected Literal liBotones;

		protected ImageButton btnExcel;

		protected ImageButton btnTXT;

		protected Repeater rptCabecera;

		private DataTable dataDet
		{
			get
			{
				return this.ViewState["dataDet"] as DataTable;
			}
			set
			{
				this.ViewState["dataDet"] = value;
			}
		}

		private DataTable dataEnc
		{
			get
			{
				return this.ViewState["dataEnc"] as DataTable;
			}
			set
			{
				this.ViewState["dataEnc"] = value;
			}
		}

		private int PaginaActual
		{
			get
			{
				object item = this.ViewState["PaginaActual"];
				return (item != null ? (int)item : 1);
			}
			set
			{
				this.ViewState["PaginaActual"] = value;
			}
		}

		private int RegActual
		{
			get
			{
				object item = this.ViewState["RegActual"];
				return (item != null ? (int)item : 1);
			}
			set
			{
				this.ViewState["RegActual"] = value;
			}
		}

		public EstadoCuentaBusqueda()
		{
		}

		private void binddata()
		{
			string sNroCuenta = this.ddlCuenta.getCuenta().SNroCuenta;
			try
			{
				RespuestaIfcedoctadsjv respuestaIfcedoctadsjv = HelperIbs.ibsEstadoCta(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, sNroCuenta, this.ddlMes.SelectedValue);
				if (respuestaIfcedoctadsjv.ifcedoctadsjv.ifcedoctadsjvDet.Length == 0)
				{
					WebUtils.MessageBox2005(this, "No existen movimientos para este mes");
					return;
				}
				else
				{
					if (this.dataEnc == null)
					{
						this.dataEnc = new DataTable();
						this.dataEnc.Columns.Add("SCuenta", typeof(string));
						this.dataEnc.Columns.Add("SNombre", typeof(string));
						this.dataEnc.Columns.Add("SDireccion1", typeof(string));
						this.dataEnc.Columns.Add("SDireccion2", typeof(string));
						this.dataEnc.Columns.Add("SDireccion3", typeof(string));
						this.dataEnc.Columns.Add("SDireccion4", typeof(string));
						this.dataEnc.Columns.Add("SNombreProdcto", typeof(string));
						this.dataEnc.Columns.Add("SSaldoIniMes", typeof(string));
						this.dataEnc.Columns.Add("SDepoEfec", typeof(string));
						this.dataEnc.Columns.Add("SInteres", typeof(string));
						this.dataEnc.Columns.Add("SInteresCant", typeof(string));
						this.dataEnc.Columns.Add("SOtrosCredCta", typeof(string));
						this.dataEnc.Columns.Add("SOtrosCredCtaCant", typeof(string));
						this.dataEnc.Columns.Add("SCheqpagados", typeof(string));
						this.dataEnc.Columns.Add("SCheqpagadosCant", typeof(string));
						this.dataEnc.Columns.Add("SITF", typeof(string));
						this.dataEnc.Columns.Add("SITFCant", typeof(string));
						this.dataEnc.Columns.Add("SOtrosDebCta", typeof(string));
						this.dataEnc.Columns.Add("SCantOtrosDebCta", typeof(string));
						this.dataEnc.Columns.Add("SSaldoFinMes", typeof(string));
						this.dataEnc.Columns.Add("SSaldoGirable", typeof(string));
						this.dataEnc.Columns.Add("SLimitSgiro", typeof(string));
						this.dataEnc.Columns.Add("SFechaDesde", typeof(string));
						this.dataEnc.Columns.Add("SFechaHasta", typeof(string));
						this.dataEnc.Columns.Add("STotalDebitos", typeof(string));
						this.dataEnc.Columns.Add("STotalCreditos", typeof(string));
						this.dataEnc.Columns.Add("SSaldoFinal", typeof(string));
						this.dataEnc.Columns.Add("pagina", typeof(int));
						this.dataEnc.Columns.Add("totalpaginas", typeof(int));
					}
					this.dataEnc.Rows.Clear();
					if (this.dataDet == null)
					{
						this.dataDet = new DataTable();
						this.dataDet.Columns.Add("FechaOperacion", typeof(string));
						this.dataDet.Columns.Add("FechaEfectiva", typeof(string));
						this.dataDet.Columns.Add("NumeroDocumento", typeof(string));
						this.dataDet.Columns.Add("Descripcion", typeof(string));
						this.dataDet.Columns.Add("Cargos", typeof(string));
						this.dataDet.Columns.Add("Abonos", typeof(string));
						this.dataDet.Columns.Add("Saldo", typeof(string));
					}
					this.dataDet.Rows.Clear();
					DataRow str = this.dataDet.NewRow();
					str["Descripcion"] = "SALDO INICIAL :...";
					str["Saldo"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SSaldoIniMes));
					this.dataDet.Rows.Add(str);
					IfcedoctadsjvDet[] ifcedoctadsjvDetArray = respuestaIfcedoctadsjv.ifcedoctadsjv.ifcedoctadsjvDet;
					for (int i = 0; i < (int)ifcedoctadsjvDetArray.Length; i++)
					{
						IfcedoctadsjvDet ifcedoctadsjvDet = ifcedoctadsjvDetArray[i];
						str = this.dataDet.NewRow();
						DateTime fecha = Formatos.ISOToFecha(ifcedoctadsjvDet.SFechaProc);
						str["FechaOperacion"] = fecha.ToString("dd/MM/yyyy");
						DateTime dateTime = Formatos.ISOToFecha(ifcedoctadsjvDet.SFechaValor);
						str["FechaEfectiva"] = dateTime.ToString("dd/MM/yyyy");
						str["NumeroDocumento"] = ifcedoctadsjvDet.SChqRef;
						str["Descripcion"] = string.Concat(ifcedoctadsjvDet.SDesctrans, ifcedoctadsjvDet.SDescripcion1);
						if (ifcedoctadsjvDet.SIndDebCre.Equals("0"))
						{
							str["Cargos"] = Formatos.formatoMonto(ifcedoctadsjvDet.SMonto);
						}
						if (ifcedoctadsjvDet.SIndDebCre.Equals("5"))
						{
							str["Abonos"] = Formatos.formatoMonto(ifcedoctadsjvDet.SMonto);
						}
						str["Saldo"] = Formatos.formatoMonto(ifcedoctadsjvDet.SBalanceFin);
						this.dataDet.Rows.Add(str);
					}
					this.totalpaginas = 1;
					if (this.dataDet.Rows.Count > 45)
					{
						double num = Convert.ToDouble(this.dataDet.Rows.Count - 45) / Convert.ToDouble(60);
						string str1 = num.ToString().Replace(",", ".");
						int num1 = Convert.ToInt32(str1.Substring(0, str1.IndexOf(".")));
						if ((float)num - (float)num1 > 0f)
						{
							EstadoCuentaBusqueda estadoCuentaBusqueda = this;
							estadoCuentaBusqueda.totalpaginas = estadoCuentaBusqueda.totalpaginas + num1 + 1;
						}
					}
					this.sb = new StringBuilder();
					this.sb.Append("var next = 1;\n");
					this.sb.Append("function plus(){ next++; mostrar(next); }\n");
					this.sb.Append("function minus(){ next--; mostrar(next); }\n");
					this.sb.Append(string.Concat("var paneles = new Array(", this.totalpaginas, ");\n"));
					this.sb.Append("function mostrar(x)\n");
					this.sb.Append("{\n");
					this.sb.Append("var i; \n");
					this.sb.Append("for(i = 0;i < paneles.length; i++)\n");
					this.sb.Append("{\n");
					this.sb.Append("$(paneles[i]).style.display='none';\n");
					this.sb.Append("}\n");
					this.sb.Append("$(paneles[x-1]).style.display='block';\n");
					this.sb.Append("next = x;\n");
					this.sb.Append("$('btnNext').disabled = ( next == paneles.length );\n");
					this.sb.Append("$('btnPrevious').disabled = ( next == 1 );\n");
					this.sb.Append("$('divPaginas').innerHTML ='Página ' + next + ' de ' + paneles.length;\n");
					this.sb.Append("};");
					this.dataEnc.Rows.Clear();
					for (int j = 0; j < this.totalpaginas; j++)
					{
						DataRow sNombre = this.dataEnc.NewRow();
						sNombre["SCuenta"] = Formatos.formatoCuenta(sNroCuenta);
						sNombre["SNombre"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SNombre;
						sNombre["SDireccion1"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SDireccion1;
						sNombre["SDireccion2"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SDireccion2;
						sNombre["SDireccion3"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SDireccion3;
						sNombre["SDireccion4"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SDireccion4;
						sNombre["SNombreProdcto"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SNombreProdcto;
						DateTime fecha1 = Formatos.ISOToFecha(respuestaIfcedoctadsjv.ifcedoctadsjv.SFechaDesde);
						sNombre["SFechaDesde"] = fecha1.ToString("dd/MM/yyyy");
						sNombre["SSaldoIniMes"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SSaldoIniMes));
						sNombre["SDepoEfec"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SDepoEfec));
						sNombre["SInteres"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SInteres));
						sNombre["SInteresCant"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SCantIntereses;
						sNombre["SOtrosCredCta"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SOtrosCredCta));
						sNombre["SOtrosCredCtaCant"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SCantOtrosCredCta;
						sNombre["SCheqpagados"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SCheqpagados));
						sNombre["SCheqpagadosCant"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SCantCheqpagados;
						sNombre["SITF"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SITF));
						sNombre["SITFCant"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SCantITF;
						sNombre["SOtrosDebCta"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SOtrosDebCta));
						sNombre["SCantOtrosDebCta"] = respuestaIfcedoctadsjv.ifcedoctadsjv.SCantOtrosDebCta;
						DateTime dateTime1 = Formatos.ISOToFecha(respuestaIfcedoctadsjv.ifcedoctadsjv.SFechaHasta);
						sNombre["SFechaHasta"] = dateTime1.ToString("dd/MM/yyyy");
						sNombre["SSaldoFinMes"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SSaldoFinMes));
						sNombre["SSaldoGirable"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SSaldoGirable));
						sNombre["SLimitSgiro"] = Formatos.formatoMonto(Formatos.ISOToDecimal(respuestaIfcedoctadsjv.ifcedoctadsjv.SLimitSgiro));
						sNombre["totalpaginas"] = this.totalpaginas;
						this.dataEnc.Rows.Add(sNombre);
					}
					this.rptCabecera.DataSource = this.dataEnc;
					this.rptCabecera.DataBind();
					this.panelBotones.Visible = true;
				}
			}
			catch (IBException bException)
			{
				WebUtils.MessageBox2005(this, bException.IBMessage);
				return;
			}
			Literal literal = this.liBotones;
			object[] objArray = new object[] { "<table><tr><td><div id='divPaginas'>Página 1 de ", this.totalpaginas, "</div></td><td><input id='btnPrevious' type='button' value='<' onclick='minus();' disabled=true style='width:20px' /><input id='btnNext' type='button' value='>' onclick='plus();' style='width:20px' ", null, null };
			objArray[3] = (this.totalpaginas == 1 ? "disabled=true" : "");
			objArray[4] = "/></td></tr></table>";
			literal.Text = string.Concat(objArray);
			System.Web.UI.Page page = this.Page;
			Type type = this.Page.GetType();
			Guid guid = Guid.NewGuid();
			System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, type, guid.ToString(), string.Concat("<script type='text/javascript' language='javascript'>", this.sb.ToString(), "</script>"), false);
		}

		protected void btnExcel_Click(object sender, ImageClickEventArgs e)
		{
			if (this.rptCabecera.Items.Count > 0)
			{
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(this.dataDet);
				DataGrid dataGrid = this.rptCabecera.Items[0].FindControl("dtgDetalle") as DataGrid;
				DataTable dataSource = dataGrid.DataSource as DataTable;
				dataGrid.DataSource = dataSet;
				dataGrid.DataBind();
				if ((dataGrid.DataSource == null ? false : ((DataSet)dataGrid.DataSource).Tables[0].Rows.Count > 0))
				{
					WebUtils.dataGridToExcel(dataGrid, "EstadoCuenta.xls");
					dataGrid.DataSource = dataSource;
					dataGrid.DataBind();
				}
				else
				{
					WebUtils.MessageBox2005(this, "No existen movimientos para exportar");
				}
			}
			else
			{
				WebUtils.MessageBox2005(this, "No existen movimientos para exportar");
			}
		}

		protected void btnRegresar_Click(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat("~/pages/Consolidada.aspx?sCod=1&SessionId=", base.SessionId));
		}

		protected void btnTXT_Click(object sender, ImageClickEventArgs e)
		{
			if (this.rptCabecera.Items.Count > 0)
			{
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(this.dataDet);
				DataGrid dataGrid = this.rptCabecera.Items[0].FindControl("dtgDetalle") as DataGrid;
				DataTable dataSource = dataGrid.DataSource as DataTable;
				dataGrid.DataSource = dataSet;
				dataGrid.DataBind();
				if ((dataGrid.DataSource == null ? false : ((DataSet)dataGrid.DataSource).Tables[0].Rows.Count > 0))
				{
					WebUtils.dataGridToTXT(dataGrid, "EstadoCuenta.txt");
					dataGrid.DataSource = dataSource;
					dataGrid.DataBind();
				}
				else
				{
					WebUtils.MessageBox2005(this, "No existen movimientos para exportar");
				}
			}
			else
			{
				WebUtils.MessageBox2005(this, "No existen movimientos para exportar");
			}
		}

		protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.ddlMes.Items.Clear();
				this.ddlMes.Items.Add("Seleccione un Mes");
				this.ddlMes.Enabled = false;
				if (this.ddlCuenta.SelectedIndex > 0)
				{
					RespuestaCmesdidsjv respuestaCmesdidsjv = HelperIbs.ibsConMesDisp(base.Afiliado.AF_CodCliente, base.Afiliado.sAF_Rif, this.ddlCuenta.getCuenta().SNroCuenta);
					string sVecMesDip = respuestaCmesdidsjv.cmesdidsjv.SVecMesDip;
					List<DateTime> dateTimes = new List<DateTime>();
					int num = 0;
					while (num < sVecMesDip.Length)
					{
						string str = sVecMesDip.Substring(num, 4);
						if (!str.Contains("0000"))
						{
							int num1 = int.Parse(str.Substring(0, 2));
							int num2 = int.Parse(str.Substring(2, 2));
							dateTimes.Add(new DateTime(num2, num1, 1));
							num += 3;
							num++;
						}
						else
						{
							break;
						}
					}
					dateTimes.Sort((DateTime i, DateTime j) => ((IComparable)(object)j).CompareTo(i));
					foreach (DateTime dateTime in dateTimes)
					{
						ListItem listItem = new ListItem()
						{
							Text = Calendario.MonthName(dateTime.Month),
							Value = dateTime.ToString("MMyy")
						};
						this.ddlMes.Items.Add(listItem);
						this.ddlMes.Enabled = true;
					}
				}
				else
				{
					this.ddlCuenta.ClearSelection();
					this.ddlMes_SelectedIndexChanged(null, null);
				}
			}
			catch (IBException bException)
			{
				WebUtils.MessageBox2005(this, bException.IBMessage);
			}
		}

		protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag;
			this.liBotones.Text = string.Empty;
			this.panelBotones.Visible = false;
			this.totalpaginas = 1;
			this.RegActual = 0;
			this.PaginaActual = 1;
			if (this.ddlMes.SelectedIndex <= 0)
			{
				flag = false;
			}
			else
			{
				flag = (!base.IsCallback ? true : !base.IsPostBack);
			}
			if (flag)
			{
				this.binddata();
				this.LogEdoCuenta();
			}
			this.UpdatePanel2.Update();
		}

		private void dg_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
			{
				foreach (TableCell cell in e.Item.Cells)
				{
					cell.Attributes.Add("style", "padding-right:2px;padding-left:2px");
				}
			}
		}

		protected bool LogEdoCuenta()
		{
			bool flag = false;
			try
			{
				string sNroCuenta = this.ddlCuenta.getCuenta().SNroCuenta;
				flag = HelperGlobal.LogTransAdd(new DataLog()
				{
					NAF_Id = base.Afiliado.nAF_Id,
					SAF_NombreUsuario = base.Afiliado.sAF_NombreUsuario,
					DtFecha_Trans = DateTime.Now.Date,
					STime_Trans = DateTime.Now.ToLongTimeString(),
					SCod_Trans = "COEDO",
					SAF_IP = base.Afiliado.sIP,
					SBanco = string.Empty,
					SCuenta_Origen = sNroCuenta,
					SCuenta_Destino = string.Empty,
					SMonto = string.Empty,
					STipo_Tarjeta = string.Empty,
					SBeneficiario = string.Empty,
					SCedula_Id_B = string.Empty,
					SSerial_Chequera = string.Empty,
					SCheques = string.Empty,
					STitular = base.Afiliado.sCO_Nombres,
					ICantidad = 0,
					SReferencia = string.Empty,
					SConcepto = string.Concat("Consulta de Estado de Cuenta Corriente de la cuenta ", sNroCuenta, " en el mes de ", this.ddlMes.SelectedItem.Text),
					SMotivo_Suspension = string.Empty,
					SDir_Envio_Chequera = string.Empty
				});
			}
			catch (IBException bException)
			{
			}
			return flag;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.btnImprimir.Attributes.Add("onclick", string.Concat("BAV.Form.PrintToFrame('Estado de cuenta', paneles,'", base.ResolveClientUrl("~/css/main.css"), "');return false;"));
			if (!base.IsPostBack)
			{
				if (this.Session["mes"] != null)
				{
					this.Session.Remove("mes");
				}
				try
				{
					this.ddlCuenta.HasTextoInicial = true;
					this.ddlCuenta.TextoInicial = "Seleccione una cuenta a consultar";
					this.ddlCuenta.TipoCombo = TipoCombo.CuentasCliente;
					this.ddlCuenta.TipoCuentaConsulta = TipoCuentaConsulta.TodasCorrientes;
					this.ddlCuenta.TipoComboCuentaFormato = TipoComboCuentaFormato.Cuenta;
					this.ddlCuenta.BindCombo();
				}
				catch (IBException bException)
				{
					WebUtils.MessageBox2005(this, bException.IBMessage);
					return;
				}
				this.ddlMes.Items.Add("Seleccione un Mes");
			}
		}

		protected void rptCabecera_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
			{
				DataGrid dataGrid = e.Item.FindControl("dtgDetalle") as DataGrid;
				if (dataGrid != null)
				{
					Panel panel = e.Item.FindControl("panel") as Panel;
					DataTable dataTable = new DataTable(string.Concat("table", this.PaginaActual));
					dataTable = this.dataDet.Clone();
					if (this.PaginaActual == 1)
					{
						this.sb.Append(string.Concat("paneles[0] = '", panel.ClientID, "';"));
						int j = 0;
						while (true)
						{
							if ((j >= 45 ? true : this.dataDet.Rows.Count == j))
							{
								break;
							}
							DataRow dataRow = dataTable.NewRow();
							for (int k = 0; k < this.dataDet.Columns.Count; k++)
							{
								dataRow[this.dataDet.Columns[k].ColumnName] = this.dataDet.Rows[j][this.dataDet.Columns[k].ColumnName];
							}
							dataTable.Rows.Add(dataRow);
							j++;
						}
						this.RegActual = 45;
					}
					else
					{
						StringBuilder stringBuilder = this.sb;
						stringBuilder.Append(string.Concat(new object[] { "paneles[", this.PaginaActual - 1, "] = '", panel.ClientID, "';" }));
						panel.Attributes.Add("style", "display:none");
						(e.Item.FindControl("trResumenTitulo") as HtmlTableRow).Visible = false;
						(e.Item.FindControl("trResumen") as HtmlTableRow).Visible = false;
						int count = 0;
						count = this.dataDet.Rows.Count;
						int num = 1;
						int regActual = this.RegActual;
						while (regActual < count)
						{
							DataRow item = dataTable.NewRow();
							for (int i = 0; i < this.dataDet.Columns.Count; i++)
							{
								item[this.dataDet.Columns[i].ColumnName] = this.dataDet.Rows[regActual][this.dataDet.Columns[i].ColumnName];
							}
							dataTable.Rows.Add(item);
							num++;
							if (num == 60)
							{
								this.RegActual = regActual + 1;
								break;
							}
							else
							{
								regActual++;
							}
						}
					}
					dataGrid.DataSource = dataTable;
					dataGrid.ItemDataBound += new DataGridItemEventHandler(this.dg_ItemDataBound);
					dataGrid.DataBind();
					Literal str = e.Item.FindControl("liPagina") as Literal;
					if (str != null)
					{
						str.Text = this.PaginaActual.ToString();
					}
					EstadoCuentaBusqueda estadoCuentaBusqueda = this;
					estadoCuentaBusqueda.PaginaActual = estadoCuentaBusqueda.PaginaActual + 1;
				}
			}
		}
	}
}