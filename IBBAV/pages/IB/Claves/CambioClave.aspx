<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="CambioClave.aspx.cs" Inherits="IBBAV.pages.IB.Claves.CambioClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=ResolveUrl("~/css/jquery-bav-keyboard.css") + "?t=" + DateTime.Now.Ticks %>" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-bav-keyboard.js") + "?t=" + DateTime.Now.Ticks %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/js/tooltipsy.min.js") + "?t=" + DateTime.Now.Ticks %>"></script>
    <style>
        .info {
            cursor: help;
            margin-left: 4px;
        }
    </style>
    <script type="text/javascript">

        function validarClaves() {
            var i = $(".ca, .cn, .ccn");
            i.parent().removeClass("has-error");

            i = $(".ca");
            if (i.val() == "") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Su clave actual es requerida");
                return false;
            }

            i = $(".cn");
            if (i.val() == "") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Su nueva clave es requerida");
                return false;
            }

            i = $(".cn")
            var patt = /(.)\1\1/;
            if (patt.test(i.val())) {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Su clave no puede contener caracteres repetidos");
                return false;
            }


            i = $(".cn");
            if (i.val().length < 8) {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Su nueva clave debe tener al menos 9 caracteres, con al menos 4 caracteres alfabético, 4 caracter numéricos, 1 caracter especial");
                return false;
            }

            i = $(".ccn");
            if (i.val() == "") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "La confirmación de su nueva clave es requerida");
                return false;
            }
            i = $(".cn, .ccn");
            //Modificado 30/08/2018 por Liliana Guerra 
            //if ($(".cn").val()!= $(".ccn").val()) {
            if ($(".cn").val().toUpperCase() != $(".ccn").val().toUpperCase()) {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "La nueva clave y la confirmación son diferentes");
                return false;
            }
            return true;
        }
        $("#btnCancelar, #btnRegresar").on("click", function (e) {
            var a = window.parent.$(".gw-nav").find("li:first").find("a");
            window.parent.$(".gw-nav").attr("src", a.attr("href"));
            a.click();
            e.preventDefault();
        });

    
        $(document).ready(function () {
            $('.jkeyboard').jkeyboard({
                target: '#keyboard',
                inputId: ['<%= txtPass.ClientID%>', '<%= txtPassNew.ClientID%>', '<%= txtPassNewConfirm.ClientID%>'],
                radioId: ['rdoPass', 'rdoPassNew', 'rdoPassNewConfirm']
            });

            $('#<%= txtPass.ClientID %>').on("focus", function () {
                $("#rdoPass").prop("checked", true);
                $("#rdoPassNew").prop("checked", false);
                $("#rdoPassNewConfirm").prop("checked", false);
            });

            $('#<%= txtPassNew.ClientID %>').on("focus", function () {
                $("#rdoPassNew").prop("checked", true);
                $("#rdoPass").prop("checked", false);
                $("#rdoPassNewConfirm").prop("checked", false);
            });

            $('#<%= txtPassNewConfirm.ClientID %>').on("focus", function () {
                $("#rdoPassNewConfirm").prop("checked", true);
                $("#rdoPass").prop("checked", false);
                $("#rdoPassNew").prop("checked", false);
            });

            setInterval(valdata, 100);

            function valdata() {

                var splchar = new RegExp(/[$@!%*#?&\.]{1}/),
                //Modificado 30/08/2018 por Liliana Guerra 
                //char = new RegExp(/[A-Z]/g),
                char = new RegExp(/[A-z]/g),
                num = new RegExp(/[0-9]/g),

                rc = $("#reqclv"),
                e8 = $("#8char"),
                e1 = $("#1splchar"),
                n1 = $("#num"),
                mc = $("#pwmatch"),

                clvact = $("#<%= txtPass.ClientID%>"),
                clv = $("#<%= txtPassNew.ClientID%>"),
                clvcon = $("#<%= txtPassNewConfirm.ClientID%>");




                if (clvact.val() != "") {
                    rc.removeClass("glyphicon-remove")
                    .addClass("glyphicon-ok")
                    .css("color", "#00A41E");
                } else {
                    rc.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                    .css("color", "#FF0004");
                }

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

                //Modificado 30/08/2018 por Liliana Guerra para desactivar case sensitive en validación
                //if (clv.val() == clvcon.val()) {
                var clv = clv.val().toUpperCase();
                var clvcon = clvcon.val().toUpperCase();
                
                if ((clv ==clvcon) && (clv != "")) {
                    mc.removeClass("glyphicon-remove")
                    .addClass("glyphicon-ok")
                    .css("color", "#00A41E");
                } else {
                    mc.removeClass("glyphicon-ok")
                    .addClass("glyphicon-remove")
                    .css("color", "#FF0004");
                }

            }

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

        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        Cuando desee asignar una nueva clave de usuario actualícela aquí.
    </div>

    <table id="tblClaves" runat="server" class="table table-responsive table-bav">
        <tr>
            <td class="fieldTxt">Clave Actual:</td>
            <td class="fieldVal">
                <div class="input-group">
                    <span class="input-group-addon">
                        <input name="clave" id="rdoPass" type="radio" data-input="<%= txtPass.ClientID%>" checked tabindex="-1" />
                    </span>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control jkeyboard ca" placeholder="No distingue may&uacute;scula y min&uacute;scula" MaxLength="9"></asp:TextBox>
                </div>
            </td>
            <td class="fieldInf"><span class="info" title="Introduzca su clave actual">(?)</span></td>
        </tr>
        <tr>
            <td></td>
            <td><span id="reqclv" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> Clave actual requerida</td>
            <td></td>
        </tr>
        <tr>
            <td class="fieldTxt">Nueva Clave:</td>
            <td class="fieldVal">
                <div class="input-group">
                    <span class="input-group-addon">
                        <input name="clave" id="rdoPassNew" type="radio" data-input="<%= txtPassNew.ClientID%>" tabindex="-1" />
                    </span>
                    <asp:TextBox ID="txtPassNew" runat="server" TextMode="Password" CssClass="form-control cn jkeyboard" placeholder="Nueva Clave" MaxLength="9"></asp:TextBox>
                </div>
                
            </td>
            <td class="fieldInf"><span class="info" title="Introduzca la nueva clave (mínimo 8 caracteres)">(?)</span></td>
        </tr>
        <tr>
            <td></td>
            <td><span id="8char" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 4 Letras
                    <span id="num" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 4 Numeros
                    <span id="1splchar" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> 1 Caracter Especial</td>
            <td></td>
        </tr>
        <tr>
            <td class="fieldTxt">Confirmar Nueva Clave:</td>
            <td class="fieldVal">
                <div class="input-group">
                    <span class="input-group-addon">
                        <input name="clave" id="rdoPassNewConfirm" data-input="<%= txtPassNewConfirm.ClientID%>" type="radio" tabindex="-1" />
                    </span>
                    <asp:TextBox ID="txtPassNewConfirm" runat="server" TextMode="Password" CssClass="form-control ccn jkeyboard" placeholder="Confirmar" MaxLength="9"></asp:TextBox>
                </div>
            </td>
            <td class="fieldInf"><span class="info" title="Confirme la nueva clave">(?)</span></td>
        </tr>
        <tr>
            <td></td>
            <td><span id="pwmatch" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span> Las claves coinciden</td>
            <td></td>
        </tr>
        <tr>
            <td ></td>
            <td style="text-align:center">

                <div id="keyboard" style="position: relative; display: block; padding: 5px 0px 40px 15px; width: 0;"></div>
            </td>
            <td ></td>
        </tr>

        <tr>
            <td class="fieldTxt">Vencimiento de Clave:</td>
            <td class="fieldVal">
                <asp:DropDownList ID="ddlDiasCaducidad" runat="server" CssClass="form-control input-sm"></asp:DropDownList></td>
            <td class="fieldInf"><span class="info" title="Seleccione los dias de vencimiento de su nueva clave">(?)</span></td>
        </tr>
        <tr>
            <td colspan="3" class="buttons">
                <asp:Button ID="btnConfirmar" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClientClick="return validarClaves();" OnClick="btnConfirmar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click1" />
            </td>
        </tr>
    </table>
    <table id="tblResult" class="table table-responsive table-bav" runat="server" visible="false">
        <tr>
            <td class="fieldTxt msg1"><b>
                <asp:Literal ID="liMensaje1" runat="server"></asp:Literal></b></td>
        </tr>
        <tr>
            <td class="fieldTxt msg2"><b>
                <asp:Literal ID="liMensaje2" runat="server"></asp:Literal></b></td>
        </tr>
        <tr>
            <td class="buttons">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="btnRegresar_Click" />
            </td>
        </tr>
    </table>


    
    
</asp:Content>

