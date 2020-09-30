using IBBAV.Entidades.Notificaciones;
using IBBAV.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.UserControls.BAVCommons
{
    [ToolboxBitmap(typeof(DropDownList))]
    [ToolboxData("<{0}:DropDownListPaises runat=server></{0}:DropDownListPaises>")]
    public class DropDownListPaises : DropDownList
    {
        private bool hasTextoInicial;

        private string textoInicial;

        public DropDownListPaises()
		{
		}

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

        [Bindable(false)]
        [Localizable(false)]
        public List<Paises> ListaPaises
        {
            get
            {
                return this.ViewState["dataPaises"] as List<Paises>;
            }
            set
            {
                this.ViewState["dataPaises"] = value;
            }
        }

        [Bindable(false)]
        [Localizable(false)]
        public Paises getPaises(ListItem item)
        {
            Paises paises = this.ListaPaises.Find((Paises obj) => obj.sNombre.Equals(item.Value));
            return paises;
        }

        [Localizable(false)]
        public Paises getPaises()
        {
            return this.getPaises(this.SelectedItem);
        }		
		
		
		public void GetLista(){
			this.ListaPaises = new List<Paises>();
			List<Paises> listaPaises = HelperNotificacionIBP.GetListaPaises();
            List<Paises>.Enumerator enumerator1 = listaPaises.GetEnumerator();

            if (this.HasTextoInicial)
            {
                this.Items.Add(new ListItem(this.TextoInicial, "0"));
            }

            try
            {
                while (enumerator1.MoveNext())
                {
                    Paises pais = enumerator1.Current;
                    ListItem key = new ListItem()
                    {
                        Text = string.Concat(new string[] { pais.sNombre})
                    };
                    this.ListaPaises.Add(pais);
                    this.Items.Add(key);
                }                
            }
            finally
            {
                ((IDisposable)(object)enumerator1).Dispose();
            }
        }
    }
}