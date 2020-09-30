<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionOut.aspx.cs" Inherits="IBBAV.pages.SessionOut"  %>
<!DOCTYPE html>
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="cache-control" content="no-store" />
<meta http-equiv="Window-target" content="_top" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap/css/bootstrap.min.css")%>" />
<link rel="stylesheet" href="<%= ResolveUrl("~/css/main.aspx") %>"/>
    <title>Sesión Inactiva</title>
    <style>
        BODY
        {
            position: relative;
            height: 100%;
			font-family: 'MyriadWebPro' sans-serif;
            font-size: 10pt;
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body onunload="window.opener.reiniciarTimer();">
    <form id="form1" runat="server">
    <div style="padding-top:15px;">
        <div style="width:350px; text-align:center;margin:0 auto; border:solid 1px #95a5a6; background-color:#ecf0f1; border-top-left-radius: 30px; border-top-right-radius: 30px; padding:30px;">
            <img src="<%= ResolveUrl("~/images/sessionbav.png")%>" class="img-responsive" />
            <p>no ha detectado actividad en los ultimos minutos</p>   
            <p>Por su seguridad su sesión finalizará en 30 segundos si no hay actividad.</p> 
            <p><b>Presione "Extender" para volver a la pantalla anterior.</b></p>
            <input type="button" value="Extender" id="sessbtn" class="btn btn-danger" onclick="window.close();" />
        </div>
    </div>
    </form>
</body>
</html>

