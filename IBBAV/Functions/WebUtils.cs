using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Functions
{
	public class WebUtils
	{
		private WebUtils()
		{
		}

		public static string ApplicationRootUrl()
		{
			string str = WebUtils.RootUrl();
			if (str.Substring(str.Length - 1, 1) == "/")
			{
				str = str.Substring(0, str.Length - 1);
			}
			str = string.Concat(str, HttpContext.Current.Request.ApplicationPath);
			if (str.Substring(str.Length - 1, 1) == "/")
			{
				str = str.Substring(0, str.Length - 1);
			}
			return str;
		}

		public static TableRow BuildTableRow(string caption, Control[] controls, string info)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell()
			{
				CssClass = "fieldTxt"
			};
			tableCell.Controls.Add(new LiteralControl(caption));
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell()
			{
				HorizontalAlign = HorizontalAlign.Left
			};
			Panel panel = new Panel()
			{
				CssClass = "form-group"
			};
			tableCell.CssClass = "fieldVal";
			if ((int)controls.Length > 1)
			{
				TableCell tableCell1 = tableCell;
				tableCell1.CssClass = string.Concat(tableCell1.CssClass, " form-inline ");
			}
			Control[] controlArray = controls;
			for (int i = 0; i < (int)controlArray.Length; i++)
			{
				Control control = controlArray[i];
				panel.Controls.Add(control);
			}
			tableCell.Controls.Add(panel);
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell()
			{
				CssClass = "fieldInf",
				Text = info
			};
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		public static DropDownList CreateDropDownListMonth(string id, string valor, string css)
		{
			DropDownList dropDownList = new DropDownList()
			{
				ID = id,
				CssClass = css
			};
			for (int i = 1; i < 13; i++)
			{
				string str = i.ToString().PadLeft(2, '0');
				dropDownList.Items.Add(new ListItem(str, i.ToString()));
			}
			ListItem listItem = dropDownList.Items.FindByValue(valor);
			if (listItem != null)
			{
				listItem.Selected = true;
			}
			return dropDownList;
		}

		public static DropDownList CreateDropDownListYear(string id, string valor, string css)
		{
			DropDownList dropDownList = new DropDownList()
			{
				ID = id,
				CssClass = css
			};
			int year = DateTime.Now.Year;
			for (int i = year; i < year + 10; i++)
			{
				int num = i - 2000;
				dropDownList.Items.Add(new ListItem(i.ToString(), num.ToString()));
			}
			ListItem listItem = dropDownList.Items.FindByValue(valor);
			if (listItem != null)
			{
				listItem.Selected = true;
			}
			return dropDownList;
		}

		public static TextBox CreateTextBox(string id, int maxLen, string value, bool isReadOnly, string css)
		{
			TextBox textBox = WebUtils.CreateTextBox(id, maxLen, string.Empty, string.Empty, value, isReadOnly, TextBoxMode.SingleLine, css);
			return textBox;
		}

		public static TextBox CreateTextBox(string id, int maxLen, string keyjs, string valuejs, string value, bool isReadOnly, string css)
		{
			TextBox textBox = WebUtils.CreateTextBox(id, maxLen, keyjs, valuejs, value, isReadOnly, TextBoxMode.SingleLine, css);
			return textBox;
		}

		public static TextBox CreateTextBox(string id, int maxLen, string keyjs, string valuejs, string value, bool isReadOnly, TextBoxMode textmode, string css)
		{
			List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
			if (keyjs != string.Empty)
			{
				keyValuePairs.Add(new KeyValuePair<string, string>(keyjs, valuejs));
			}
			TextBox textBox = WebUtils.CreateTextBox(id, maxLen, keyValuePairs, value, isReadOnly, textmode, css);
			return textBox;
		}

		public static TextBox CreateTextBox(string id, int maxLen, List<KeyValuePair<string, string>> js, string value, bool isReadOnly, TextBoxMode textmode, string css)
		{
			TextBox textBox = new TextBox()
			{
				ID = id,
				MaxLength = maxLen,
				Columns = maxLen,
				TextMode = textmode,
				CssClass = css
			};
			foreach (KeyValuePair<string, string> j in js)
			{
				string key = j.Key;
				string str = j.Value;
				if (key != string.Empty)
				{
					textBox.Attributes.Add(key, str);
				}
			}
			textBox.Text = value;
			textBox.ReadOnly = isReadOnly;
			return textBox;
		}

		public static void dataGridToExcel(DataGrid dtg, string nombrereporte)
		{
			HttpResponse response = HttpContext.Current.Response;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
			DataGrid dataGrid = new DataGrid();
			DataTable dataTable = new DataTable();
			if (dtg.Columns.Count > 0)
			{
				foreach (DataGridColumn dataGridColumn in dtg.Columns)
				{
					dataTable.Columns.Add(new DataColumn(dataGridColumn.HeaderText, typeof(string)));
				}
			}
			else
			{
				foreach (DataColumn column in ((DataSet)dtg.DataSource).Tables[0].Columns)
				{
					dataTable.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
				}
			}
			foreach (DataGridItem item in dtg.Items)
			{
				DataRow text = dataTable.NewRow();
				for (int i = 0; i < item.Cells.Count; i++)
				{
					if (item.Cells[i].HasControls())
					{
						Control control = item.Cells[i].Controls[1];
						if (control.GetType() == typeof(Literal))
						{
							text[i] = ((Literal)control).Text;
						}
						else if (control.GetType() == typeof(LinkButton))
						{
							text[i] = ((LinkButton)control).Text;
						}
						else if (control.GetType() == typeof(TextBox))
						{
							text[i] = ((TextBox)control).Text;
						}
						else if (control.GetType() == typeof(Label))
						{
							text[i] = ((Label)control).Text;
						}
						else if (control.GetType() == typeof(DropDownList))
						{
							text[i] = ((DropDownList)control).SelectedItem.Text;
						}
					}
					else
					{
						text[i] = item.Cells[i].Text;
					}
				}
				dataTable.Rows.Add(text);
			}
			List<string> strs = new List<string>();
			foreach (DataColumn dataColumn in dataTable.Columns)
			{
				if (dataColumn.ColumnName.StartsWith("Column"))
				{
					strs.Add(dataColumn.ColumnName);
				}
			}
			foreach (string str in strs)
			{
				dataTable.Columns.Remove(str);
			}
			dataGrid.DataSource = dataTable;
			dataGrid.DataBind();
			string str1 = "<style>table {mso-displayed-decimal-separator:\"\\,\"; mso-displayed-thousand-separator:\"\\.\";}.text { mso-number-format:\\@; }</style>";
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				for (int k = 0; k < dataGrid.Items[j].Cells.Count; k++)
				{
					if (dataTable.Columns[k].DataType == typeof(string))
					{
						dataGrid.Items[j].Cells[k].Attributes.Add("class", "text");
					}
				}
			}
			response.Clear();
			response.ClearHeaders();
			response.ClearContent();
			response.Buffer = true;
			response.AddHeader("content-disposition", string.Concat("attachment; filename=", nombrereporte));
			response.ContentType = "application/vnd.xls";
			response.ContentEncoding = Encoding.UTF8;
			if (HttpContext.Current.Request.RawUrl.ToUpper().Contains("HTTPS"))
			{
				response.AddHeader("Cache-Control", "max-age=0");
			}
			response.Charset = "";
			dataGrid.EnableViewState = false;
			dataGrid.Visible = true;
			StringWriter stringWriter = new StringWriter();
			dataGrid.RenderControl(new HtmlTextWriter(stringWriter));
			string str2 = "<html><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">";
			string str3 = "</head>";
			response.Write(string.Concat(new string[] { str2, str1, str3, stringWriter.ToString(), "</html>" }));
			response.Flush();
			response.End();
		}

		public static void dataGridToTXT(DataGrid dtg, string nombrereporte)
		{
			HttpResponse response = HttpContext.Current.Response;
			StringBuilder stringBuilder = new StringBuilder();
			List<string> strs = new List<string>();
			foreach (DataGridColumn column in dtg.Columns)
			{
				strs.Add(column.HeaderText);
			}
			stringBuilder.Append(string.Join("|", strs.ToArray()));
			stringBuilder.Append(Environment.NewLine);
			foreach (DataGridItem item in dtg.Items)
			{
				if ((item.ItemType == ListItemType.Item ? true : item.ItemType == ListItemType.AlternatingItem))
				{
					strs.Clear();
					foreach (TableCell cell in item.Cells)
					{
						strs.Add(cell.Text.Replace("&nbsp;", "").Replace("&NBSP;", ""));
					}
					stringBuilder.Append(string.Join("|", strs.ToArray()));
					stringBuilder.Append(Environment.NewLine);
				}
			}
			response.Clear();
			response.ClearHeaders();
			response.ClearContent();
			response.AddHeader("content-disposition", string.Concat("attachment;filename=", nombrereporte));
			response.Charset = "";
			if (HttpContext.Current.Request.RawUrl.ToUpper().Contains("HTTPS"))
			{
				response.AddHeader("Cache-Control", "max-age=0");
			}
			response.ContentType = "application/vnd.text";
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
			response.Write(stringBuilder.ToString());
			response.End();
		}

		public static Control FindControlRecursive(Control Root, string Id)
		{
			Control root;
			if (Root.ID != Id)
			{
				IEnumerator enumerator = Root.Controls.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Control control1 = WebUtils.FindControlRecursive((Control)enumerator.Current, Id);
						if (control1 != null)
						{
							root = control1;
							return root;
						}
					}
					root = null;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				root = Root;
			}
			return root;
		}

		public static string GetClientIP(Page p)
		{
			string empty = string.Empty;
			empty = p.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if ((empty == null ? true : empty.Equals("")))
			{
				empty = p.Request.ServerVariables["REMOTE_ADDR"];
			}
			return empty;
		}

		public static string GetClientIP()
		{
			string empty = string.Empty;
			return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
		}

		public static Control GetControl(Table table, string controlName)
		{
			Control control = null;
			IEnumerator enumerator = table.Rows.GetEnumerator();
			try
			{
				do
				{
					if (enumerator.MoveNext())
					{
						control = ((TableRow)enumerator.Current).FindControl(controlName);
					}
					else
					{
						break;
					}
				}
				while (control == null);
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return control;
		}

		public static string GetValueForControl(Table table, string ControlName)
		{
			string selectedValue = "";
			foreach (TableRow row in table.Rows)
			{
				Control control = row.FindControl(ControlName);
				if (control != null)
				{
					if (ControlName.StartsWith("cbo"))
					{
						selectedValue = (control as DropDownList).SelectedValue;
						break;
					}
					else
					{
						selectedValue = (control as TextBox).Text.Trim();
						break;
					}
				}
			}
			return selectedValue;
		}

		public static string ImageToBase64(System.Drawing.Image image, ImageFormat format)
		{
			string base64String;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, format);
				base64String = Convert.ToBase64String(memoryStream.ToArray());
			}
			return base64String;
		}

		public static string JavascriptAcentosUnicode(string mensaje)
		{
			string str = mensaje.Replace("á", "á").Replace("é", "é").Replace("í", "í").Replace("ó", "ó").Replace("ú", "ú").Replace("Á", "Á").Replace("É", "É").Replace("Í", "Í").Replace("Ó", "Ó").Replace("Ú", "Ú").Replace("ñ", "ñ").Replace("Ñ", "Ñ");
			return str;
		}

		public static void MessageBootstrap(Page pag, string message, Button btn)
		{
			if ((message == null ? false : message != string.Empty))
			{
				message = message.Replace("'", "\\'");
				message = message.Replace("\"", string.Concat("\\", '\"'));
				message = message.Replace("\n", "\\n");
				StringBuilder stringBuilder = new StringBuilder(512);
				if (btn == null)
				{
					stringBuilder.Append("if($('.iconconsultar').is(':visible')) $('.iconconsultar').hide();var m = $('#dvMensajes');m.addClass('alert-danger');m.show().find('.msg').html('").Append(message).Append("');m.find()");
				}
				else
				{
					stringBuilder.Append(string.Concat("document.getElementById('", btn.ClientID, "').disabled=false;alert('")).Append(message).Append("');");
				}
				ScriptManager.RegisterStartupScript(pag, pag.GetType(), "ShowMessage", stringBuilder.ToString(), true);
			}
		}

		public static void MessageBox(Page p, string message)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("alert('").Append(message).Append("');");
			if (!p.ClientScript.IsStartupScriptRegistered("ShowMessage"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "ShowMessage", stringBuilder.ToString(), true);
			}
		}

		public static void MessageBox2005(Page pag, string message, Button btn)
		{
			if ((message == null ? false : message != string.Empty))
			{
				message = message.Replace("'", "\\'");
				message = message.Replace("\"", string.Concat("\\", '\"'));
				message = message.Replace("\n", "\\n");
				StringBuilder stringBuilder = new StringBuilder(512);
				if (btn == null)
				{
					stringBuilder.Append("alert('").Append(message).Append("');");
				}
				else
				{
					stringBuilder.Append(string.Concat("document.getElementById('", btn.ClientID, "').disabled=false;alert('")).Append(message).Append("');");
				}
				ScriptManager.RegisterStartupScript(pag, pag.GetType(), "ShowMessage", stringBuilder.ToString(), true);
			}
		}

		public static void MessageBox2005(Page pag, string message)
		{
			WebUtils.MessageBox2005(pag, message, null);
		}

		public static void MessageBoxAndBack(Page p, string message)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append("back();");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("MessageBoxAndBack"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "MessageBoxAndBack", stringBuilder.ToString());
			}
		}

		public static void MessageBoxAndClosePopup(Page p, string message)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append("window.close();");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("MessageBoxAndClosePopup"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "MessageBoxAndClosePopup", stringBuilder.ToString());
			}
		}

		public static void MessageBoxAndRedirect(Page p, string message, string pageString)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append("self.parent.location='");
			stringBuilder.Append(pageString);
			stringBuilder.Append("';");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("MessageBoxAndRedirect"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "MessageBoxAndRedirect", stringBuilder.ToString());
			}
		}

		public static void MessageBoxAndRedirectToFrame(Page p, string message, string pageString)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append("self.parent.mainFrame.location='");
			stringBuilder.Append(pageString);
			stringBuilder.Append("';");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("MessageBoxAndRedirect"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "MessageBoxAndRedirect", stringBuilder.ToString());
			}
		}

		public static void MessageBoxAndRedirectToFrame2005(Page p, string message, string pageString)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append(string.Concat("var url = '", pageString, "';"));
			stringBuilder.Append("if(self.parent.mainFrame)self.parent.mainFrame.location=url;");
			stringBuilder.Append("else window.parent.mainFrame.location=url;");
			stringBuilder.Append("</script>");
			ScriptManager.RegisterStartupScript(p, p.GetType(), "MessageBoxAndRedirect", stringBuilder.ToString(), false);
		}

		public static void MessageBoxBye(Page p, string message)
		{
			message = message.Replace("'", "\\'");
			message = message.Replace("\"", string.Concat("\\", '\"'));
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("alert('").Append(message).Append("');");
			stringBuilder.Append("self.parent.location='../inicio.aspx';");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("MessageBoxBye"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "MessageBoxBye", stringBuilder.ToString());
			}
		}

		public static string readHtmlPageToString(string url)
		{
			string end;
			try
			{
				WebRequest webRequest = WebRequest.Create(url);
				using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
				{
					end = streamReader.ReadToEnd();
					streamReader.Close();
				}
			}
			catch (Exception exception)
			{
				end = exception.Message;
			}
			return end;
		}

		public static string RootUrl()
		{
			string str = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.IndexOf(HttpContext.Current.Request.Url.AbsolutePath) + 1);
			return str;
		}

		public static bool SelectValueInDropDownList(DropDownList ddl, string value)
		{
			bool flag = false;
			foreach (ListItem item in ddl.Items)
			{
				if (item.Value.IndexOf(value) != -1)
				{
					item.Selected = true;
					flag = true;
					break;
				}
			}
			return flag;
		}

		public static void SetFocus(Page p, TextBox ctrlIn)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language='JavaScript'>");
			stringBuilder.Append("document.getElementById('").Append(ctrlIn.ClientID).Append("').focus();");
			stringBuilder.Append("</script>");
			if (p.ClientScript.IsStartupScriptRegistered("SetFocus"))
			{
				p.ClientScript.RegisterStartupScript(typeof(string), "SetFocus", stringBuilder.ToString());
			}
		}

		public static void SetFocus(Control control)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\r\n<script language='JavaScript'>\r\n");
			stringBuilder.Append("<!--\r\n");
			stringBuilder.Append("function SetFocus()\r\n");
			stringBuilder.Append("{\r\n");
			stringBuilder.Append("\tdocument.");
			Control parent = control.Parent;
			while (!(parent is HtmlForm))
			{
				parent = parent.Parent;
			}
			stringBuilder.Append(parent.ClientID);
			stringBuilder.Append("['");
			stringBuilder.Append(control.UniqueID);
			stringBuilder.Append("'].focus();\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append("window.onload = SetFocus;\r\n");
			stringBuilder.Append("// -->\r\n");
			stringBuilder.Append("</script>");
			if (control.Page.ClientScript.IsClientScriptBlockRegistered("SetFocus"))
			{
				control.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "SetFocus", stringBuilder.ToString());
			}
		}

		public static void SetValueForControl(Table table, string ControlName, string value)
		{
			bool flag = false;
			IEnumerator enumerator = table.Rows.GetEnumerator();
			try
			{
				while (true)
				{
				Label0:
					if (enumerator.MoveNext())
					{
						Control control = ((TableRow)enumerator.Current).FindControl(ControlName);
						if (control != null)
						{
							if (ControlName.StartsWith("cbo"))
							{
								foreach (ListItem item in (control as DropDownList).Items)
								{
									if (item.Value.IndexOf(value) != -1)
									{
										item.Selected = true;
										flag = true;
										break;
									}
								}
							}
							else
							{
								(control as TextBox).Text = value;
								flag = true;
							}
							if (!flag)
							{
								goto Label0;
							}
							else
							{
								break;
							}
						}
					}
					else
					{
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}