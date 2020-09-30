<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="OtrosBancos.aspx.cs" Inherits="IBBAV.pages.Pagos.Tarjetas.OtrosBancos" %>

<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validar() {
		
			var i = $(".cta");
            i.parent().removeClass("has-error");
				// 0149 Bco del pueblo
            if (i.val() == "BANCO DEL PUEBLO SOBERANO, C.A. BANCO DE DESARROLLO") {				
                i.parent().addClass("has-error").focus();
                alert("El banco destino no acepta transferencias. Le invitamos actualizar sus favoritos");
                return false;
            }
			
            i = $(".ct");
            i.parent().removeClass("has-error");
            if (i.val() == "") {
                i.parent().addClass("has-error").focus();
                showAlert("Alerta", "Ingrese el Monto");
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
            if (i.length > 0)
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
    <div class="container-fluid">
        <asp:Panel ID="mensaje" runat="server">
            <div class="well">
            Recuerde:
                <br />	
                <br />			
			Si esta operación se realiza en días hábiles bancarios, antes de las 10:00 a.m. se enviará en el transcurso del mismo día.
	            <br />
            En caso de ser realizada después de las 10:00 a.m. se enviará el día hábil bancario siguiente.
	            <br />
                <br />
			BAV Banco Agrícola de Venezuela no se hace responsable por el tiempo que tarde el otro banco en procesar la transacción.
			    <br />		
                <br />	
			<asp:Literal ID="liDescription" runat="server" Text=""></asp:Literal>
                <br />
                <br />	
			<asp:Literal ID="liLimites" runat="server" Text=""></asp:Literal>
        </div>
        </asp:Panel>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server" Width="100%">
                            <table class="table table-responsive table-bav">
                                <tr>
                                    <td class="fieldTxt">Cuenta a Debitar:</td>
                                    <td class="fieldVal">
                                        <cc1:DropDownListCuentas ID="ddlCtaDebitar" runat="server" CssClass="form-control input-sm cd">
                                        </cc1:DropDownListCuentas>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Tarjeta a Pagar:
                                    </td>
                                    <td class="fieldVal">
                                        <cc1:DropDownListCuentas ID="ddlCtaAcreditar" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCtaAcreditar_SelectedIndexChanged" CssClass="form-control input-sm tca">
                                        </cc1:DropDownListCuentas>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr id="trTarjetaPagar" runat="server">
                                    <td class="fieldTxt">Número de Tarjeta:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtTarjetaPagar" runat="server" ReadOnly="true" EnableViewState="False" CssClass="form-control input-sm"></asp:TextBox>
                                    </td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Banco:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtBanco" runat="server" ReadOnly="True" Width="200px" CssClass="form-control input-sm cta"></asp:TextBox>
                                        <asp:TextBox ID="TxtCodeBanco" runat="server" Enabled="False" Visible="false" Width="63px" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Beneficiario:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtBeneficiario" runat="server" CssClass="form-control input-sm" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Cédula/Rif Beneficiario:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtCedulaBeneficiario" runat="server" ReadOnly="True" Width="120px" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt">Monto a Transferir:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control input-sm ct" Width="120px"></asp:TextBox></td>
                                    <td class="fieldInf"></td>
                                </tr>
                                <tr id="trConcepto" runat="server">
                                    <td class="fieldTxt">Concepto:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="form-control input-sm c" MaxLength="80" Width="400px"></asp:TextBox></td>
                                    <td class="fieldInf">&nbsp;</td>
                                </tr>
                                <tr id="trCorreo" runat="server">
                                    <td class="fieldTxt">Correo:</td>
                                    <td class="fieldVal">
                                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control input-sm em" MaxLength="50" Width="400px"></asp:TextBox></td>
                                    <td class="fieldInf">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="fieldTxt"></td>
                                    <td class="fieldVal">
                                        <%--******** COMISION *****************--%>
							            <div id="comision">
								            <asp:Literal ID="liComision" runat="server" Text="liComision"></asp:Literal>
                                        </div>
                                    </td>
                                    <td class="fieldInf">&nbsp;</td>
                                </tr>						                	
                                <tr align="center" >
							        <td colspan="3" class="buttons">
                                        <asp:Button ID="btnConfirmar" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClientClick="return validar();" OnClick="btnConfirmar_Click" />
                                        <asp:button id="btnCancelar" runat="server" Text="Cancelar" Cssclass="btn btn-danger" OnClick="btnCancelar_Click"/>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
            </ContentTemplate>
               <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCtaAcreditar" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnConfirmar" />
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
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn-danger" OnClick="btnCancelar_Click"/>
                            </div>
                        </td>                           
                    </tr>
                </table>					
                </ContentTemplate>  
        </asp:Panel>
    </div>
</asp:Content>
