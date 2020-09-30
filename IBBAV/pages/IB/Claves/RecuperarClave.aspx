<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="IBBAV.pages.IB.Claves.RecuperarClave" MasterPageFile="~/pages/BAVMasterSimple.Master" %>

<%@ Register Src="~/UserControls/PreguntasDesafio.ascx" TagPrefix="uc1" TagName="PreguntasDesafio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .info {
            cursor: help;
            margin-left: 4px;
        }
    </style>
    <link href="<%=ResolveUrl("~/css/jquery-bav-keyboard.css") + "?t=" + DateTime.Now.Ticks %>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-bav-keyboard.js") + "?t=" + DateTime.Now.Ticks %>"></script>
    <script>
        function OnlyNumeric(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
               ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            $('.jkeyboard').jkeyboard({
                target: '#keyboard',
                inputId: ['<%= txtClave.ClientID%>', '<%= txtClaveConfirmar.ClientID%>'],
                radioId: ['rdoClave', 'rdoClaveConfirm']
            });

            $('#<%= txtClave.ClientID %>').on("focus", function () {
                $("#rdoClave").prop("checked", true);
                $("#rdoClaveConfirm").prop("checked", false);
            });

            $('#<%= txtClaveConfirmar.ClientID %>').on("focus", function () {
                $("#rdoClave").prop("checked", false);
                $("#rdoClaveConfirm").prop("checked", true);
            });

            $('.info').tooltipsy({
                offset: [10, 0],
                css: {
                    'padding': '10px',
                    'max-width': '500px',
                    'color': '#303030',
                    'background-color': '#f5f5b5',
                    'border': '1px solid #deca7e',
                    '-moz-box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
                    '-webkit-box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
                    'box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
                    'text-shadow': 'none',
                    'font-size': '14px',
                    'border-radius': '5px'
                }
            });

            $(document).on('focus', ':input', function () {
                $(this).attr('autocomplete', 'off');
            });

        });
        
        function valdata() {

            //var splchar = new RegExp(/[$%&#@*,.]{1,}/),
            var splchar = new RegExp(/[$@!%*#?&\.]{1,}/),
            //Modificado 30/08/2018 por Liliana Guerra 
	        //char = new RegExp(/[A-Z]/g),
            char = new RegExp(/[A-z]/g),
            num = new RegExp(/[0-9]/g),

            e8 = $("#8char"),
            e1 = $("#1splchar"),
            n1 = $("#num"),
            mc = $("#pwmatch"),

            clv = $("#<%= txtClave.ClientID%>"),
            clvcon = $("#<%= txtClaveConfirmar.ClientID%>");

                if (clv.val() == "") {
                    e8.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                    .css("color", "#FF0004");
                    e1.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                    .css("color", "#FF0004");
                    n1.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                    .css("color", "#FF0004");
                } else {

                    var c = clv.val();
                    var ev = c.match(char);

                    if (ev)
                        if (ev.length > 3) {
                            e8.removeClass("glyphicon-remove")
                            .addClass("glyphicon-ok")
                            .css("color", "#00A41E");
                        } else {
                            e8.removeClass("glyphicon-ok")
                            .addClass("glyphicon-remove")
                            .css("color", "#FF0004");
                        }


                    if (splchar.test(c)) {
                        e1.removeClass("glyphicon-remove")
                        .addClass("glyphicon-ok")
                        .css("color", "#00A41E");
                    } else {
                        e1.removeClass("glyphicon-ok")
                        .addClass("glyphicon-remove")
                        .css("color", "#FF0004");
                    }

                    ev = c.match(num);
                    if (ev)
                        if (ev.length > 3) {
                            n1.removeClass("glyphicon-remove")
                            .addClass("glyphicon-ok")
                                .css("color", "#00A41E");
                        } else {
                            n1.removeClass("glyphicon-ok")
                            .addClass("glyphicon-remove")
                            .css("color", "#FF0004");
                        }                    
                }

                //Modificado 30/08/2018 por Liliana Guerra
                //if (clv.val() == clvcon.val()) {
                //if (clv.val.toUpperCase() == clvcon.val().toUpperCase()) 
            
                var clv = clv.val().toUpperCase();
                var clvcon = clvcon.val().toUpperCase();
                
                if ((clv === clvcon) && (clv !="")) {
                    mc.removeClass("glyphicon-remove")
                    .addClass("glyphicon-ok")
                        .css("color", "#00A41E");
                } else {
                    mc.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                        .css("color", "#FF0004");
                }
        }      

        
        $("form").addClass("form-signin");

        function validatefields() {
            var tdd = $('#<%= tddnumtxt.ClientID %>').val();
                var id = $('#<%= ddlTipoCedula.ClientID %>').val();
                var ced = $('#<%= txtCedula.ClientID %>').val();
                var cta = $('#<%= txtCta.ClientID %>').val();

                if (tdd == "") {
                    alert("Debe indicar su número de tarjeta de débito");
                    $('#<%= tddnumtxt.ClientID %>').focus();
                    return false;
                }
                else if (tdd.length < 19) {
                    alert("La longitud del número de la tarjeta de débito es incorrecto");
                    $('#<%= tddnumtxt.ClientID %>').focus();
                    return false;
                }
                else if (id == "") {
                    alert("Debe indicar su tipo identificación");
                    $('#<%= ddlTipoCedula.ClientID %>').focus();
                     return false;
                 }
                 else if (ced == "") {
                     alert("Debe indicar su número de identificación");
                     $('#<%= txtCedula.ClientID %>').focus();
                return false;
            }
            else if (cta == "" || cta.length < 4) {
                alert("Debe indicar lo últimos 4 dígitos de su cuenta");
                $('#<%= txtCta.ClientID %>').focus();
                return false;
            }
}


        function validateKeys() {

            //Modificado 30/08/2018 por Liliana Guerra 
            //var clave = $('#<%= txtClave.ClientID %>').val();
            //var claveconf = $('#<%= txtClaveConfirmar.ClientID %>').val();
            var clave = $('#<%= txtClave.ClientID %>').val().toUpperCase();
            var claveconf = $('#<%= txtClaveConfirmar.ClientID %>').val().toUpperCase();

            if (clave == "") {
                alert("Debe indicar su clave de acceso");
                $('#<%= txtClave.ClientID %>').focus();
                    return false;
                }

                else if (clave.length < 8) {
                    alert("Su clave de acceso debe ser mayor a 8 caracteres");
                    $('#<%= txtClave.ClientID %>').focus();
                return false;
            }

            else if (clave.length > 10) {
                alert("Su clave de acceso no debe ser mayor a 10 caracteres");
                $('#<%= txtClave.ClientID %>').focus();
                return false;
            }

            else if (claveconf == "") {
                alert("Confirme la clave de acceso");
                $('#<%= txtClaveConfirmar.ClientID %>').focus();
                return false;
            }
            else if (clave != claveconf) {
                alert("La clave y la confirmación son distintas");
                $('#<%= txtClaveConfirmar.ClientID %>').focus();
                return false;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="afi" style="margin: 0 auto; min-width: 450px; width: 800px; ">
        <asp:Panel ID="panelDatos" runat="server" Width="95%">

            <fieldset style="margin:0 auto;min-width: 400px; width: 600px;">
                <legend>Recuperar Clave</legend>

                <div style="color: Red;">
                    <asp:Literal runat="server" ID="trAviso" Visible="false">
                         Usted ha excedido el n&uacute;mero m&aacute;ximo de intentos<br /> para ingresar al sistema.
                    </asp:Literal>
                </div>

                <div style="padding-bottom: 30px;"><b>Por tu seguridad completa la siguiente informaci&oacute;n para restaurar tu acceso.</b></div>
                <div class="form-group">
                    <label for="tddnum">N&deg; de Tarjeta de D&eacute;bito</label><br />
                    <asp:TextBox runat="server" CssClass="form-control" ID="tddnumtxt" title="Tarjeta de d&uacute;bito" placeholder="N&uacute;mero de TDD" Style="min-width: 250px; width: 350px;" MaxLength="19"></asp:TextBox>
                    <span class="info" title="Su n&uacute;mero de tarjeta de d&eacute;bito (19 d&iacute;gitos)">(?)</span>
                </div>
                <label for="Id">Identificaci&oacute;n</label>
                <div class="form-group">
                    <div class="controls form-inline">
                        <asp:DropDownList runat="server" ID="ddlTipoCedula" class="form-control" Style="min-width: 50px; width: 150px;">
                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                            <asp:ListItem Value="V">V</asp:ListItem>
                            <asp:ListItem Value="E">E</asp:ListItem>
                            <asp:ListItem Value="P">P</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Ejemplo:16874125 " ID="txtCedula" MaxLength="9" Style="min-width: 100px; width: 200px;"></asp:TextBox>
                        <span class="info" title="Su n&uacute;mero de identificaci&oacute;n">(?)</span>
                    </div>
                </div>
                <div class="form-group">
                    <label for="tddnum">&Uacute;ltimos 4 d&iacute;gitos de su n&uacute;mero de cuenta</label>
                    <br />
                    <asp:TextBox runat="server" type="password" CssClass="form-control" ID="txtCta" placeholder="4 &uacute;ltimos n&uacute;mero de su cuenta" Style="width: 80px;" MaxLength="4"></asp:TextBox>
                    <span class="info" title="Indique los &uacute;ltimos 4 d&iacute;gitos de su n&uacute;mero de cuenta">(?)</span>
                </div>

                <div style="margin: 0 auto; text-align: center">
                    <asp:Button runat="server" ID="btnceptar" CssClass="btn btn-danger" Text="Aceptar" OnClick="btnAceptar_Click" OnClientClick="if(validatefields()==false) return (false);" CausesValidation="true" />
                    <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancelar" OnClick="btncancel_Click" />
                </div>

            </fieldset>
            <hr style="color: red;" />
        </asp:Panel>
    
        <asp:Panel ID="panelValidacion" runat="server"  Width="95%" Visible="false">
            <fieldset style="margin:0 auto;min-width: 400px; ">
                <legend>Preguntas de Seguridad</legend>
                <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="PreguntasAfiliado" TipoRepeater="Div" MaskedInputs="true" />
            </fieldset>
            <center>
                <div style="margin:0 auto; padding-top:20px;">
                    <asp:Button id="btnAceptarPreguntas" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnAceptarPreguntas_Click" />
                    <asp:Button id="btnCancelarPreguntas" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarPreguntas_Click" />
                </div>
            </center>
        </asp:Panel>
        <asp:Panel ID="panelClave" runat="server" Width="95%" Visible="false">
            <asp:Literal ID="liScript" runat="server"></asp:Literal>
            <fieldset style="min-width: 400px;">
                <legend>Por favor indique su nueva clave</legend>
                <div class="form-group">
                  <div runat="server" id="divmsg" style="display: block; text-align: left; color: red;" visible="false">
                        <div class="alert alert-danger" role="alert">
                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                            <span class="sr-only">Error:</span><asp:Literal ID="sError" runat="server" Visible="False"></asp:Literal>
                        </div>
                    </div>

                    <label for="txtClave" class="col-sm-4 control-label">Clave de Acceso:</label>
                    <div class="input-group col-sm-7">
                        <span class="input-group-addon">
                            <input name="clave" id="rdoClave" type="radio" data-input="<%= txtClave.ClientID%>" checked tabindex="-1" />
                        </span>
                        <asp:TextBox runat="server" CssClass="form-control jkeyboard" ID="txtClave" title="clave" TextMode="Password" placeholder="No distingue may&uacute;scula y min&uacute;scula" Style="min-width: 250px; width: 350px;" MaxLength="9"></asp:TextBox>
                        <span class="info" title="Clave de acceso al sistema de BAV en línea">(?)</span>
                    </div>
                    <!--KJFG-->
                    <div style="margin: 0 auto; text-align: center; padding-top: 15px; width: 380px; padding-left: 30px;">
                        <span id="8char" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>4 Letras
                                <span id="num" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>4 Numeros
                                <span id="1splchar" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>1 Caracter Especial
                    </div>

                </div>
                <div class="form-group">
                    <label for="txtClaveConfirmar" class="col-sm-4 control-label">Confirmar Clave de Acceso:</label>
                    <div class="input-group col-sm-7">
                        <span class="input-group-addon">
                            <input name="clave" id="rdoClaveConfirm" data-input="<%= txtClaveConfirmar.ClientID%>" type="radio" tabindex="-1" />
                        </span>
                        <asp:TextBox runat="server" CssClass="form-control jkeyboard" ID="txtClaveConfirmar" title="clave" TextMode="Password" placeholder="Confirmar Clave" Style="min-width: 250px; width: 350px;" MaxLength="9"></asp:TextBox>
                        <span class="info" title="Confirma la Clave de acceso al sistema de BAV en línea">(?)</span>
                    </div>

                    <!--KJFG-->
                    <div style="margin: 0 auto; padding-top: 15px; width: 380px; padding-left: 40px;">
                        <span id="pwmatch" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>Las claves coinciden
                    </div>

                </div>
                <div class="form-group">
                    <label for="txtClave" class="col-sm-4 control-label"></label>
                    <div class="col-sm-7">
                        <div id="keyboard" style="position: relative; display: block; float: left; padding: 5px 0px 40px 0px; width: 0;"></div>
                    </div>
                </div>
                <center>
                     <asp:Button ID="btnAceptar3" runat="server" CssClass="btn btn-danger" Text="Aceptar" OnClick="btnAceptar3_Click" OnClientClick="if(validateKeys()==false) return (false);" CausesValidation="true" />
                    <asp:Button id="btnCancelar3" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar3_Click" />
                 </center>
            </fieldset>

        </asp:Panel>
        <asp:Panel ID="panelExito" runat="server" Width="95%" Visible="false">

            <fieldset style="margin:0 auto;min-width: 400px; width: 600px;">
                <legend>Recuperación de clave exitosa</legend>

                <div style="margin:0 auto; width: 900px;">
                    <div style="margin: 0 auto; width: 700px; border: solid 1px #95a5a6; background-color: #ecf0f1; border-top-left-radius: 30px; border-top-right-radius: 30px; padding: 30px;">

                        <div style="margin: 0 auto; text-align: center; width: 500px; text-align: center;">
                            <img src="<%=ResolveUrl("~/images/sessionbav.png")%>" />

                            <br />
                            <br />

                            <p style="color: green;"><b>
                                <asp:Literal ID="liMensaje1" runat="server">Recuperaci&oacute;n de Clave realizada Satisfactoriamente</asp:Literal></b></p>
                            <img src="<%=ResolveUrl("~/images/key.png")%>" />
                            <br />
                            <br />
                            <p><b>
                                <asp:Literal ID="liMensaje2" runat="server">Por seguridad te recomendamos cambiar peri&oacute;dicamente tus claves</asp:Literal></b></p>

                            <br />
                            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" CssClass="btn btn-danger" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </fieldset>
        </asp:Panel>


    </div>

    <footer style="margin: 0 auto; max-width: 1024px; min-width: 200px; clear: both; text-align: center; bottom: 0; padding-top: 25px;">
        <div style="margin: 0 auto; text-align: center; max-width: 1024px; min-width: 200px;">
            <div style="border-radius: 5px; background: #336633; padding: 3px; max-width: 1024px; min-width: 200px; height: 30px;">
                <p style="font-size: 14px; color: white;">&#169; Copyright 2015 Banco Agr&iacute;cola de Venezuela. RIF G-20005795-5</p>
            </div>
        </div>
    </footer>
    
    
</asp:Content>

