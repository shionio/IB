<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="EnBAV.aspx.cs" Inherits="IBBAV.pages.Tarjetas.EnBAV" %>

<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function validar() {
            var i = $(".cd .ca .cca .tca .m .c .em");
            i.parent().removeClass("has-error");

            i = $(".cd");
            if (i.length > 0)
            if (i.val() == "0") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Debe indicar la cuenta a Debitar");
                return false;
            }

            i = $(".tca");
            if (i.length > 0)
            if (i.val() == "") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Debe indicar la tarjeta de crédito");
                return false;
            }

            if ($(".cd").val() == $(".ca").val()) {
                $(".cd").parent().addClass("has-error");
                $(".ca").parent().addClass("has-error");
                $(".cd").focus();
                showAlert("Alerta", "La cuenta a debitar y la cuenta a acreditar son iguales");
                return false;
            }

            i = $(".m");
            if (i.length > 0)
            if (i.val() == "") {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "Debe indicar el monto de la transferencia");
                return false;
            }

            i = $(".c");
            if (i.length > 0)
            if (i.val().length > 80) {
                i.parent().addClass("has-error");
                i.focus();
                showAlert("Alerta", "La longitud del Concepto no puede exceder los 80 caracteres.");
                return false;
            }

            i = $(".em");
            if(i.length>0)
            if (i.val() != "") {
                if (!validateEmail(i.val())) {
                    i.parent().addClass("has-error");
                    i.focus();
                    showAlert("Alerta", "El correo electrónico no es válido");
                    return false;
                }
            }
            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:Panel ID="mensaje" runat="server">
            <div class="well">
                Recuerda:
    			    </br>
	    		    </br>
			    Esta operación se realiza en línea y será acredita en el transcurso del día.
    			    <br/>						
			    BAV Banco Agrícola de Venezuela se complace en servirle con eficiencia y seguridad.
                    </br>
			        </br>
			    <asp:Label ID="lblDescription" runat="server" Text="lblDescription"></asp:Label>
                    </br>
			        </br>
			    <asp:Label ID="lblLimites" runat="server" Text="lblLimites"></asp:Label>
            </div>
        </asp:Panel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server" Width="100%">
                            <table class="table table-responsive table-bav">
                                <tr>
                                    <td class="fieldTxt">Cuenta a Debitar:</td>
                                    <td class="fieldVal">
                                        <cc1:DropDownListCuentas ID="ddlCtaDebitar" runat="server" CssClass="form-control cd">
                                        </cc1:DropDownListCuentas>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">
                                        <asp:Literal ID="liDescripcionTarjeta" runat="server" Text="Tarjeta a Pagar:"></asp:Literal>
                                    </td>
                                    <td class="fieldVal">
                                        <cc1:DropDownListCuentas ID="ddlCtaAcreditar" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCtaAcreditar_SelectedIndexChanged" CssClass="form-control tca">
                                        </cc1:DropDownListCuentas>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr id="trFechaPago" runat="server" style="height: 40px">
                                    <td class="fieldTxt">Fecha de Pago:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtFechaPago" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        
                                        <asp:UpdateProgress ID="UPFechaPago" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelTipoPago" runat="server" Enabled="false" Width="100%">
                           
                            <table class="table table-responsive table-bav">
                                <tr>
                                    <td class="fieldTxt"><b>Seleccione el tipo de pago</b></td>
                                    <td class="fieldVal">
                                         
                                    </td>
                                        <td class="fieldTxt"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">
                                        <asp:RadioButton ID="rb_actual" runat="server" CssClass="td_align1" GroupName="RadioGroup" Text="Saldo Actual" />
                                    </td>
                                    <td class="fieldVal">
                                        <asp:Literal ID="liSaldoActual" runat="server"></asp:Literal>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">
                                        <asp:RadioButton ID="rb_minimo" runat="server" CssClass="td_align1" GroupName="RadioGroup" Text="Pago Mínimo" />
                                    </td>
                                    <td class="fieldVal">
                                        <asp:Literal ID="liPagoMinimo" runat="server"></asp:Literal>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">
                                        <asp:RadioButton ID="rb_otro" runat="server" CssClass="td_align1" GroupName="RadioGroup" Text="Otro Monto" />
                                    </td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtOtroMonto" runat="server" MaxLength="80" Text="0,00" Width="120px"></asp:TextBox>
                                        Coloca una coma para indicar decimales.
                                                                                                <br />
                                        Ej. 300568,87</td>
                                    <td class="fieldInf"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelTerceros" runat="server" Width="100%">
                            
                            <table class="table table-responsive table-bav">
                                <tr>
                                    <td class="fieldTxt">Tarjeta a Pagar:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtTarjetaPagar" runat="server" CssClass="form-control" ReadOnly="true" Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Monto a Transferir:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control m" Width="120px"></asp:TextBox>Coloca una coma para indicar decimales. Ej. 300568,87
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Concepto:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="form-control" MaxLength="80" Width="400px"></asp:TextBox>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Correo:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control" MaxLength="50" Width="400px"></asp:TextBox>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>								
                            </table>
                        </asp:Panel>

                        <center>
						<!--  ******** COMISION ***************** -->
								<div id="comision">
									Nota: El pago de tarjetas de crédito del Banco Agrícola de Venezuela, no genera comisión.
                                        <%--<asp:Literal ID="liComision" runat="server" Text="liComision"></asp:Literal>--%>
								</div>                            
                            <asp:Button ID="BtAceptar" runat="server" Text="Aceptar" OnClick="Aceptar_Click" CssClass="btn btn-danger" /></td>
                            <asp:Button ID="BtCancelar" runat="server" OnClick="BtCancelar_Click" Text="Cancelar" CssClass="btn btn-danger" /></td>
                        </center>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtAceptar" />
                        <asp:PostBackTrigger ControlID="BtCancelar" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCtaAcreditar" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCtaDebitar" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

       <asp:Panel ID="pnlRestriccion" runat="server" Visible="false">
            <ContentTemplate>
                <table id="Table1" runat="server" class="table table-responsive table-bav-mensajes">
                    <tr>
                        <td class="fieldTxt">
                            <p style="font-weight:lighter">
                                Dando cumplimiento a la <strong>Circular N° SIB-DSB-14539</strong>, emanada de la <strong>SUDEBAN</strong> de fecha 27 de agosto de 2018, 
                                según lo establecido en el artículo 171 numeral 26 del <strong>Decreto con Rango, Valor y Fuerza de Ley de 
                                Instituciones del Sector Bancario</strong>, en concordancia con el artículo 172 numeral 7 de la misma ley, se establece
                                la Protección Integral de los Clientes y Usuarios del Sistema Bancario Nacional que realicen operaciones 
                                mediante la Banca a Distancia o Banca por Internet fuera de la República Bolivariana de Venezuela.
                            </p>                                
                            <p style="font-weight:lighter">
                                Si te encuentras en Venezuela, te recomendamos validar con tu proveedor de Internet la dirección de IP asignada 
                                a tu conexión, ya que la regulación* establece que el acceso a la Banca por Internet debe hacerse a través de 
                                empresas que cuenten con direcciones IP del Registro Regional de Internet para América Latina y el Caribe; y 
                                hayan recibido de CONATEL la habilitación administrativa, concesión y permiso correspondiente.
                             </p>
                            <p style="font-weight:lighter">
                                Para liberar las opciones de transferencias y pagos a terceros debes notificar tu permanencia fuera del país,indicando el lugar o lugares
                                de destino y el período en que permanecerás fuera del país. La cantidad de destinos no debe ser mayor a 3 países diferentes y el período no 
                                puede exceder los 6 meses continuos. Esto garantizará la seguridad y protección a tus transacciones.
                            </p>
                            <p style="font-style: italic; text-align:center;font-family: 'Verdana'">Circular N° SIB-DSB-UNIF-17799 emitida por la SUDEBAN con fecha 02 de noviembre de 2018.</p>                           
                            <div style ="text-align:center;">
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn-danger" OnClick="BtCancelar_Click"/>
                            </div>
                        </td>                           
                    </tr>
                </table>					
                </ContentTemplate>  
        </asp:Panel>
    </div>
</asp:Content>
