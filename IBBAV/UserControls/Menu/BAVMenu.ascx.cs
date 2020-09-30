using IBBAV.Entidades;
using IBBAV.Helpers;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.UserControls.Menu
{
    public partial class BAVMenu : UserControl
    {
        private List<BAVMenuOpcion> lista;

        public BAVMenu()
        {
        }

        public void LlenarMenu()
        {
            this.lista = HelperMenu.getMenuTest();
            this.rptMenuLvl1.DataSource = this.lista;
            this.rptMenuLvl1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected void rptMenuLvl1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                BAVMenuOpcion dataItem = e.Item.DataItem as BAVMenuOpcion;
                object obj = e.Item.DataItem;
                if (dataItem.Opciones.Count <= 0)
                {
                    string str = "?";
                    if (dataItem.Pagina.Contains("?"))
                    {
                        str = "&";
                    }
                    Literal literal = e.Item.FindControl("liItem") as Literal;
                    if (string.IsNullOrEmpty(dataItem.Pagina))
                    {
                        literal.Text = string.Concat("<li><a href='javascript:void(0);'>", dataItem.Nombre, "</a></li>");
                    }
                    else
                    {
                        literal.Text = string.Concat(new object[] { "<li><a href='", dataItem.Pagina, str, "sCod=", dataItem.Codigo, "' onclick='goPage(this, event);'>", dataItem.Nombre, "</a></li>" });
                    }
                }
                else
                {
                    Control control = e.Item.FindControl("liChilds");
                    Repeater opciones = e.Item.FindControl("rptMenuLvl2") as Repeater;
                    opciones.DataSource = dataItem.Opciones;
                    opciones.DataBind();
                    control.Visible = true;
                }
            }
        }

        protected void rptMenuLvl2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                BAVMenuOpcion dataItem = e.Item.DataItem as BAVMenuOpcion;
                string str = "?";
                if (dataItem.Pagina.Contains("?"))
                {
                    str = "&";
                }
                Literal literal = e.Item.FindControl("liItem") as Literal;
                if (string.IsNullOrEmpty(dataItem.Pagina))
                {
                    literal.Text = string.Concat("<li><a href='javascript:void(0);'>", dataItem.Nombre, "</a></li>");
                }
                else
                {
                    literal.Text = string.Concat(new object[] { "<li><a href='", dataItem.Pagina, str, "sCod=", dataItem.Codigo, "' onclick='goPage(this, event);'>", dataItem.Nombre, "</a></li>" });
                }
            }
        }
    }
}