<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetNewUser.aspx.cs" Inherits="IBBAV.pages.IB.AfiliacionIB.GetNewUser" MasterPageFile="~/pages/BAVMasterSimple.Master" %>
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

    <script type="text/javascript">
        if (window.frameElement) {
            window.parent.location = "<%= ResolveUrl("~/Login.aspx")%>";
        }
        else {
            // not in frame
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
            $("form").addClass("form-horizontal");

            $(document).on('focus', ':input', function () {
                $(this).attr('autocomplete', 'off');
            });


            setInterval(valdata, 100);

            function valdata() {

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

                //Modificado 29/08/2018 por Liliana Guerra para desactivar case sensitive en validación
                //if (clv.val() == clvcon.val()) {
                //if (clv.val.toUpperCase() == clvcon.val().toUpperCase()) {

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





            function validatefields() {
                var user = $('#<%= txtUsuario.ClientID %>').val();
                // Modificado por Liliana Guerra 30/08/2018
                var clave = $('#<%= txtClave.ClientID %>').val().toUpperCase();
                var claveconf = $('#<%= txtClaveConfirmar.ClientID %>').val().toUpperCase();
                var ce = $('#<%= txtCorreo.ClientID %>').val();
                var cce = $('#<%= txtCorreoConfirmar.ClientID %>').val();
                var patt = /(.)\1\1/;

                if (user == "") {
                    alert("Debe indicar el usuario de acceso");
                    $('#<%= txtUsuario.ClientID %>').focus();
                    return false;
                }
                else if (!isNiceUser(user)) {
                    alert("El nombre de usuario debe tener entre 6 y 10 letras y/o números, sin espacios, comas ni puntos");
                    $('#<%= txtUsuario.ClientID %>').focus();
                    return false;
                }
                else if (clave == "") {
                    alert("Debe indicar su clave de acceso");
                    $('#<%= txtClave.ClientID %>').focus();
                    return false;
                }
                else if (patt.test(clave)) {
                    alert("Su clave no puede contener caracteres repetidos");
                    $('#<%= txtClave.ClientID %>').focus();
                    return false;
                }
                else if (clave.length < 8) {
                    alert("Su nueva clave debe tener al menos 8 caracteres, con al menos 1 caracter alfabético, 1 caracter numérico, 1 caracter especial");
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
                else if (ce == "") {
                    alert("Debe indicar el correo electrónico");
                    $('#<%= txtCorreo.ClientID %>').focus();
                    return false;
                }
                else if (!isValidEmailAddress(ce)) {
                    alert("Formato de correo inválido");
                    $('#<%= txtCorreo.ClientID %>').focus();
                    return false;
                }
                else if (cce == "") {
                    alert("Debe confirmar el correo electrónico");
                    $('#<%= txtCorreoConfirmar.ClientID %>').focus();
                    return false;
                }
                else if (!isValidEmailAddress(cce)) {
                    alert("Formato de correo inválido");
                    $('#<%= txtCorreoConfirmar.ClientID %>').focus();
                    return false;
                }
                else if (ce != cce) {
                    alert("El correo electrónico y la confirmación no son iguales");
                    $('#<%= txtCorreoConfirmar.ClientID %>').focus();
                    return false;
                }
            }

            function isValidEmailAddress(emailAddress) {
                var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
                return pattern.test(emailAddress);
            };

            function isNiceUser(user) {
                var patternuser = new RegExp("([a-z|A-Z|0-9]){6,10}$");
                return patternuser.test(user);
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="afi" style="margin: 0 auto; min-width: 450px; width: 800px; ">
        <asp:Panel ID="panelNuevoAfiliado" runat="server" Width="95%">
            <div class="form-group">
                <label for="lblNombreCompleto">Nombre: </label>
                <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label>
                <br />
                <label for="lblCedula">C&eacute;dula: </label>
                <asp:Label ID="lblCedula" runat="server"></asp:Label>
            </div>


            <fieldset style="min-width: 400px;">
                <legend>Datos de Afiliaci&oacute;n</legend>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4 control-label">Usuario Internet Banking:</label>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUsuario" title="usuario" placeholder="Usuario" Style="min-width: 250px; width: 350px;" MaxLength="10"></asp:TextBox>
                        <span class="info" title="Usuario de acceso al sistema de BAV en línea">(?)</span>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtClave" class="col-sm-4 control-label">Clave de Acceso:</label>
                    <div class="input-group col-sm-7">
                        <span class="input-group-addon">
                            <input name="clave" id="rdoClave" type="radio" data-input="<%= txtClave.ClientID%>" checked tabindex="-1"  />
                        </span>
                        <asp:TextBox runat="server" CssClass="form-control jkeyboard" ID="txtClave" title="clave" TextMode="Password" placeholder="No distingue may&uacute;scula y min&uacute;scula" Style="min-width: 250px; width: 350px;" MaxLength="9"></asp:TextBox>
                        <span class="info" title="Clave de acceso al sistema de BAV en línea">(?)</span>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label for="txtClave" class="col-sm-4 control-label">&nbsp;</label>
                    <div class="col-sm-6">
                        <span id="8char" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 4 Letras
                        <span id="num" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 4 Numeros
                        <span id="1splchar" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 1 Caracter Especial
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtClaveConfirmar" class="col-sm-4 control-label">Confirmar Clave de Acceso:</label>
                    <div class="input-group col-sm-7">
                        <span class="input-group-addon">
                            <input name="clave" id="rdoClaveConfirm" data-input="<%= txtClaveConfirmar.ClientID%>" type="radio" tabindex="-1"  />
                        </span>
                        <asp:TextBox runat="server" CssClass="form-control jkeyboard" ID="txtClaveConfirmar" title="clave" TextMode="Password" placeholder="Confirmar Clave" Style="min-width: 250px; width: 350px;" MaxLength="9"></asp:TextBox>
                        <span class="info" title="Confirma la Clave de acceso al sistema de BAV en línea">(?)</span>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtClave" class="col-sm-4 control-label">&nbsp;</label>
                    <div class="col-sm-6">
                        <span id="pwmatch" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> Las claves coinciden
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtClave" class="col-sm-4 control-label"></label>
                    <div class="col-sm-7">
                        <div id="keyboard" style="position: relative; display: block; float: left; padding: 5px 0px 40px 0px; width: 0;"></div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Id" class="col-sm-4 control-label">Vencimiento clave de acceso:</label>
                    <div class="col-sm-7" style="width: 46.7%">
                        <asp:DropDownList runat="server" class="form-control" Style="min-width: 50px; width: 120px;" ID="ddlDiasCaducidad"></asp:DropDownList>
                    </div>
                    <div style="float: left;"><span class="info" title="Elija los días de vencimiento de su clave de acceso">(?)</span></div>
                </div>

                <div class="form-group">
                    <label for="txtCorreo" class="col-sm-4 control-label">Correo Electr&oacute;nico:</label>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCorreo" title="correo electrónico" placeholder="ejemplo@ejemplo.com" Style="min-width: 250px; width: 350px;" MaxLength="40"></asp:TextBox>
                        <span class="info" title="Ingresa la dirección de correo electrónico donde quieras recibir las notificaciones de BAV en línea">(?)</span>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtCorreoConfirmar" class="col-sm-4 control-label">Confirmar Correo Electr&oacute;nico:</label>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCorreoConfirmar" title="confirma correo electrónico" placeholder="ejemplo@ejemplo.com" Style="min-width: 250px; width: 350px;" MaxLength="40"></asp:TextBox>
                        <span class="info" title="Confirma el Correo Electrónico">(?)</span>
                    </div>
                </div>
                <fieldset style="min-width: 400px;">
                    <legend>Preguntas de Seguridad</legend>
                    <uc1:PreguntasDesafio runat="server" id="PreguntasDesafio" TipoPreguntas="Preguntas" TipoRepeater="Div" MaskedInputs="false" />
                </fieldset>
                <div class="form-group" style="margin-bottom: 0; padding-top:20px;">
                    <div class="col-sm-offset-4 col-sm-10">
                        <asp:Button runat="server" ID="btnceptar" CssClass="Btn btn-danger" Text="Aceptar" OnClick="btnAceptar_Click" OnClientClick="if(validatefields()==false) return (false);" CausesValidation="true" />
                        <asp:Button runat="server" ID="btncancel" CssClass="Btn btn-danger" Text="Cancelar" OnClick="btnCancelar_Click" />
                    </div>
                </div>
            </fieldset>
            
        </asp:Panel>

        <asp:Panel ID="panelAfiliacionExitosa" runat="server" Visible="false" Width="95%" HorizontalAlign="Center">
            <table width="95%">
                <tr class="tab_bg1">
                    <td style="height: 20px; text-align: center" class="tab_tit" align="center">
                        <!--<asp:Literal ID="liTitulo" runat="server" Text="Afiliaci&oacute;n Exitosa"></asp:Literal>-->
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <table align="center">
                            <tr>
                                <td><b>
                                    <asp:Literal ID="liMensaje1" runat="server" Text="El proceso de Afiliación se ha realizado exitosamente"></asp:Literal></b>
                                    <br />
                                    <br />
                                    <asp:Literal ID="liMensaje2" runat="server" Text="Te informamos que de ahora en adelante, debes utilizar este Usuario y Clave para ingresar a BAV en línea"></asp:Literal>
                                    <br />
                                    <br />
                                    <asp:Literal ID="liMensaje3" runat="server" Text='Haz "Clic" en ACEPTAR para iniciar sesión'></asp:Literal>

                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAceptar2" runat="server" Text="Aceptar" CssClass="Btn btn-danger" OnClick="btnAceptar2_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>

    <!--footer-->
    <footer style="margin: 0 auto; width: 1024px; min-width: 1024px; clear: both; text-align: center; bottom: 0; padding-top: 40px;">
        <div style="margin: 0 auto; text-align: center; width: 1024px;">
            <div style="border-radius: 5px; background: #336633; padding: 3px; width: 1024px; height: 30px;">
                <p style="font-size: 14px; color: white;">&#169; Copyright 2015 Banco Agr&iacute;cola de Venezuela. RIF G-20005795-5</p>
            </div>
        </div>
    </footer>

    
    

</asp:Content>
