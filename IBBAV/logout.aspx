<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="IBBAV.logout" %>

<html>
<head>
    <title>Sesión BAV Finalizada</title>
    <link href="<%= ResolveUrl("~/bootstrap/css/bootstrap.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/bootstrap/css/bootstrap-theme.css") %>" rel="stylesheet" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/css/main.aspx") %>">
    <link rel="shortcut icon" href="<%= ResolveUrl("~/images/fav/icon.png")%>" type="image/x-icon" />
    <style>
        BODY
        {
            position: relative;
            height: 100%;
			font-family: 'MyriadWebPro' sans-serif;
            font-size: 12pt;
            margin: 0;
            padding: 0;
        }
        #divOut {
        width:400px; 
        height:305px; 
        position: fixed; 
        top: 50%; 
        left: 50%; 
        margin-top: -200px; 
        margin-left: -200px;
        }
    </style>

	
	<script>	
		<!--
		alert('EPA');

				debugger;
				
				$.ajax({
							debugger;
						   url: "http://172.18.52.142:8080/ZK700App6/logout.zul",
						   context: document.body
						 }).done(function() {
						 alert('Hola');
						   console.log("Executado");
						 });
		-->
	</script>
	
	
</head>
<body>
    <form runat="server" id="outform" method="post">
        <div id="divOut">
            
            <div style="margin: 0 auto; width: 400px; border: solid 1px #95a5a6; background-color: #ecf0f1; border-radius:135px;padding: 75px; text-align: center;">
                <p>Su sesión con</p>

                <div style="margin: 0 auto; text-align: center; width: 335px;">
                    <img src="images/sessionbav.png" class="img-responsive" />
                </div>
                <p></p>
                <p>ha finalizado.</p>
                <p>Gracias por preferirnos.</p>

                <p>
                    <b>Fecha de logout:
            <asp:Literal ID="liFecha" runat="server"></asp:Literal></b>
                </p>
                <br />
                <asp:Button ID="btnVolver" runat="server" Text="Salir" CssClass="btn-danger" OnClick="btnBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>
