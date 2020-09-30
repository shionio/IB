<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="ContratoExtraEfect.aspx.cs"
    Inherits="IBBAV.pages.Extraefectivo.ContratoExtraEfect" %>
<%@ Register Src="~/UserControls/PreguntasDesafio.ascx" TagPrefix="uc1" TagName="PreguntasDesafio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascrCaracas12
        ipt" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>    
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />    
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />    
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />    
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />    
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.js") %>"></script>    
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>    
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>

<!-- ************************************************************************************ <%--"~/pages/BAVMasterSimple.master"--%> -->
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <asp:Panel ID="panelValidacion" runat="server">
             <div class="container-fluid">
		<div class="well">
           Esta Opción le permitira solicitar adelanto de efectivo directo a sus cuentas BAV
		   </br>
		   Con la nueva linea ExtraEfectivo.
		   </br>
		   
        </div>        
        
            <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="PreguntasAfiliado" TipoRepeater="Table" MaskedInputs="true" />
            <center>
                <asp:Button id="btnAceptarPreguntas" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnAceptarPreguntas_Click" />
                <asp:Button id="btnCancelarPreguntas" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarPreguntas_Click" />
            </center>    
              </div>
        </asp:Panel>
  
    <asp:Panel ID="panelContrato" runat="server" UpdateMode="Conditional" Visible ="false">
        <style type="text/css">
        body {
            font-family: Trebuchet MS;
            margin: 0;
            padding: 0;
            font-size: 16px;
        }

        .hidden {
            visibility: hidden;
        }

        a {
            color: #990000;
            text-decoration: none;
        }

            a, a * {
                cursor: pointer;
                outline: none;
            }
    </style>

    <!DOCTYPE html>
        <html xmlns="http://www.w3.org/1999/xhtml">
            <body>   
            <div style="width: 100%; margin: 0 auto;">
   <div style="margin: 0 auto; text-align: center; ">
                <h2>
                    <b>
                        Extraefectivo - Banco Agr&iacute;cola de Venezuela, C.A. Banco Universal
                   </b>
                </h2>
            </div>
            <div id="container_terms" name="container_terms" style="height: 400px; width: 70%; overflow: auto; padding: 2%; margin: 0 auto; background-color: #D2D7D3; font-size:1em; text-align: justify;">
                <h3 style="text-align: center;">
                    <b>CONDICIONES GENERALES</b>
                </h3>
<br>
<strong>PRIMERA: DEFINICIONES</strong>
<p style="padding: -0.3% 2% 0.3% 3%">
<ol type="a">
    <li> <i>BANCO:</i>Se refiere al Banco Agrícola de Venezuela, C.A. Banco Universal.
    <li> <i>CLIENTE:</i> Se refiere a aquellas personas naturales que posean CUENTAS con el BANCO y sean titulares de una TARJETA DE CRÉDITO emitida por el BANCO.
    <li> <i>BANCO:</i> Cuenta Corriente, Cuenta Electrónica, señalada por el CLIENTE al momento de solicitar el <i>“EXTRAEFECTIVO”</i>, y en la cual será liquidado el monto solicitado en calidad de préstamo a interés.
    <li> <i>EXTRAEFECTIVO:</i> Es un servicio dirigido a los clientes que posean tarjetas de créditos del Banco Agrario de Venezuela, C.A., Banco Universal, con cargo al límite rotativo de las tarjetas de crédito, el cual permitirá disponer de dinero en efectivo que será abonado de forma inmediata en la cuenta corriente o cuenta electrónica del titular, sin afectar el límite ni saldo disponible de las tarjetas de crédito.
    <li> <i>TARJETA:</i>Serán las tarjetas de crédito emitidas por el BANCO.
</ol>
</p> 

<br><strong>SEGUNDA: DE LA SOLICITUD</strong>
<p style="padding: 0.3% 2% 0.3% 3%">EL CLIENTE podrá solicitar el <i>“EXTRAEFECTIVO”</i>, a través de www.bav.com.ve / <strong>BAV en Línea</strong>, accediendo a la opción <strong>“Persona Natural”</strong>. La aceptación será a través del BAV en Línea, del Contrato / Condiciones del servicio.</p>

<br><strong>TERCERA: DE LOS BENEFICIARIOS</strong>
<p style="padding: 0.3% 2% 0.3% 3%">Serán beneficiarios del <i>“EXTRAEFECTIVO”</i> todas las personas naturales titulares de tarjetas de crédito MASTERCARD emitidas, por EL BANCO que reúnan las particularidades y condiciones que se establecen en la Cláusula Cuarta.</p>

