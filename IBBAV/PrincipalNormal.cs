using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.UI;

namespace IBBAV
{
	public class PrincipalNormal : System.Web.UI.Page
	{
		private System.Web.UI.ScriptManager manager;

		private bool _IsJavaScriptEnabled;

		public bool IsJavaScriptEnabled
		{
			get
			{
				return this._IsJavaScriptEnabled;
			}
			set
			{
				this._IsJavaScriptEnabled = value;
			}
		}

		public PrincipalNormal()
		{
		}

		protected void manager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			if (e.Exception.Message != null)
			{
				this.manager.AsyncPostBackErrorMessage = e.Exception.Message;
			}
			else
			{
				this.manager.AsyncPostBackErrorMessage = "An unspecified error occurred.";
			}
		}

		public static void MessageBoxAjax(string message)
		{
			throw new Exception(message);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			base.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			base.Response.Cache.SetNoStore();
			if (!this.Page.ClientScript.IsStartupScriptRegistered("BAV"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAV", string.Concat(this.Page.ResolveUrl("~"), "js/bav/BAV.js"));
			}
			if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVCaching"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVCaching", string.Concat(this.Page.ResolveUrl("~"), "js/bav/Caching.js"));
			}
			if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVDisable"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVDisable", string.Concat(this.Page.ResolveUrl("~"), "js/bav/disable.js"));
			}
			if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVTimesession"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVTimesession", string.Concat(this.Page.ResolveUrl("~"), "js/bav/timesession.js"));
			}
			if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVFuntions"))
			{
				this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVFuntions", string.Concat(this.Page.ResolveUrl("~"), "js/bav/functions.js"));
			}
			base.OnLoad(e);
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			if (this.Page.Master != null)
			{
				this.manager = this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("ScriptManager1") as System.Web.UI.ScriptManager;
			}
			else
			{
				this.manager = this.Page.FindControl("ScriptManager1") as System.Web.UI.ScriptManager;
			}
			if (this.manager != null)
			{
				this.manager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(this.manager_AsyncPostBackError);
				this.manager.Scripts.Add(new ScriptReference("~/js/bav/BAV.js"));
				this.manager.Scripts.Add(new ScriptReference("~/js/bav/Caching.js"));
				this.manager.Scripts.Add(new ScriptReference("~/js/bav/disable.js"));
				this.manager.Scripts.Add(new ScriptReference("~/js/bav/timesession.js"));
				this.manager.Scripts.Add(new ScriptReference("~/js/bav/functions.js"));
			}
			else
			{
				if (!this.Page.ClientScript.IsStartupScriptRegistered("BAV"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAV", string.Concat(this.Page.ResolveUrl("~"), "js/bav/BAV.js"));
				}
				if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVCaching"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVCaching", string.Concat(this.Page.ResolveUrl("~"), "js/bav/Caching.js"));
				}
				if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVDisable"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVDisable", string.Concat(this.Page.ResolveUrl("~"), "js/bav/disable.js"));
				}
				if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVTimesession"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVTimesession", string.Concat(this.Page.ResolveUrl("~"), "js/bav/timesession.js"));
				}
				if (!this.Page.ClientScript.IsStartupScriptRegistered("BAVFuntions"))
				{
					this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "BAVFuntions", string.Concat(this.Page.ResolveUrl("~"), "js/bav/functions.js"));
				}
			}
			base.OnLoadComplete(e);
		}
	}
}