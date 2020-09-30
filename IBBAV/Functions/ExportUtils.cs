using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace IBBAV.Functions
{
	public class ExportUtils
	{
		public ExportUtils()
		{
		}

		public static void Export2Excel(string nombrereporte, string encabezado, DataTable data, List<ExportUtils.ExportUtilDatos> campos)
		{
			HttpResponse response = HttpContext.Current.Response;
			string str = "<style>table {mso-displayed-decimal-separator:\"\\,\"; mso-displayed-thousand-separator:\"\\.\";}.text { mso-number-format:\\@; }</style>";
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
			string str1 = "<html><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">";
			string str2 = "</head>";
			StringBuilder stringBuilder = new StringBuilder(string.Concat("<body>", encabezado, "<table border=1>"));
			stringBuilder.Append("<tr>");
			foreach (ExportUtils.ExportUtilDatos campo in campos)
			{
				string[] headerColor = new string[] { "<th style='text-align:", null, null, null, null, null, null };
				headerColor[1] = campo.Align.ToString();
				headerColor[2] = "; background-color:";
				headerColor[3] = campo.HeaderColor;
				headerColor[4] = "; color:";
				headerColor[5] = campo.HeaderTextColor;
				headerColor[6] = "'>{0}</th>";
				string[] strArrays = headerColor;
				stringBuilder.AppendFormat(string.Concat(strArrays), campo.FieldHeader);
			}
			stringBuilder.Append("</tr>");
			bool flag = false;
			foreach (DataRow row in data.Rows)
			{
				stringBuilder.Append("<tr'>");
				foreach (ExportUtils.ExportUtilDatos exportUtilDato in campos)
				{
					if (flag)
					{
						stringBuilder.AppendFormat("<td style='background-color:#f9f9f9;'>{0}</td>", row[exportUtilDato.FieldData]);
					}
					else
					{
						stringBuilder.AppendFormat("<td>{0}</td>", row[exportUtilDato.FieldData]);
					}
				}
				flag = !flag;
				stringBuilder.Append("</tr>");
			}
			stringBuilder.Append("</table></body>");
			response.Write(string.Concat(new string[] { str1, str, str2, stringBuilder.ToString(), "</html>" }));
			response.Flush();
			response.End();
		}

		public enum Align
		{
			left,
			right,
			center
		}

		public class ExportUtilDatos
		{
			public ExportUtils.Align Align
			{
				get;
				set;
			}

			public string FieldData
			{
				get;
				set;
			}

			public string FieldHeader
			{
				get;
				set;
			}

			public string Format
			{
				get;
				set;
			}

			public string HeaderColor
			{
				get;
				set;
			}

			public string HeaderTextColor
			{
				get;
				set;
			}

			public bool Striped
			{
				get;
				set;
			}

			public ExportUtilDatos()
			{
				string empty = string.Empty;
				string str = empty;
				this.Format = empty;
				string str1 = str;
				string str2 = str1;
				this.FieldData = str1;
				this.FieldHeader = str2;
				this.Align = ExportUtils.Align.left;
				this.HeaderColor = "lightgray";
				this.HeaderTextColor = "#ffffff";
				this.Striped = false;
			}

			public ExportUtilDatos(string FieldHeader, string FieldData, ExportUtils.Align Align, string Format, string HeaderColor, string HeaderTextColor, bool Striped)
			{
				this.FieldHeader = FieldHeader;
				this.FieldData = FieldData;
				this.Format = Format;
				this.Align = Align;
				this.HeaderColor = HeaderColor;
				this.Striped = Striped;
				this.HeaderTextColor = HeaderTextColor;
			}
		}
	}
}