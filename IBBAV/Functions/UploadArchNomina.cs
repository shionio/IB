using System;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Functions
{
	public class UploadArchNomina
	{
		private string _uploadDirectory;

		private int _uploadMaxFileSize;

		private string _uploadedFileExt;

		private HtmlInputFile _uploadedInputFile;

		private string _uploadedMessage;

		private string _uploadedFileName;

		private bool _uploadedOk;

		public string uploadDirectory
		{
			get
			{
				return this._uploadDirectory;
			}
			set
			{
				this._uploadDirectory = value;
			}
		}

		public string uploadedFileExt
		{
			get
			{
				return this._uploadedFileExt;
			}
			set
			{
				this._uploadedFileExt = value;
			}
		}

		public string uploadedFileName
		{
			get
			{
				return this._uploadedFileName;
			}
			set
			{
				this._uploadedFileName = value;
			}
		}

		public HtmlInputFile uploadedInputFile
		{
			get
			{
				return this._uploadedInputFile;
			}
			set
			{
				this._uploadedInputFile = value;
			}
		}

		public string uploadedMessage
		{
			get
			{
				return this._uploadedMessage;
			}
			set
			{
				this._uploadedMessage = value;
			}
		}

		public bool uploadedOk
		{
			get
			{
				return this._uploadedOk;
			}
			set
			{
				this._uploadedOk = value;
			}
		}

		public int uploadMaxFileSize
		{
			get
			{
				return this._uploadMaxFileSize;
			}
			set
			{
				this._uploadMaxFileSize = value;
			}
		}

		public UploadArchNomina()
		{
			this._uploadedMessage = string.Empty;
			this._uploadDirectory = string.Empty;
			this._uploadedInputFile = null;
			this._uploadedFileName = string.Empty;
			this._uploadedOk = false;
		}

		public bool UploadArchivo()
		{
			bool flag;
			int contentLength = 0;
			if (this._uploadedInputFile.PostedFile != null)
			{
				this._uploadedFileName = Path.GetFileName(this._uploadedInputFile.PostedFile.FileName);
				contentLength = this._uploadedInputFile.PostedFile.ContentLength;
				if (contentLength == 0)
				{
					this._uploadedMessage = "Error. No existe el archivo a subir.";
					flag = false;
					return flag;
				}
				else if (Path.GetFileNameWithoutExtension(this._uploadedFileName).Length <= 35)
				{
					if (Path.GetExtension(this._uploadedInputFile.PostedFile.FileName).ToLower() != ".txt")
					{
						this._uploadedMessage = "Error. La extension del archivo debe ser .TXT";
						flag = false;
						return flag;
					}
					byte[] numArray = new byte[contentLength];
					this._uploadedInputFile.PostedFile.InputStream.Read(numArray, 0, contentLength);
					int num = 0;
					while (File.Exists(string.Concat(this._uploadDirectory, this._uploadedFileName)))
					{
						num++;
						this._uploadedFileName = string.Concat(Path.GetFileNameWithoutExtension(this._uploadedInputFile.PostedFile.FileName), num.ToString(), ".txt");
					}
					try
					{
						try
						{
							FileStream fileStream = new FileStream(string.Concat(this._uploadDirectory, this._uploadedFileName), FileMode.Create);
							fileStream.Write(numArray, 0, (int)numArray.Length);
							fileStream.Close();
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							this._uploadedMessage = string.Concat(new string[] { "Error salvando <b>", this._uploadDirectory, this._uploadedFileName, "</b><br>", exception.ToString() });
						}
					}
					finally
					{
						this._uploadedOk = true;
						this._uploadedMessage = "Archivo subido satisfactoriamente!";
						this._uploadedFileName = string.Concat(this._uploadDirectory, this._uploadedFileName);
					}
				}
				else
				{
					this._uploadedMessage = "Error. El nombre del archivo excede el máximo permitido de 35 caracteres.";
					flag = false;
					return flag;
				}
			}
			flag = true;
			return flag;
		}
	}
}