<br><strong>CUARTA: REQUISITOS Y RECAUDOS</strong>
<p style="padding: 0.3% 2% 0.3% 3%">Son requisitos fundamentales para optar al <i>“EXTRAEFECTIVO”</i> los siguientes:
<ul type="disc">
<li> Ser titular de una tarjeta de crédito emitida por EL BANCO, con una antigüedad mínima de seis (6) meses.
<li> El tarjetahabiente no debe presentar pagos vencidos durante los últimos seis (6) meses anteriores a la solicitud del <i>“EXTRAEFECTIVO”</i>
<li> Ser titular de una cuenta de depósito corriente electrónica en línea, así como todas las cuentas corrientes y/o cuentas de ahorro (nómina, persona natural, cuenta corriente remunerada y no remunerada), en EL BANCO, la cual debe estar afiliada a la tarjeta de débito (TDD).
<li> Contar con disponibilidad en su línea de crédito 2; y no presentar mora en los últimos seis (6) meses.
<li> Cumplir con las políticas crediticias de EL BANCO para su aprobación.
</ul>
</p>

<br><strong>QUINTA: MONTO</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El monto es hasta por el 100% del monto correspondiente al límite de la tarjeta de crédito.</p>

<br><strong>SEXTA: PLAZO</strong>
<p style="padding: 0.3% 2% 0.3% 3%">Financiamiento desde seis (6) hasta treinta y seis (36) meses para conveniencia de pago del cliente.</p>

<br><strong>SÉPTIMA: DE LA CANCELACIÓN DEL PRÉSTAMO</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El préstamo a que se refiere en estas Condiciones Generales, será cancelado mediante el pago de cuotas mensuales y consecutivas, las cuales contendrán <u>capital e intereses</u>. El BANCO ofrecerá a sus CLIENTES distintos plazos para cancelar el <i>“EXTRAEFECTIVO”</i> el cual estará comprendido entre<u> seis (6) hasta treinta y seis (36) meses</u>, el CLIENTE al momento de solicitar el préstamo a interés a que se refieren estas condiciones generales, señalará el plazo en el cual cancelará el préstamo.
<br /><br />
El monto de las cuotas mensuales correspondientes del <i>“EXTRAEFECTIVO”</i>, incluyendo tanto el capital como los intereses, serán cargados automáticamente en el pago mínimo mensual de la tarjeta de crédito y las mismas se verán reflejadas mensualmente en el estado de cuenta de la tarjeta de crédito. Los pagos podrán realizarse de forma parcial o total, es decir, el monto de la respectiva cuota, más los intereses que se generen, incluyendo los intereses moratorios de producirse, así como cualquier otro cargo o comisión convenida, la cual será debidamente informada al cliente en el momento de su solicitud.
<br /><br />
El CLIENTE conviene en que el vencimiento de cada cuota de pago del <i>“EXTRAEFECTIVO”</i> corresponderá a la fecha máxima de pago indicada según la facturación realizada por el BANCO para la respectiva TARJETA, a través de cuyo sistema cancelará el <i>“EXTRAEFECTIVO”</i>.
<br /><br />
Será en moneda de curso legal de Bolívares.
<br /><br />
La amortización de cuotas será a partir del primer mes de solicitar el Servicio.</p>

<br><strong>OCTAVA: DE LOS INTERESES Y TASAS</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El préstamo definido en estas Condiciones Generales, al tarjetahabiente del Banco Agrícola de Venezuela, C.A. Banco Universal (BAV) será según la tasa activa del mercado y con sujeción a lo establecido en las Resoluciones emitidas por el Banco Central de Venezuela (BCV), comunicadas en tabla de tasas del Banco.
<br /><br />
La tasa de interés aplicable al monto utilizado del <i>“EXTRAEFECTIVO”</i> devenga una tasa de interés del 40% anual, variable y revisable semestralmente, la cual será anunciada y publicada en la red de oficinas de EL BANCO y en su página web, todo esto de conformidad con lo estipulado en el artículo 78 del Decreto con Rango, Valor y Fuerza de Ley de Reforma Parcial de la Ley de las Instituciones del Sector Bancario. La consulta sobre los intereses, tarifas, comisiones y cargos  aplicados a este producto, podrá llevarlas a cabo EL CLIENTE a través de la página web de EL BANCO en las secciones Tasas de Interés y Tarifas y Comisiones. La tasa de interés está sujeta a cambios por decisión del Banco Central de Venezuela. En todo caso, al CLIENTE acepta que la tasa de interés resultante de cada revisión o modificación hecho por el BANCO, según lo antes establecido, se aplicará automáticamente al saldo deudor, por cuanto éste mantiene informado al CLIENTE mediante anuncios dispuestos en su sede principal, agencias, sucursales y página web, así como en cualquier otro medio de difusión, a través de cualquier otro medio de publicación de su elección.</p>

<br><strong>NOVENA: COMISIONES</strong>
<p style="padding: 0.3% 2% 0.3% 3%">Las comisiones relacionadas al <i>“EXTRAEFECTIVO”</i> por Internet es del cinco por ciento (5%) del monto solicitado.</p>

