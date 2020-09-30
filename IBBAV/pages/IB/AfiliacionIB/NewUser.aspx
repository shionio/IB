<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="IBBAV.pages.IB.AfiliacionIB.NewUser" MasterPageFile="~/pages/BAVMasterSimple.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .info {
            cursor: help;
            margin-left: 4px;
        }
        .texto-msg {
            font-size: 12pt;
            text-align: justify;
        
        }

        .panel-primary > .panel-heading {
        color: #fff;
        border-color: #336633 !important;
        background-color: #336633 !important;
        background-image: linear-gradient(to bottom, #4B984B 5%, #336633 95%);
        }


        .panel-primary
        {
            border-color: #336633 !important;
        }
        .panel-heading
        {
            background-color: rgb(51, 102, 51) !important;
            border-color: #336633 !important;
        }
    </style>

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
            $(document).on('focus', ':input', function () {
                $(this).attr('autocomplete', 'off');
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

            $('#<%= txtCedula.ClientID %>').keypress(function (e) {
                var selects = document.getElementById('<%=ddlTipoCedula.ClientID %>');
                var selectedValue = selects.options[selects.selectedIndex].value;
                if (selectedValue == "V" || selectedValue == "E" || selectedValue == "J") {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        //display error message
                        //$("#errmsg").html("Sólo números!").show().fadeOut("slow");
                        return false;
                    }
                }
                if (selectedValue == "P") {
                    var regex = new RegExp("^[a-zA-Z0-9]+$");
                    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                    if (regex.test(str)) {
                        return true;
                    }
                }
            });
        });
        function getval() {
            $('#<%= txtCedula.ClientID %>').val("");
        }

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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div id="afi" style="margin: 0 auto; min-width:450px; width: 650px; ">
            <asp:Panel ID="panelDatos" runat="server">
                <fieldset style="min-width:400px; width:600px;">
                    <legend>Datos de Afiliaci&oacute;n</legend>
                    <div class="form-group">
                        <label for="tddnum">N° de Tarjeta de D&eacute;bito</label><br />
                        <asp:TextBox runat="server" cssclass="form-control" id="tddnumtxt" title="Tarjeta de débito" placeholder="Número de TDD" style="min-width: 250px; width:350px;" maxlength="19"></asp:TextBox>
                        <span class="info" title="Su número de tarjeta de débito (19 dígitos)">(?)</span>
                    </div>
                    <label for="Id">Identificaci&oacute;n</label>
                    <div class="form-group">
                        <div class="controls form-inline">
                            <asp:DropDownList runat="server" ID="ddlTipoCedula" class="form-control" style="min-width:50px; width: 150px;" onchange="getval();">
                                <asp:ListItem value="">--Seleccione--</asp:ListItem>
                                <asp:ListItem value="V">V</asp:ListItem>
                                <asp:ListItem value="E">E</asp:ListItem>
                                <asp:ListItem value="P">P</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" cssclass="form-control" placeholder="Ejemplo:16874125 " id="txtCedula" maxlength="9" style="min-width: 100px; width:200px;"></asp:TextBox>
                            <span class="info" title="Su número de identificación">(?)</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="tddnum">&Uacute;ltimos 4 d&iacute;gitos de su n&uacute;mero de cuenta</label>
                        <br />
                        <asp:TextBox runat="server" type="password" cssclass="form-control" id="txtCta" placeholder="4 últimos número de su cuenta" style="width: 80px;" maxlength="4"></asp:TextBox>
                        <span class="info" title="Indique los últimos 4 dígitos de su número de cuenta">(?)</span>
                    </div>

                    <div style="margin:0 auto; text-align:center">
                        <asp:button runat="server" ID="btnceptar" cssclass="Btn btn-danger" Text="Aceptar" OnClick="btnAceptar_Click" OnClientClick="if(validatefields()==false) return (false);" CausesValidation="true" />
                        <asp:button runat="server" ID="btncancel" cssclass="Btn btn-danger" Text="Cancelar" OnClick="btnCancelar_Click" />
                    </div>
                </fieldset>

                <hr style="color:red;" />

                <!--<div style="margin:0 auto; padding-top:20px;">
                    <img src="<%=ResolveUrl("~/images/propa.png") %>" class="img-responsive" />
                </div>-->
            </asp:Panel>
            <asp:Panel ID="panelMSG" runat="server" Visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <span>Clave de Afiliaci&oacute;n Temporal</span>
                    </div>
                    <div class="panel-body">
                        <table class="table table-responsive">
                            <tr>
                                <td style="text-align: center;">
                                    <img src="<%=ResolveClientUrl("~/images/message_not.png")%>" class="img-responsive" />
                                </td>
                                <td style="text-align: center;">
                                    <p class="texto-msg">Consulte en su <b>celular</b> el mensaje de Banco Agr&iacute;cola de Venezuela con la <b>Clave de Afiliaci&oacute;n Temporal</b> para completar el proceso.</p>
                                    <p class="texto-msg">Recuerde que esa clave ser&aacute; unicamente v&aacute;lida para realizar la Afiliaci&oacute;n a BAV en L&iacute;nea y su tiempo de duraci&oacute;n es de tres (03) minutos.</p>
                                    <p class="texto-msg">Si la clave no llega a su n&uacute;mero celular, por favor dir&iacute;jase a la Agencia Bancaria o llame al n&uacute;mero 0500-228.00.01</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center">
                                    Coloque la clave de afiliación enviada: <asp:TextBox ID="txtClave" TextMode="Password" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                </td>    
                            </tr>
                        </table>
                        <hr />
                        <center>
                            <asp:Button ID="btnContinuarSMS" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnContinuarSMS_Click" />&nbsp;
                            <asp:Button ID="btnCancelarSMS" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarSMS_Click" />
                        </center>
                    </div>
                </div>
            </asp:Panel>
        </div>

       <!--footer-->
            <footer style="margin:0 auto; width:1024px; min-width:1024px; clear:both; text-align:center; bottom:0; padding-top:40px;">
            <div style="margin:0 auto; text-align:center; width:1024px;">
                <div style="border-radius: 5px; background: #336633; padding: 3px; width: 1024px;  height: 30px;">
                    <p style="font-size: 14px; color:white;">&#169; Copyright 2015 Banco Agr&iacute;cola de Venezuela. RIF G-20005795-5</p>
                </div>
            </div>
        </footer>

</asp:Content>

