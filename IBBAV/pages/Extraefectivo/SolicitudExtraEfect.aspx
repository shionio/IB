

<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="SolicitudExtraEfect.aspx.cs" Inherits="IBBAV.pages.Extraefectivo.SolicitudExtraEfect" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function validar() {
            
        }        
        $(document).on("ready", function (e) {
            $("#btnImprimir").on("click", function () {
                $("#<%= panelRecibo.ClientID %>").print({
                    // Use Global styles
                    globalStyles: true,
                    // Add link with attrbute media=print
                    mediaPrint: false,
                    //Custom stylesheet
                    stylesheet: null,
                    //Print in a hidden iframe
                    iframe: true,
                    // Don't print this
                    noPrintSelector: ".avoid-this",
                    // Add this on top
                    append: "",
                    // Add this at bottom
                    prepend: "BAV - Banco Agrícola de Venezuela",
                    // Manually add form values
                    manuallyCopyFormValues: true,
                    // resolves after print and restructure the code for better maintainability
                    deferred: $.Deferred()
                });
            });
        });

        function formatoMoneda() {
            try {
                var monto = $("#ctl00_ContentPlaceHolder1_txtMonto").val();

                if (monto.substr(-1) == '.' || monto.substr(-1) == ',') {

                } else {
                    monto = monto.replace(".", "");
                    monto = monto.replace(".", "");
                    monto = monto.replace(".", "");
                    monto = monto.replace(".", "");

                    monto = monto.replace(",", ".");

                    if (monto.substr(-3) == '.10') {
                        monto = new Intl.NumberFormat('de-DE').format(monto);
                        document.getElementById('ctl00_ContentPlaceHolder1_txtMonto').value = monto + '0';
                    } else {
                        monto = new Intl.NumberFormat('de-DE').format(monto);
                        document.getElementById('ctl00_ContentPlaceHolder1_txtMonto').value = monto;
                    }
                }
            } catch (err){

            }
        }
        /*
        $("#ctl00_ContentPlaceHolder1_txtMonto").blur(function () {
            $(this).parseNumber({ format: "#,###.00", locale: "us" });
            $(this).formatNumber({ format: "#,###.00", locale: "us" });
        });*/

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        
        <asp:Panel ID="mensaje" runat="server">
            <div class="well" >
                Recuerda:
			        <br />
                    <br/>	
			    Esta operación se realiza en línea y será acredita a la cuenta destino una vez aprobada la operación.
                    <br />            
			    BAV Banco Agrícola de Venezuela se complace en servirle con eficiencia y seguridad.
                    <br/>	
                    <br/>	
                <asp:Label ID="lblDescription" runat="server" Text="lblDescription" ></asp:Label>
                    <br />
                    <br/>
            </div>
        </asp:Panel>
    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="tblClaves" runat="server" class="table table-responsive table-bav">
                        <tr>
                            <td class="fieldTxt">Tarjeta de Crédito:</td>
                            <td class="fieldVal"><cc1:DropDownListCuentas ID="TarjCredito" runat="server" OnSelectedIndexChanged="TarjCredito_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control cd"> </cc1:DropDownListCuentas></td>
                            <td class="fieldInf"></td>
                        </tr>                        
                        <tr>
                            <td class="fieldTxt"><asp:Literal ID="liCuentaAcreditar" runat="server" Text="Cuenta a Acreditar:"></asp:Literal></td>
                            <td class="fieldVal"><cc1:DropDownListCuentas ID="CtaAcreditar" runat="server" OnSelectedIndexChanged="CtaAcreditar_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control cca"> </cc1:DropDownListCuentas></td>
                            <td class="fieldInf"></td>
                        </tr>  <tr>
                            <td class="fieldTxt">Tarjeta de Crédito:</td>
                            <td class="fieldVal"><asp:TextBox ID="tarjetaCred" runat="server" CssClass="form-control tca" ReadOnly="true" Width="160px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                         <tr>
                            <td class="fieldTxt">Monto Disponible:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtdisponible" runat="server" CssClass="form-control m" ReadOnly="true" Width="120px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>                      
                        <tr id="trCuentaAcreditar" runat="server" >
                            <td class="fieldTxt">Cuenta a Acreditar:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtCuentaAcreditar" runat="server" CssClass="form-control tca" ReadOnly="true" Width="160px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>   
                        <tr>
                            <td class="fieldTxt">Monto Solicitado:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtMonto"  runat="server" CssClass="form-control css_monto"  ReadOnly="false" Width="120px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>    
                        <tr ID="cuotasCant" visible="false">
                            <td class="fieldTxt"><asp:Literal ID="liCuotas" runat="server" Text="Cuotas:"></asp:Literal></td>
                            <td class="fieldVal">
                                <asp:DropDownList ID="CantCuotas" runat="server" OnSelectedIndexChanged="Cuotas_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control cca">
                                    <asp:ListItem>Seleccione la cantidad de meses para pagar:</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem> 
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>36</asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr id="MontoCuota" runat="server" visible="false">
                            <td class="fieldTxt">Monto Cuota Mensual:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtMontoCuota" runat="server" CssClass="form-control tca" ReadOnly="true" Width="160px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr>							
						    <td colspan="3" class="buttons">
                                <asp:Button ID="btnConfirmar1" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClientClick="return validar();" OnClick="btnConfirmar1_Click" />
                                <asp:Button ID="btnConfirmar2" runat="server" Visible="false" Text="Aceptar 2***" CssClass="btn btn-danger" OnClientClick="return validar();" OnClick="btnConfirmar2_Click" />                                
                                <asp:button ID="btnCancelar1" runat="server" Text="Cancelar" Cssclass="btn btn-danger" OnClick="btnCancelar1_Click"/>
                            </td>
                        </tr>
                    </table>
					
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TarjCredito" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnConfirmar1"  />
                    <asp:PostBackTrigger ControlID="btnConfirmar2"  />
                </Triggers>
            </asp:UpdatePanel> 
        </div>


    <div class="container-fluid">
    
    <asp:Panel ID="panelRecibo" runat="server" Visible="false" Width="95%" EnableViewState="false">
        <ContentTemplate>
        <table class="table table-responsive">
            <tr>
                <td align="center">
                    <div id="encabezadoRecibo" class="container-fluid">
                        <div class="row">
                            
                        </div>
                        <div class="row">
                            <h4>
                                <asp:Label ID="lblTitlePages" runat="server"></asp:Label>
                            </h4>
                            <hr />
                            <div class="col-md-2 col-xs-2 pull-left">
                                <asp:Image ID="imagengrid" runat="server" ImageUrl="~/images/logobav2.png" Height="69px" />
                            </div>
                            <div class="col-md-6 col-xs-6 pull-right" style="text-align: right;margin-top: 20px;">
                                <asp:Label ID="lblNombreUsuarioRecibo" runat="server"></asp:Label><br />
                                <asp:Label ID="lblFechaRecibo" runat="server"></asp:Label>
                            </div>
                            <br />
                            <table class="table table-responsive">
                                <tr>
                                    <td align="center">
                                        <asp:Panel ID="PanelReferencia" runat="server">
                                            <table width="100%" style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                       <!-- <asp:Literal ID="liTextoFecha" runat="server" Text="Fecha:"></asp:Literal>
                                                        <b>
                                                            <asp:Literal ID="liFecha" runat="server"></asp:Literal></b>
                                                        -->
                                                    </td>
                                                    <td align="left" style="text-align: right">
                                                        <asp:Literal ID="liTextoReferencia" runat="server" Text="Número:"></asp:Literal>
                                                        <b>
                                                            <asp:Literal ID="liReferencia" runat="server"></asp:Literal></b></td>
                                                </tr>
                                                <tr>
                                                    <!--<td colspan="2">&nbsp;</td>-->
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <br />
                                        <table ID="dgDatosRecibo" runat="server" Width="100%"  CellPadding="4" CellSpacing="4" GridLines="None">
                                            
                                            <tr>
                                                <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liDebito" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValordebito" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liCredito" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorcredito" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liConcepto" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorConcepto" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liMonto" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValormonto" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liTotalcuotas" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValorcuotas" runat="server"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                 <td align="Left">
                                                    <b>
                                                        <asp:Literal ID="liMontocuota" runat="server"></asp:Literal></b></td>
                                                <td align="Right">
                                                    <b>
                                                        <asp:Literal ID="liValormontocuota" runat="server"></asp:Literal></b></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table class="tablatransaccion" width="100%" style="width: 100%" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td align="center">
                                                    <b>
                                                        <asp:Literal ID="liNota" runat="server" Visible="False"></asp:Literal></b></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Literal ID="liNota3" runat="server" Visible="False"></asp:Literal></td>
                                            </tr>
                                        </table>
                                        <br />                                       
                                        <br />
                                        <asp:Literal ID="liiFrame" runat="server" Visible="false"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table class="tablatransaccion" width="100%" style="width: 100%">
                                            <tr>
                                                <td class="td_ayudas" align="center">
                                                    <asp:Literal ID="liNota2" runat="server" Visible="False"></asp:Literal></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblResultado" runat="server" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
            </ContentTemplate> 
    </asp:Panel>
    <asp:Panel ID="panelBotonImpresion" runat="server" Width="95%" Visible="false">
        <table class="table table-responsive">
            <tr>
                <td align="center" colspan="2" style="height: 24px">
                    <asp:Button ID="btnImprimirRecibo" runat="server" Text="Imprimir Referencia" Width="200px" Visible="False" CssClass="btn btn-danger" OnClick="btnImprimirRecibo_Click" />&nbsp;
                    <asp:Button ID="btnRegresar2" runat="server" OnClick="btnRegresar2_Click" Text="Regresar" CssClass="btn btn-danger" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
</asp:Content>


