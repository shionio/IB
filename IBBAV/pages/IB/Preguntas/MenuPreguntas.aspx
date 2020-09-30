<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="MenuPreguntas.aspx.cs" Inherits="IBBAV.pages.IB.Preguntas.MenuPreguntas" %>

<%@ Register Src="~/UserControls/PreguntasDesafio.ascx" TagPrefix="uc1" TagName="PreguntasDesafio" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
    <style>
        .gi-2x {
            font-size: 1em;
        }

        .gi-3x {
            font-size: 2em;
        }

        .gi-4x {
            font-size: 3em;
        }

        .gi-5x {
            font-size: 4em;
        }

        #msgimg {
            cursor:pointer;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="well">
            Esta opción te permite re-establecer sus preguntas de seguridad. Le recomendamos no colocar espacios en blanco en las respuestas.
        </div>

        <asp:Panel ID="panelHome" runat="server" Visible="true">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span>Cambio de preguntas de seguridad</span>
                </div>
                <div class="panel-body">
                    <table class="table table-responsive">
                        <thead>
                            <tr>
                                <td style="text-align: center;">
                                    <p>Al presionar "Aceptar" se enviará un mensaje al número de celular asociado al servicio de mensajería de BAV En línea. Ingrese la clave temporal para proceder con el cambio de sus preguntas de seguridad.</p>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div style="margin: 0 auto; position:relative; text-align:center; width:80px;" id="divimg">
                                        <img src="<%=ResolveClientUrl("~/images/msgsent.png")%>" id="msgimg" />
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center;">
                                    <div class="form-group" style="margin-bottom: 0; padding-top: 20px;">
                                        <asp:Button runat="server" ID="btnhome" CssClass="btn btn-danger" Text="Aceptar" OnClick="btnhome_Click" />
                                        <asp:Button runat="server" ID="btnhomecancel" CssClass="btn btn-danger" Text="Cancelar" OnClick="btncancel_Click" />
                                    </div>
                                </td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </asp:Panel>


        <asp:Panel ID="panelValidacion" runat="server" Visible="false">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span>Clave de operaciones especiales</span>
                </div>
                <div class="panel-body">
                    <table class="table table-responsive">
                        <tr>
                            <td style="text-align: center;">
                                <img src="<%=ResolveClientUrl("~/images/message_not.png")%>" class="img-responsive" />
                            </td>
                            <td style="text-align: center;">
                                <p class="texto-msg">Consulte en su <b>celular</b> el mensaje de Banco Agr&iacute;cola de Venezuela con la <b>Clave de Afiliaci&oacute;n Temporal</b> para completar el proceso.</p>
                                <p class="texto-msg">Si la clave no llega a su n&uacute;mero celular, por favor dir&iacute;jase a la Agencia Bancaria o llame al n&uacute;mero 0500-228.00.01</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">Coloque la clave de operaciones especiales enviada:
                                <asp:TextBox ID="txtClave" TextMode="Password" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
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
        <asp:Panel ID="panelDatos" runat="server" Visible="false">
            <table id="tabla" class="table-striped" data-height="299" data-cache="false">
                <thead>
                    <tr>
                        <td>
                            <fieldset style="min-width: 400px;">
                                <legend>Preguntas de Seguridad</legend>
                                <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="Preguntas" TipoRepeater="Div" MaskedInputs="false" />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group" style="margin-bottom: 0; padding-top: 20px;">
                                <div class="col-sm-offset-4 col-sm-10">
                                    <asp:Button runat="server" ID="btnceptar" CssClass="btn btn-danger" Text="Aceptar" OnClick="btnceptar_Click" OnClientClick="if(validatefields()==false) return (false);" CausesValidation="true" />
                                    <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancelar" OnClick="btncancel_Click" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </thead>
            </table>
        </asp:Panel>

        <asp:Panel ID="panelCambiOk" runat="server" Visible="false">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span>Cambio de preguntas de seguridad</span>
                </div>
                <div class="panel-body">
                    <table class="table table-responsive">
                        <thead>
                            <tr>
                                <td style="text-align: center;">
                                    <p>Sus preguntas de seguridad han sido reestablecidas satisfactoriamente. Por favor, intente no olvidar las respuestas.</p>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center;">
                                    <img src="<%=ResolveClientUrl("~/images/white_list.png")%>" />
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center;">
                                    <div class="form-group" style="margin-bottom: 0; padding-top: 20px;">
                                        <asp:Button runat="server" ID="btnokregresar" CssClass="btn btn-danger" Text="Aceptar" OnClick="btncancel_Click" />
                                    </div>
                                </td>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
      <script>
          $(document).ready(function () {
              $('#msgimg').click(function () {
                  $(this).effect("bounce", { times: 6 }, 1500);
              });
          });
    </script>
</asp:Content>
