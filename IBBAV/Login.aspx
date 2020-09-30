<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMasterSimple.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IBBAV.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%=ResolveUrl("~/css/jquery.reject.css")%>">
    <link href="<%= ResolveUrl("~/css/jquery-bav-keyboard.css")  + "?t=" + DateTime.Now.Ticks %>" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveUrl("~/js/jquery-bav-keyboard.js")  + "?t=" + DateTime.Now.Ticks %>" ></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/js/vendor/twitter-bootstrap/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/js/bav/jquery.reject.js")%>"></script>
    <script>
        if (window.frameElement) {
            window.parent.location = "<%= ResolveUrl("~/Login.aspx")%>";
        }
        else {
            // not in frame
        }

 $(document).ready(function () {
            $('#infoModal').modal('show');
            $('.jkeyboard').jkeyboard({
                //keys: ['jamies', 'custom', 'keys']
                target: '#keyboard',
                inputId: ['<%= txtAF_Password.ClientID %>']
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



            $("form").addClass("form-signin").on("submit", function (e) {
                if (validatefields() == false) e.preventDefault();
            });

            //explorers
            //$.reject();
            //return false;
            $.reject({
                reject: { safari: true, unknown: true, konqueror: true },
                    display: ['firefox', 'chrome', 'opera', 'msie'],
                    header: 'Explorador no soportado', // Header Text
                    paragraph1: 'Estas utilizando un explorador no soportado o desactualizado', // Paragraph 1
                    paragraph2: 'Te invitamos a instalar las últimas versiones de los siguientes exploradores web',
                    close:false
                   }); // Customized Text
                return false;
            });
        

        function validatefields() {
            var log = $('#<%= AF_Login.ClientID %>').val();
            var pass = $('#<%= txtAF_Password.ClientID %>').val();

            if (log == "") {
                alert("Debe indicar su usuario de conexión");
                $('#<%= AF_Login.ClientID %>').focus();
                  return false;
              }
              else if (pass == "") {
                  alert("Debe indicar su clave de acceso");
                  $('#<%= txtAF_Password.ClientID %>').focus();
                return false;
            }
        }    

    </script>
    <style>
       .modal-body {
            position: relative;
            max-height: 600px;
            padding: 15px;
            overflow-y: auto;
         
        }

        .modal-dialog {
               width:800px;
        }

        .modal-dialog h4 {
                background-color:#336633; 
                color:#FFF; 
                padding:2px 10px 2px 10px;
                font-size:18px; 
                text-align:justify; 
                border-radius: 4px;
        }
                
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="liScript" runat="server"></asp:Literal>
    <noscript>Your browser does not support JavaScript!</noscript>
	<div style="width: 398px; height: 55px; background-image: url(images/bavenlineapersonanatural.png); background-repeat: no-repeat; margin: 0 auto; padding-top: 20px;"></div>
    <!--<h4>Bienvenido a BAV En L&iacute;nea</h4>-->
    <hr style="border-top: 2px solid #545454;" />
    <div class="col-md-1"></div>
    <div class="col-md-5">
        <div style="display: block; float: left; width: 50%; text-align: center;">
            <div style="width: 298px; height: 400px; background-image: url(images/infologin.png); background-repeat: no-repeat; margin: 0 auto; padding-top: 20px;">
                <div style="padding-top: 80px;">
                    <table border="0" align="center" style="width: 280px;">
                        <tr>
                            <td>
                                <img src="images/secureicon.png" /></td>
                            <td><b><a href="http://www.bav.com.ve/index.php/tips-de-seguridad/" target="_blank" title="Acerca de la Seguridad">Acerca de la Seguridad</a></b></td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/qicon.png" /></td>
                            <td><b><a title="Preguntas Frecuentes" data-toggle="modal" data-target="#myModal">Preguntas Frecuentes</a></b></td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/telfico.png" /></td>
                            <td>Números de Contacto</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>0800-228.00.00<br />
                                0500-228.00.01</td>
                        </tr>

                        <tr>
                            <td>
                                <img src="images/arrobaico.png" /></td>
                            <td><b><a href="mailto:atencionalcliente@bav.com.ve" title="Correo Electrónico">atencionalcliente@bav.com.ve</a></b></td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div runat="server" id="divmsg" style="display: block; text-align: left; color: red;" visible="false">
            <div class="alert alert-danger" role="alert">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span><asp:Literal ID="sError" runat="server" Visible="False"></asp:Literal>
            </div>
        </div>

        <div style="text-align: left;">Inicie Sesi&oacute;n o <b><a href="pages/IB/AfiliacionIB/terms_conditions.aspx" title="¿Nuevo usuario?">Af&iacute;liese</a></b></div>
        <br />
        <div class="form-group" style="text-align: left;">
            <label for="AF_Login">Usuario:</label><br />
            <asp:TextBox runat="server" ID="AF_Login" Width="300" placeholder="Indique su usuario" MaxLength="10" autofocus onkeydown="if(event.ctrlKey && event.keyCode==86){return false;}" autocomplete="off"></asp:TextBox>
            <span class="info" title="Su usuario de conexión a BAV en línea">(?)</span>
        </div>

        <div class="form-group" style="text-align: left;">
            <label for="txtAF_Password">Clave de Acceso:</label><br />
            <asp:TextBox runat="server" ID="txtAF_Password" Width="300" placeholder="Indique su clave" TextMode="Password" ToolTip="ingrese su clave" MaxLength="10" CssClass="jkeyboard"></asp:TextBox>
            <span class="info" title="Su clave de acceso a BAV en línea">(?)</span>
        </div>
        <div class="form-group" style="text-align: left;display:inline-block">
            <div id="keyboard" style="padding: 0px 0px 20px 0px;display:inline-block"></div>
        </div>
        <div class="form-inline">
            <div class="form-group">
                <label for="btnlogin">
                    <asp:linkbutton ID="lnkRecPass" runat="server" CommandArgument="¿Has olvidado tu contraseña?" OnClick="lnkRecPass_Click">¿Has olvidado tu contrase&ntilde;a?</asp:LinkButton>
                <div style="float:right; padding-left:50px;"><asp:Button runat="server" ID="btnlogin" Text="Iniciar sesión" ToolTip="Iniciar sesión" CssClass="btn" OnClick="btnlogin_Click" OnClientClick="if(validatefields()==false) return (false);" /></div>
            </div>
        </div>
    </div>

<!-- Modal -->
<div class="modal fade" id="infoModal" tabindex="-1" role="dialog" aria-labelledby="infoModalLabel" aria-hidden="true">
	<div style=" z-index: -1; width: 525px; height: 545px; background-image: url(images/PopUp.png); background-repeat: no-repeat;  margin-top:125px; margin-left:500px;">
		<!--<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
					</button>
                 
				</div>
				<div class="modal-body" >     
					<img src="images/PopUp.png"/>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
				</div>
			</div>
		</div> -->
	</div>
</div>

<div class="modal fade bs-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h3 class="modal-title" id="myModalLabel">Preguntas Frecuentes</h3>
      </div>
      <div class="modal-body">
        
              <h4>¿Cu&aacute;les son los requisitos para acceder al servicio de Banca en L&iacute;nea BAV?</h4>
                <ul>
                <li>Ser Persona Natural</li>
	            <li>Poseer tarjeta de d&eacute;bito con su clave de cajero autom&aacute;tico</li>
        	    <li>Realizar la afiliaci&oacute;n en l&iacute;nea, a través del link "Af&iacute;liese"</li>  
                </ul>
              <h4>¿Cu&aacute;les son los navegadores soportados?</h4>
              <p>BAV en l&iacute;nea soporta los navegadores Internet Explorer 9.0 o superior, Firefox y Google Chrome. Para obtener informaci&oacute;n respecto a la versi&oacute;n utilizada por usted seleccione del menu principal de su navegador la opci&oacute;n Ayuda, luego la opción <b>"Acerca de ..."</b></p>

              <h4>¿Cu&aacute;les operaciones se pueden realizar a trav&eacute;s del servicio Banca en L&iacute;nea BAV?</h4>
              <b>Consultas</b>
                <ul>
            	<li>Saldos y movimientos en l&iacute;nea de cuentas</li>
	            <li>Saldo de su Cr&eacute;dito</li>
                </ul>
            
            <b>Transferencias</b>
        	    <ul>
                <li>Entre cuentas propias</li>
    	        <li>A terceros en el Banco Agr&iacute;cola de Venezuela</li>
        	    </ul>
            
            <b>Pagos</b>
	        <ul>      
                <li>Tarjetas Mismo Titular</li>
            </ul>
            
          <b>Otros</b>
				<ul>
                <li>Personalizar Cuenta</li>
				<!--<li>Histórico de Transacciones</li>-->
				<li>Cambio de Clave</li>
				<li>Notificaci&oacute;n Correo</li>
				<li>Notificaci&oacute;n SMS</li>
                </ul>

              <h4>¿Qu&eacute; debe hacer si olvid&oacute; su usuario o clave de acceso?</h4>
              <p>En caso de que usted no recuerde su usuario o contrase&ntilde;a presione click en el link <b>¿Has olvidado tu contrase&ntilde;a?</b></p>

              
              <h4>¿Si tengo problemas con el acceso al servicio BAV en L&iacute;nea, ¿a qui&eacute;n puedo llamar?</h4>
              
              <p>El Banco Agr&iacute;cola de Venezuela, le ofrece el centro de atenci&oacute;n telef&oacute;nica <b>0800-2280000 / 0500-2280001</b>, donde usted ser&aacute; atendido por nuestros especialistas, o por el correo electr&oacute;nico: <a href="mailto:atencionalcliente@bav.com.ve"> atencionalcliente@bav.com.ve</a></p>    

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div> 
</div>
    <footer style="margin: 0 auto; max-width: 1024px; min-width: 200px; clear: both; text-align: center; bottom: 0; padding-top: 25px;">
        <div style="margin: 0 auto; text-align: center; max-width: 1024px; min-width: 200px;">
            <div style="border-radius: 5px; background: #336633; padding: 3px; max-width: 1024px; min-width: 200px; height: 30px;">
                <p style="font-size: 14px; color: white;">&#169; Copyright 2015 Banco Agr&iacute;cola de Venezuela. RIF G-20005795-5</p>
            </div>
        </div>
    </footer>
</asp:Content>
