using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace IBBAV.Helpers
{
	public class HelperPDF
	{
		public HelperPDF()
		{
		}

		public static void DownloadAsPDF(byte[] bytes, string nombrearchivo)
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ClearContent();
			HttpContext.Current.Response.ClearHeaders();
			HttpContext.Current.Response.ContentType = "application/pdf";
			HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Concat("attachment;filename=", nombrearchivo));
			HttpContext.Current.Response.OutputStream.Write(bytes, 0, (int)bytes.Length);
			HttpContext.Current.Response.OutputStream.Flush();
			HttpContext.Current.Response.OutputStream.Close();
			HttpContext.Current.Response.End();
		}

		public static byte[] getPdf(Stream st)
		{
			return HelperPDF.getPdf(HelperPDF.StreamToString(st), "");
		}

		public static byte[] getPdf(string html, string css)
		{
			byte[] array;
			Image instance = Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/logobav_edocta.jpg"));
			instance.SetAbsolutePosition(0f, 0f);
			instance.ScalePercent(72f);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Document document = new Document(PageSize.LETTER))
				{
					using (PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream))
					{
						document.Open();
						try
						{
							try
							{
								using (MemoryStream memoryStream1 = new MemoryStream(Encoding.UTF8.GetBytes(css)))
								{
									using (MemoryStream memoryStream2 = new MemoryStream(Encoding.UTF8.GetBytes(html)))
									{
										XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, document, memoryStream2, memoryStream1);
										PdfContentByte directContent = pdfWriter.DirectContent;
										PdfTemplate pdfTemplate = directContent.CreateTemplate(644f, 52f);
										pdfTemplate.AddImage(instance);
										directContent.AddTemplate(pdfTemplate, 0f, 747f);
									}
								}
							}
							catch (Exception exception)
							{
							}
						}
						finally
						{
							document.Close();
						}
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		public static string StreamToString(Stream stream)
		{
			string end;
			using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
			{
				end = streamReader.ReadToEnd();
			}
			return end;
		}
	}
}