<br><strong>DÉCIMA: DE LA MORA</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El CLIENTE conviene y acepta que deberá mantener todas las tarjetas que posea con el BANCO solventes, en el caso que las cuotas de pago de préstamo definido en estas Condiciones Generales, no pudieron ser cargadas a la TARJETA por encontrarse ésta en situación de mora con las obligaciones derivadas de su uso normal, o por cualquier otro incumplimiento o hecho imputables al mismo, produciendo con ello el retardo en el pago de las obligaciones contraídas en virtud del <i>“EXTRAEFECTIVO”</i>, el BANCO tendrá derecho a cobrar de forma adicional a los intereses de financiamiento, tres puntos porcentuales (3%) anuales adicionales a la tasa de financiamiento, por concepto de mora, que el BANCO podrá ajustar de tiempo en tiempo a la máxima tasa anual legalmente permitida, mediante Resoluciones de su Junta Directiva y/o Comité creado al efecto, monto adicional que deberá el CLIENTE pagar desde el momento en que se produzca la mora hasta que tenga lugar el pago total de las obligaciones atrasadas o el pago definitivo del principal adeudado.</p>

<br><strong>DÉCIMA PRIMERA: AUTORIZACIÓN DE DÉBITO</strong> 
<p style="padding: 0.3% 2% 0.3% 3%">El CLIENTE autoriza expresa e irrevocablemente al BANCO para compensar el saldo insoluto del préstamo, el de sus intereses respectivos y moratorios, comisiones, así como los gastos de cobranza extrajudicial y/o judicial y honorarios de abogados llegado el caso, de la cuenta de depósito que autorizó el CLIENTE al momento de solicitar el producto, así como de cualquier colocación a la vista, a plazo que mantuviere en el BANCO, o en cualquiera de las instituciones que conforman su Grupo Financiero.
<br>
De no disponer de fondos suficientes en las CUENTAS, el CLIENTE se compromete a acudir, previo al vencimiento de la cuota del <i>“EXTRAEFECTIVO”</i>, a las oficinas del BANCO, a los fines de cancelar por anticipado la cuota mensual correspondiente o la totalidad del <i>“EXTRAEFECTIVO”</i>.</p>

<br><strong>DÉCIMA SEGUNDA: DEL VENCIMIENTO DEL TÉRMINO</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El CLIENTE conviene y acepta que la falta de cancelación de cuatro (4) cuotas mensuales consecutivas del <i>“EXTRAEFECTIVO”</i>, producirá de pleno derecho el vencimiento del término concedido para el pago de la obligación aquí documentada y, en consecuencia, el BANCO podrá exigir el pago inmediato del préstamo en la cuenta señalada por el CLIENTE al momento de solicitar el producto y autoriza por éste para debitar de dicha cuenta las cuotas vencidas, en los mismos términos establecidos en Cláusula Décima de estas Condiciones Generales, sin prejuicio del derecho que posee el CLIENTE de cancelar de manera anticipada el préstamo concedido por el BANCO.</p>

<br><strong>DÉCIMA TERCERA: DOMICILIO</strong>
<p style="padding: 0.3% 2% 0.3% 3%">El CLIENTE y el BANCO eligen como domicilio especial para todos los efectos y consecuencias de estas Condiciones Generales el domicilio del CLIENTE, señalado por éste, al momento de solicitar el producto <i>“EXTRAEFECTIVO”</i>, a la jurisdicción de cuyos Tribunales declaran someterse, asimismo, todas las controversias que se presenten con ocasión a la ejecución o incumplimiento del presente contrato, así como la interpretación y alcance de sus Cláusulas, serán  resueltas en los Tribunales de la República Bolivariana de Venezuela, conforme a la legislación venezolana que le sea aplicable de acuerdo a la materia, legislación que se aplicará de manera exclusiva y excluyente.</p>
</div>
               

            <div style="margin: 0 auto; text-align: center; padding-top: 20px;">
                <input name="afichk" id="afichk" type="checkbox" value="Submit" disabled="disabled" onclick="foo();">
                <b>Acepto lo indicado en la oferta p&uacute;blica</b>
            </div>

            <div style="margin: 0 auto; text-align: center; padding-top: 20px;">
                <asp:Button ID="contbtn" runat="server" Text="Continuar" CssClass="hidden" OnClick="contbtn_Click" />
                <asp:Button ID="cancelbtn" runat="server" Text="Cancelar" CssClass="Btn btn-danger" OnClick="cancelbtn_Click" />
            </div>
                    </div>                    

                <script type="text/javascript">
                    document.getElementsByName("container_terms")[0].addEventListener("scroll", checkScrollHeight, false);
                    function checkScrollHeight() {
                        var agreementTextElement = document.getElementsByName("container_terms")[0];
                        if (agreementTextElement.clientHeight + agreementTextElement.scrollTop >= agreementTextElement.scrollHeight) {
                            document.getElementsByName("afichk")[0].disabled = false;
                        }
                    }

                    var checkbox = document.getElementById("afichk");
                    function foo() {
                        if (checkbox.checked) {
                            $("input[id$=contbtn]").removeClass("hidden").addClass("Btn btn-danger");
                        }
                        else {
                            $("input[id$=contbtn]").removeClass("Btn").addClass("hidden");
                        }
                    };
                </script>
            </body>
        </html>
    </asp:Panel>        
</asp:Content>

