using IBBAV;
using IBBAV.Entidades;
using IBBAV.Functions;
using IBBAV.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IBBAV.UserControls
{
    public partial class PreguntasDesafio : UserControl
    {
        private int nro;
       
        public bool MaskedInputs
        {
            get;
            set;
        }

        public PreguntasDesafio.EnumTipoPreguntas TipoPreguntas
        {
            get;
            set;
        }

        public PreguntasDesafio.EnumTipoRepeater TipoRepeater
        {
            get;
            set;
        }

        public PreguntasDesafio()
        {
        }

        public List<KeyValuePair<short, string>> getPreguntasRespuestas()
        {
            List<KeyValuePair<short, string>> keyValuePairs = new List<KeyValuePair<short, string>>();
            foreach (RepeaterItem item in ((this.TipoRepeater != PreguntasDesafio.EnumTipoRepeater.Div ? this.rptPreguntas2 : this.rptPreguntas)).Items)
            {
                if ((item.ItemType == ListItemType.Item ? true : item.ItemType == ListItemType.AlternatingItem))
                {
                    TextBox textBox = item.FindControl("txtRespuesta") as TextBox;
                    if (textBox.Text.Trim() == "")
                    {
                        throw new Exception("Faltan preguntas por responder");
                    }
                    if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.Preguntas)
                    {
                        DropDownList dropDownList = item.FindControl("ddlPreguntas") as DropDownList;
                        keyValuePairs.Add(new KeyValuePair<short, string>(Convert.ToInt16(dropDownList.SelectedItem.Value), CryptoUtils.EncryptMD5(textBox.Text)));
                    }
                    else if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.PreguntasAfiliado)
                    {
                        Literal literal = item.FindControl("liId") as Literal;
                        keyValuePairs.Add(new KeyValuePair<short, string>(Convert.ToInt16(literal.Text), CryptoUtils.EncryptMD5(textBox.Text)));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return keyValuePairs;
        }

        public void llenarPreguntas()
        {
            int[] numArray = new int[] { 1, 2, 3, 4 };
            if (this.TipoRepeater != PreguntasDesafio.EnumTipoRepeater.Div)
            {
                this.rptPreguntas2.DataSource = numArray;
                this.rptPreguntas2.DataBind();
            }
            else
            {
                this.rptPreguntas.DataSource = numArray;
                this.rptPreguntas.DataBind();
            }
        }

        public void llenarPreguntasAfiliado(long af_id)
        {
            List<KeyValuePair<short, string>> keyValuePairs = HelperAfiliado.PreguntasAfiliadoGet(af_id);
            if (this.TipoRepeater != PreguntasDesafio.EnumTipoRepeater.Div)
            {
                this.rptPreguntas2.DataSource = keyValuePairs;
                this.rptPreguntas2.DataBind();
            }
            else
            {
                this.rptPreguntas.DataSource = keyValuePairs;
                this.rptPreguntas.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.Preguntas)
                {
                    this.llenarPreguntas();
                }
                else if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.PreguntasAfiliado)
                {
                    Principal page = this.Page as Principal;
                    if (page != null)
                    {
                        this.llenarPreguntasAfiliado(page.Afiliado.nAF_Id);
                    }
                }
            }
        }

        protected void rptPreguntas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item ? true : e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal str = e.Item.FindControl("liNro") as Literal;
                if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.Preguntas)
                {
                    short num = Convert.ToInt16(e.Item.DataItem);
                    DropDownList dropDownList = e.Item.FindControl("ddlPreguntas") as DropDownList;
                    dropDownList.Visible = true;
                    foreach (KeyValuePair<short, string> keyValuePair in HelperAfiliado.PreguntasGet(num))
                    {
                        ListItemCollection items = dropDownList.Items;
                        string value = keyValuePair.Value;
                        short key = keyValuePair.Key;
                        items.Add(new ListItem(value, key.ToString()));
                    }
                }
                else if (this.TipoPreguntas == PreguntasDesafio.EnumTipoPreguntas.PreguntasAfiliado)
                {
                    KeyValuePair<short, string> dataItem = (KeyValuePair<short, string>)e.Item.DataItem;
                    Literal literal = e.Item.FindControl("liId") as Literal;
                    literal.Text = dataItem.Key.ToString();
                    Literal value1 = e.Item.FindControl("liPregunta") as Literal;
                    value1.Text = dataItem.Value;
                    TextBox textBox = e.Item.FindControl("txtRespuesta") as TextBox;
                    textBox.TextMode = TextBoxMode.SingleLine;
                    if (this.MaskedInputs)
                    {
                        textBox.TextMode = TextBoxMode.Password;
                    }
                }
                this.nro++;
                str.Text = this.nro.ToString();
            }
        }

        public bool ValidarPreguntasRespuestas()
        {
            bool flag = false;
            Principal page = this.Page as Principal;
            List<KeyValuePair<short, string>> preguntasRespuestas = this.getPreguntasRespuestas();
            long nAFId = page.Afiliado.nAF_Id;
            short key = preguntasRespuestas[0].Key;
            short num = preguntasRespuestas[1].Key;
            string value = preguntasRespuestas[0].Value;
            KeyValuePair<short, string> item = preguntasRespuestas[1];
            flag = HelperAfiliado.PreguntasAfiliadoValidate(nAFId, key, num, value, item.Value);
            return flag;
        }

        public bool ValidarPreguntasRespuestas(long afiliado)
        {
            bool flag = false;
            List<KeyValuePair<short, string>> preguntasRespuestas = this.getPreguntasRespuestas();
            short key = preguntasRespuestas[0].Key;
            short num = preguntasRespuestas[1].Key;
            string value = preguntasRespuestas[0].Value;
            KeyValuePair<short, string> item = preguntasRespuestas[1];
            flag = HelperAfiliado.PreguntasAfiliadoValidate(afiliado, key, num, value, item.Value);
            return flag;
        }

        public enum EnumTipoPreguntas
        {
            Preguntas,
            PreguntasAfiliado
        }

        public enum EnumTipoRepeater
        {
            Div,
            Table
        }
    }
}