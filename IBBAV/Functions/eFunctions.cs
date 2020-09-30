using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.Functions
{
	public class eFunctions
	{
		public eFunctions()
		{
		}

		public static DataTable ArrayToDataTable(string[] arr, char[] delimiter, bool headers)
		{
			DataTable dataTable = new DataTable();
			string[] strArrays = arr[0].Split(delimiter);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				if (headers)
				{
					dataTable.Columns.Add(str);
				}
				else
				{
					dataTable.Columns.Add();
				}
			}
			for (int j = 0; j < (int)arr.Length; j++)
			{
				if ((!headers ? true : j != 0))
				{
					string[] strArrays1 = arr[j].Split(delimiter);
					DataRow dataRow = dataTable.NewRow();
					for (int k = 0; k < (int)strArrays1.Length; k++)
					{
						dataRow[k] = strArrays1[k];
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}

		public static string[] GetFileInfo(string filename)
		{
			ArrayList arrayLists = new ArrayList();
			FileInfo fileInfo = new FileInfo(filename);
			arrayLists.Add("Property,Value");
			arrayLists.Add(string.Concat("Name,", fileInfo.Name));
			arrayLists.Add(string.Concat("FullName,", fileInfo.FullName));
			arrayLists.Add(string.Concat("DirectoryName,", fileInfo.DirectoryName));
			arrayLists.Add(string.Concat("Extension,", fileInfo.Extension));
			DateTime creationTime = fileInfo.CreationTime;
			arrayLists.Add(string.Concat("CreationTime,", creationTime.ToString()));
			DateTime lastAccessTime = fileInfo.LastAccessTime;
			arrayLists.Add(string.Concat("LastAccessTime,", lastAccessTime.ToString()));
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			arrayLists.Add(string.Concat("LastWriteTime,", lastWriteTime.ToString()));
			bool exists = fileInfo.Exists;
			arrayLists.Add(string.Concat("Exists,", exists.ToString()));
			long length = fileInfo.Length;
			arrayLists.Add(string.Concat("Length,", length.ToString()));
			FileAttributes attributes = fileInfo.Attributes;
			arrayLists.Add(string.Concat("Attributes,", attributes.ToString()));
			string[] strArrays = new string[arrayLists.Count];
			arrayLists.CopyTo(strArrays);
			return strArrays;
		}

		public static int GetRandomNumber(bool noZeros)
		{
			byte[] numArray = new byte[1];
			RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
			if (noZeros)
			{
				rNGCryptoServiceProvider.GetNonZeroBytes(numArray);
			}
			else
			{
				rNGCryptoServiceProvider.GetBytes(numArray);
			}
			return numArray[0];
		}

		public static int GetRandomNumber(int high)
		{
			byte[] numArray = new byte[4];
			(new RNGCryptoServiceProvider()).GetBytes(numArray);
			int num = BitConverter.ToInt32(numArray, 0);
			return Math.Abs(num % high);
		}

		public static int GetRandomNumber(int low, int high)
		{
			return (new Random()).Next(low, high);
		}

		public double GetRandomNumber()
		{
			return (new Random()).NextDouble();
		}

		public static bool IsDate(object dt)
		{
			bool flag;
			bool flag1;
			try
			{
				DateTime.Parse(dt.ToString());
				flag1 = true;
				return flag1;
			}
			catch
			{
				flag = false;
			}
			flag1 = flag;
			return flag1;
		}

		public static bool IsNumeric(string s)
		{
			bool flag;
			bool flag1;
			try
			{
				int.Parse(s);
				flag1 = true;
				return flag1;
			}
			catch
			{
				flag = false;
			}
			flag1 = flag;
			return flag1;
		}

		public static bool IsNumeric(object Expression)
		{
			double num;
			bool flag = double.TryParse(Convert.ToString(Expression), NumberStyles.Any, (IFormatProvider)NumberFormatInfo.InvariantInfo, out num);
			return flag;
		}

		public static bool IsNumeric_TryCatch(object num)
		{
			bool flag;
			bool flag1;
			try
			{
				Convert.ToDouble(num);
				flag1 = true;
				return flag1;
			}
			catch
			{
				flag = false;
			}
			flag1 = flag;
			return flag1;
		}

		public static string MessageBox(string message)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat("var messageVar = \"", message, "\";"));
			stringBuilder.Append("var err = Error.create(messageVar);");
			stringBuilder.Append("throw err;");
			return stringBuilder.ToString();
		}

		public static string MsgBox(string Msj)
		{
			return string.Concat("<SCRIPT type='text/javascript' language='javascript'>alert('", Msj, "');</SCRIPT>");
		}

		public static string MsgBoxAndBack(string strMsg)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("<script language=\"JavaScript\">\n");
			stringBuilder.Append("alert('").Append(strMsg).Append("');\n");
			stringBuilder.Append("back();\n");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		public static string removeSpecialCharacters(string orig)
		{
			string str = orig.Replace("\\", " ");
			str = str.Replace("(", " ");
			str = str.Replace(")", " ");
			str = str.Replace("/", " ");
			str = str.Replace("-", " ");
			str = str.Replace(",", " ");
			str = str.Replace(">", " ");
			str = str.Replace("<", " ");
			str = str.Replace("-", " ");
			str = str.Replace("&", " ");
			str = str.Replace("Ñ", " ");
			str = str.Replace("ñ", " ");
			str = str.Replace("'", "");
			str = str.Replace("’", "");
			str = str.Replace("   ", " ");
			str = str.Replace("  ", " ");
			return str.Trim(new char[] { ' ' });
		}

		public static string SendMail(string to)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = "MailService@thisdomain.com",
				To = to,
				Subject = string.Concat("Message to ", to),
				Body = "Hello!"
			});
			return "Mail Sent";
		}

		public static string SendMail(string to, string body)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = "MailService@thisdomain.com",
				To = to,
				Subject = body.Substring(0, (body.Length < 15 ? body.Length : 15)),
				Body = body
			});
			return "Mail Sent";
		}

		public static string SendMail(string to, string subject, string body)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = "MailService@thisdomain.com",
				To = to,
				Subject = subject,
				Body = body
			});
			return "Mail Sent";
		}

		public static string SendMail(string from, string to, string subject, string body)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = from,
				To = to,
				Subject = subject,
				Body = body
			});
			return "Mail Sent";
		}

		public static string SendMail(string from, string to, string cc, string subject, string body)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = from,
				To = to,
				Cc = cc,
				Subject = subject,
				Body = body
			});
			return "Mail Sent";
		}

		public static string SendMail(string from, string to, string cc, string bcc, string subject, string body)
		{
			SmtpMail.Send(new MailMessage()
			{
				From = from,
				To = to,
				Cc = cc,
				Bcc = bcc,
				Subject = subject,
				Body = body
			});
			return "Mail Sent";
		}

		public static string SendMail(string from, string to, string cc, string bcc, string subject, string body, string[] attachments, bool deleteAttachments)
		{
			string str = "";
			MailMessage mailMessage = new MailMessage()
			{
				From = from,
				To = to,
				Cc = cc,
				Bcc = bcc,
				Subject = subject
			};
			for (int i = 0; i < (int)attachments.Length; i++)
			{
				try
				{
					if (File.Exists(attachments[i]))
					{
						mailMessage.Attachments.Add(new MailAttachment(attachments[i]));
					}
					else
					{
						str = string.Concat(str, "ERROR: The file ", attachments[i], " does not exist! ");
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					str = string.Concat(str, "ERROR: ", exception.Message, " ");
				}
			}
			mailMessage.Body = string.Concat(str, body);
			try
			{
				SmtpMail.Send(mailMessage);
				str = string.Concat(str, " Mail Sent ");
			}
			catch (Exception exception2)
			{
				str = string.Concat(str, "ERROR: ", exception2.Message);
			}
			if (deleteAttachments)
			{
				for (int j = 0; j < (int)attachments.Length; j++)
				{
					try
					{
						if (File.Exists(attachments[j]))
						{
							File.Delete(attachments[j]);
							str = string.Concat(str, " Attachments Deleted ");
						}
						else
						{
							str = string.Concat(str, "ERROR: The file ", attachments[j], " does not exist, so it cannot be deleted. ");
						}
					}
					catch (Exception exception3)
					{
						str = string.Concat(str, "ERROR: ", exception3.Message);
					}
				}
			}
			return str;
		}

		public static void SetComboSelectedIndex(DropDownList pCbo, int pValue)
		{
			int num = 0;
			int i = pCbo.Items.Count - 1;
			while (num <= i)
			{
				if (int.Parse(pCbo.Items[num].Value) == pValue)
				{
					pCbo.SelectedIndex = num;
					break;
				}
				else if (int.Parse(pCbo.Items[i].Value) != pValue)
				{
					num++;
					i--;
				}
				else
				{
					pCbo.SelectedIndex = i;
					break;
				}
			}
		}

		public static void SetComboSelectedIndex(DropDownList pCbo, string pValue)
		{
			int num = 0;
			int i = pCbo.Items.Count - 1;
			while (num <= i)
			{
				if (pCbo.Items[num].Value == pValue)
				{
					pCbo.SelectedIndex = num;
					break;
				}
				else if (pCbo.Items[i].Value != pValue)
				{
					num++;
					i--;
				}
				else
				{
					pCbo.SelectedIndex = i;
					break;
				}
			}
		}

		public static string SetFocus(TextBox ctrl)
		{
			return string.Concat("<script language='javascript'>document.getElementById('", ctrl.ClientID, "').focus();</script>");
		}

		public static string ShowMessage(string Msj, string Tipo)
		{
			return "";
		}

        // Modificado 30/07/2018 por Liliana Guerra
        // public static bool validarMonto(ref string monto, ref string msg, double min, double Max)
        public static bool validarMonto(ref string monto, ref string msg, double min, double MtoLimiteTrans)
		{
			bool flag;
			msg = "";
			double num = Convert.ToDouble(monto);
			if (num < min)
			{
				msg = string.Concat("El monto es menor al mínimo permitido de Bs. ", Formatos.formatoMonto(min));
				flag = false;
			}
			else if (num > MtoLimiteTrans)
			{
                //msg = string.Concat("El monto es mayor al máximo permitido de Bs. ", Formatos.formatoMonto(Max));
                msg = string.Concat("El monto es mayor al máximo permitido de Bs. ", Formatos.formatoMonto(MtoLimiteTrans));
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

        // Modificado 30/07/2018 por Liliana Guerra
        //public static bool validarMonto(ref string monto, ref string msg, decimal min, decimal Max)
        public static bool validarMonto(ref string monto, ref string msg, decimal min, decimal MtoLimiteTrans)
		{
            //bool flag = eFunctions.validarMonto(ref monto, ref msg, Convert.ToDouble(min), Convert.ToDouble(Max));
            bool flag = eFunctions.validarMonto(ref monto, ref msg, Convert.ToDouble(min), Convert.ToDouble(MtoLimiteTrans));
			return flag;
		}
	}
}