﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="EnBAV.aspx.cs" Inherits="IBBAV.pages.Transferencias.EnBAV" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function validar() {
            
        }
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        
        <asp:Panel ID="mensaje" runat="server">
            <div class="well" >
                Recuerda:
			        <br />
                    <br/>	
			    Esta operación se realiza en línea y será acredita a la cuenta destino en el transcurso del día.
                    <br />            
			    BAV Banco Agrícola de Venezuela se complace en servirle con eficiencia y seguridad.
                    <br/>	
                    <br/>	
                <asp:Label ID="lblDescription" runat="server" Text="lblDescription" ></asp:Label>
                    <br />
                    <br/>
                <asp:Label ID="lblLimites" runat="server" Text="lblLimites"></asp:Label>
            </div>
        </asp:Panel>
    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="tblClaves" runat="server" class="table table-responsive table-bav">
                        <tr>
                            <td class="fieldTxt">Cuenta a Debitar:</td>
                            <td class="fieldVal"><cc1:DropDownListCuentas ID="CtaDebitar" runat="server" CssClass="form-control cd"> </cc1:DropDownListCuentas></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr>
                            <td class="fieldTxt"><asp:Literal ID="liCuentaAcreditar" runat="server" Text="Cuenta a Acreditar:"></asp:Literal></td>
                            <td class="fieldVal"><cc1:DropDownListCuentas ID="CtaAcreditar" runat="server" OnSelectedIndexChanged="CtaAcreditar_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control cca"> </cc1:DropDownListCuentas></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr id="trCuentaAdreditar" runat="server">
                            <td class="fieldTxt">Cuenta a Acreditar:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtCuentaAcreditar" runat="server" CssClass="form-control tca" ReadOnly="true" Width="160px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr>
                            <td class="fieldTxt">Monto a Transferir:</td>
                            <td class="fieldVal"><asp:TextBox ID="txtMonto" runat="server" CssClass="form-control m" Width="120px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr id="trConcepto" runat="server">
                            <td class="fieldTxt"> Concepto:</td>
                            <td class="fieldVal"> <asp:TextBox ID="txtConcepto" runat="server" CssClass="form-control c" MaxLength="80" Width="400px"></asp:TextBox></td>
                            <td class="fieldInf"></td>
                        </tr>
                        <tr id="trCorreo" runat="server">
                            <td class="fieldTxt"> Correo:</td>
                            <td class="fieldVal"> 
								<asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control em" MaxLength="50" Width="400px"></asp:TextBox>
							</td>
                            <td class="fieldInf"></td>
                        </tr>
						<tr align="center" >
							<td colspan = "3">
						<!--  ******** COMISION ***************** -->
								<div id="comision">
									Nota: La transferencia entre cuentas del Banco Agrícola de Venezuela, no genera comisión.
									<!--<asp:Literal ID="liComision" runat="server" Text="liComision"></asp:Literal>--></span>
								</div>
							</td>
						</tr>
                        <tr>							
						    <td colspan="3" class="buttons">
                                <asp:Button ID="btnConfirmar" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClientClick="return validar();" OnClick="btnConfirmar_Click" />
                                <asp:button id="btnCancelar" runat="server" Text="Cancelar" Cssclass="btn btn-danger" OnClick="btnCancelar_Click"/>
                            </td>
                        </tr>
                    </table>
					
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="CtaDebitar" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnConfirmar"  />
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
