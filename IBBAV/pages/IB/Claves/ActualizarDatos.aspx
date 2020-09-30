<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="ActualizarDatos.aspx.cs" Inherits="IBBAV.pages.IB.Claves.ActualizarDatos" %>

<%@ Register Src="~/UserControls/PreguntasDesafio.ascx" TagPrefix="uc1" TagName="PreguntasDesafio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var intrvlEmail;
        var intrvlCel;
        function validar() {

            if ($("#tblEmail").is(":visible")) {
                var email = $(".txtCorreo").val();
                var emailc = $(".txtCorreoConfirmar").val();
                var emailreg = new RegExp(/^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/);

                if (email == "") {
                    showAlert("Alerta", "Debe colocar la dirección de correo electrónico");
                    $(".txtCorreo").focus();
                    return false;
                }

                if (!emailreg.test(email)) {
                    showAlert("Alerta", "El formato de la dirección de correo electrónico no es válido");
                    $(".txtCorreo").focus();
                    return false;
                }

                if (emailc == "") {
                    showAlert("Alerta", "Debe confirmar la dirección de correo electrónico");
                    $(".txtCorreoConfirmar").focus();
                    return false;
                }

                if (email != emailc) {
                    showAlert("Alerta", "Las direcciones de correo electrónico no coinciden");
                    $(".txtCorreo").focus();
                    return false;
                }

            }

            if ($("#tblCel").is(":visible")) {
                var c = $(".ddlistNumeroMovil").val();
                var t = $(".txtNumeroMovil").val();
                var c1 = $(".ddlistNumeroMovilConfirm").val();
                var t1 = $(".txtNumeroMovilConfirm").val();

                if (c == "-") {
                    showAlert("Alerta", "Debe seleccionar el código de celular");
                    $(".ddlistNumeroMovil").focus();
                    return false;
                }

                if (t == "") {
                    showAlert("Alerta", "Debe colocar el número de celular");
                    $(".txtNumeroMovil").focus();
                    return false;
                }

                if (c1 == "-") {
                    showAlert("Alerta", "Debe confirmar el código de celular");
                    $(".ddlistNumeroMovilConfirm").focus();
                    return false;
                }

                if (t1 == "") {
                    showAlert("Alerta", "Debe confirmar el número de celular");
                    $(".txtNumeroMovilConfirm").focus();
                    return false;
                }

                if (c + t != c1 + t1) {
                    showAlert("Alerta", "Los números no coinciden");
                    return false;
                }
            }

            
        }

        function showAceptar() {
            $("#<%= btnAceptar.ClientID %>").show();
            if ($("#tblEmail").css("display") == "none" && $("#tblCel").css("display") == "none") {
                $("#<%= btnAceptar.ClientID %>").hide();
            }
        }
        
        $(document).ready(function () {
            
            $("#<%= btnAceptar.ClientID %>").hide();
            $(".rdoEmail").on("change", function () {
                console.log($(this).val());
                if ($(this).hasClass("rdoSi")) {
                    intrvlEmail = setInterval(valdataEmail, 100);
                    $("#tblEmail").show();
                } else {
                    clearInterval(intrvlEmail);
                    $("#<%= txtCorreo.ClientID%>, #<%= txtCorreoConfirmar.ClientID%>").val("");
                    $("#tblEmail").hide();
                }
                showAceptar();
            });

            $(".rdoCel").on("change", function () {
                if ($(this).hasClass("rdoSi")) {
                    intrvlCel = setInterval(valdataCel, 100);
                    $("#tblCel").show();
                } else {
                    clearInterval(intrvlCel);
                    $("#<%= ddlistNumeroMovil.ClientID%>, #<%= ddlistNumeroMovilConfirm.ClientID%>").val("-");
                    $("#<%= txtNumeroMovil.ClientID%>, #<%= txtNumeroMovilConfirm.ClientID%>").val("");
                    $("#tblCel").hide();
                }
                showAceptar();
            });

            
            
            function valdataEmail() {
                var email = new RegExp(/^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/);

                var emailf = $("#<%= txtCorreo.ClientID%>");
                var emailcon = $("#<%= txtCorreoConfirmar.ClientID%>");

                if (email.test(emailf.val())) {
                    $("#emailfor").removeClass("glyphicon-remove");
                    $("#emailfor").addClass("glyphicon-ok");
                    $("#emailfor").css("color", "#00A41E");
                } else {
                    $("#emailfor").removeClass("glyphicon-ok");
                    $("#emailfor").addClass("glyphicon-remove");
                    $("#emailfor").css("color", "#FF0004");
                }

                if (emailf.val() == emailcon.val()) {
                    $("#emailforcon").removeClass("glyphicon-remove");
                    $("#emailforcon").addClass("glyphicon-ok");
                    $("#emailforcon").css("color", "#00A41E");
                } else {
                    $("#emailforcon").removeClass("glyphicon-ok");
                    $("#emailforcon").addClass("glyphicon-remove");
                    $("#emailforcon").css("color", "#FF0004");
                }

            }

            function valdataCel() {

                var numf = new RegExp("[0-9]{7,}");

                var ddlnum = $("#<%= ddlistNumeroMovil.ClientID%>");
                var txtnum = $("#<%= txtNumeroMovil.ClientID%>");
                var ddlnumcon = $("#<%= ddlistNumeroMovilConfirm.ClientID%>");
                var txtnumcon = $("#<%= txtNumeroMovilConfirm.ClientID%>");


                if (ddlnum.val() != "-") {
                    $("#area").removeClass("glyphicon-remove");
                    $("#area").addClass("glyphicon-ok");
                    $("#area").css("color", "#00A41E");
                } else {
                    $("#area").removeClass("glyphicon-ok");
                    $("#area").addClass("glyphicon-remove");
                    $("#area").css("color", "#FF0004");
                }



                if (numf.test(txtnum.val())) {
                    $("#num").removeClass("glyphicon-remove");
                    $("#num").addClass("glyphicon-ok");
                    $("#num").css("color", "#00A41E");
                } else {
                    $("#num").removeClass("glyphicon-ok");
                    $("#num").addClass("glyphicon-remove");
                    $("#num").css("color", "#FF0004");
                }


                if (ddlnum.val() == ddlnumcon.val() && txtnum.val() == txtnumcon.val()) {
                    $("#numcon").removeClass("glyphicon-remove");
                    $("#numcon").addClass("glyphicon-ok");
                    $("#numcon").css("color", "#00A41E");
                } else {
                    $("#numcon").removeClass("glyphicon-ok");
                    $("#numcon").addClass("glyphicon-remove");
                    $("#numcon").css("color", "#FF0004");
                }
            }


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="well">
        Cuando desee asignar un nuevo número de celular de usuario actualícela aquí.
    </div>
    <asp:Panel ID="panelValidacion" runat="server">
        <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="PreguntasAfiliado" TipoRepeater="Table" MaskedInputs="true" />
        <center>
            <asp:Button id="btnAceptarPreguntas" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnAceptarPreguntas_Click" />
            <asp:Button id="btnCancelarPreguntas" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarPreguntas_Click" />
        </center>
    </asp:Panel>
    <asp:Panel ID="panelDatos" runat="server" Visible="false">
        Correo Electronico Afiliado: <asp:Literal ID="liCorreoActual" runat="server"></asp:Literal><br />
        ¿Actualizar? <asp:RadioButton ID="rdoEmailSi" GroupName="rdoEmail" runat="server" Text="Si" CssClass="rdoEmail rdoSi"  />&nbsp;<asp:RadioButton ID="rdoEmailNo" GroupName="rdoEmail" runat="server" Text="No" Checked="true" CssClass="rdoEmail rdoNo" />
        <table id="tblEmail" class="table table-responsive table-bav" style="display:none">
            <tr>
                <td class="fieldTxt">Nuevo Correo electrónico:</td>
                <td class="fieldVal">
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control input-sm txtCorreo" MaxLength="100"></asp:TextBox></td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <span id="emailfor" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>Formato correcto
                </td>        
                <td></td>
            </tr>
            <tr>
                <td class="fieldTxt">Confirme el Correo electrónico:</td>
                <td class="fieldVal">
                    <asp:TextBox ID="txtCorreoConfirmar" runat="server" CssClass="form-control input-sm txtCorreoConfirmar" MaxLength="100"></asp:TextBox></td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <span id="emailforcon" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>Los correos coinciden
                </td>        
                <td></td>
            </tr>
        </table>
        <hr />
        Numero de Celular Afiliado: <asp:Literal ID="liNumeroActual" runat="server"></asp:Literal><br />
        ¿Actualizar? <asp:RadioButton ID="rdoCelSi" GroupName="rdoCel" runat="server" Text="Si" CssClass="rdoCel rdoSi" />&nbsp;<asp:RadioButton ID="rdoCelNo" GroupName="rdoCel" runat="server" Text="No" Checked="true" CssClass="rdoCel rdoNo" />
        <table id="tblCel" class="table table-responsive table-bav" style="display:none">
            
            <tr>
                <td class="fieldTxt">Nuevo Número a Afiliar:</td>
                <td class="fieldVal">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <asp:DropDownList ID="ddlistNumeroMovil" runat="server" CssClass="form-control input-sm ddlistNumeroMovil" Style="border-top-left-radius: 3px; border-bottom-left-radius: 3px;">
                                    <asp:ListItem Text="---" Value="-" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="0412" Value="0412"></asp:ListItem>
                                    <asp:ListItem Text="0416" Value="0416"></asp:ListItem>
                                    <asp:ListItem Text="0414" Value="0414"></asp:ListItem>
                                    <asp:ListItem Text="0426" Value="0426"></asp:ListItem>
                                    <asp:ListItem Text="0424" Value="0424"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:TextBox ID="txtNumeroMovil" runat="server" CssClass="form-control input-sm form-numeric txtNumeroMovil" MaxLength="7"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <span id="area" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>Código de Area
                    <span id="num" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>7 dígitos
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="fieldTxt">Confirme el Nuevo Número a Afiliar:</td>
                <td class="fieldVal">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <asp:DropDownList ID="ddlistNumeroMovilConfirm" runat="server" CssClass="form-control input-sm ddlistNumeroMovilConfirm" Style="border-top-left-radius: 3px; border-bottom-left-radius: 3px;">
                                    <asp:ListItem Text="---" Value="-" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="0412" Value="0412"></asp:ListItem>
                                    <asp:ListItem Text="0416" Value="0416"></asp:ListItem>
                                    <asp:ListItem Text="0414" Value="0414"></asp:ListItem>
                                    <asp:ListItem Text="0426" Value="0426"></asp:ListItem>
                                    <asp:ListItem Text="0424" Value="0424"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:TextBox ID="txtNumeroMovilConfirm" runat="server" CssClass="form-control input-sm form-numeric txtNumeroMovilConfirm" MaxLength="7"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <span id="numcon" class="glyphicon glyphicon-remove" style="color: rgb(255, 0, 4);"></span>Los números coinciden
                </td>
                <td></td>

            </tr>
        </table>
        <asp:Panel ID="pnlBotones" runat="server">
            <center>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-danger" onClientClick="return validar();" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="btnRegresar_Click" />
            </center>
        </asp:Panel>

    </asp:Panel>
    <asp:Literal ID="liScript" runat="server"></asp:Literal>
</asp:Content>
