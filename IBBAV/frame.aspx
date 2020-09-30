<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frame.aspx.cs" Inherits="IBBAV.frame" %>

<%@ Register Src="~/UserControls/Menu/BAVMenu.ascx" TagName="BAVMenu" TagPrefix="uc1" %>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title></title>

    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <link rel="shortcut icon" href="<%= ResolveUrl("~/images/fav/icon.png")%>" type="image/x-icon" />
    <link rel="stylesheet" href="css/normalize.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <link rel="stylesheet" href="css/glyphicons.css">
    <link rel="stylesheet" href="css/glyphiconshalflings.min.css">

        
    <script type="text/javascript" src="<%= ResolveUrl("~/js/prefixfree-1.0.7.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-1.11.0.js")%>"></script>
    <link rel="stylesheet" type="text/css" href="accordion/css/bs_leftnavi.css">
    <script type="text/javascript" src="accordion/js/bs_leftnavi.js"></script>
    <!--[if lt IE 9]>
		<script type="text/javascript" src="js/json3.min.js"></script>
		<script type="text/javascript" src="js/IE9.min.js"></script>
		<link rel="stylesheet" href="css/ie.css">
		<script type="text/javascript" src="js/html5.js"></script>
		<script type="text/javascript" src="js/ie.js"></script>
		<![endif]-->

    <script src="js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <link rel="stylesheet" href="js/vendor/twitter-bootstrap/css/bootstrap.css">
    <link rel="stylesheet" href="js/vendor/twitter-bootstrap/css/bootstrap-theme.css">
    <script src="js/vendor/twitter-bootstrap/js/bootstrap.js"></script>

    <!--[if IE 9]>
            <link rel="stylesheet" href="css/IE9.css">
            <script type="text/JavaScript" src="js/html5shiv.js"></script>
        <![endif]-->
    <!--[if lte IE 8]>
            <link rel="stylesheet" href="css/IE8.css">
            <script type="text/JavaScript" src="js/html5shiv.js"></script>
        <![endif]-->
    <style type="text/css" media="print">
        #headerBanner, #side {
            display: none;
        }

        #iContent {
            border: none;
        }

        .footer {
            position: fixed;
            bottom: 0;
            margin: 0 auto;
            display: block;
        }

        .page-break {
            page-break-before: always;
        }

        }
    </style>
    <style type="text/css" media="screen">
        HTML {
            height: 100%;
        }

        BODY {
            position: relative;
            height: 100%;
            font-family: Trebuchet MS;
            font-size: 9pt;
        }

        HEADER {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 110px;
            padding-left: 5px;
        }

        #side {
            position: absolute;
            top: 110px;
            left: 0;
            bottom: 0;
            /*width: 20%;*/
            width: 270px;
            overflow: auto;
            margin-bottom: 40px;
        }

        #content {
            position: absolute;
            top: 110px;
            /*left: 20%;*/
            left: 270px;
            bottom: 40px;
            right: 0;
            overflow: hidden;
             padding-top:5px;
              
        }

        #iContent {
            border: none;
        }

        .footer {
            position: absolute;
            bottom: 0;
            width: 100%;
            height: 27px;
            background-color: #336633;
            margin: 0;
            z-index: 999;
        }

        .container .text-muted {
            margin: 5px 0;
            text-align: center;
        }

        .form-control {
            font-size: 9pt;
        }
        body {
            overflow-x:hidden;
        }

        /* scroll fixes */
        .modal-open .modal {
          padding-left: 0px !important;
          padding-right: 0px !important;
          overflow-y: scroll;
        }
        
        .gi-2x{font-size: 1em;}
        .gi-3x{font-size: 2em;}
        .gi-4x{font-size: 3em;}
        .gi-5x{font-size: 4em;}
    
    </style>
    <script>
        //<![CDATA[
        var urlMensaje = "<%= ResolveUrl("~/pages/SessionOut.aspx")%>";
        var timeSessionMessage = 150000;

        var urlSalir = "<%= ResolveUrl("~/logout.aspx")%>";
        var timeSessionClose = 30000;
        var sesionVal = "<%=  Session["SessionId"] %>";
        var urlUpdate = "<%= ResolveUrl("~/ws/Data.asmx")%>/ActualizarSesion";
        //]]>
        $(document).ready(function () {
            $("#logout").on("click", function (e) {

                window.parent.location.href = "<%= ResolveClientUrl("~/logout.aspx")%>";
                e.preventDefault();
            });

            /* center modal */
            function centerModals() {
                $('.modal').each(function (i) {
                    var $clone = $(this).clone().css('display', 'block').appendTo('body');
                    var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
                    top = top > 0 ? top : 0;
                    $clone.remove();
                    $(this).find('.modal-content').css("margin-top", top);
                });
            }
            $('.modal').on('show.bs.modal', centerModals);
            $(window).on('resize', centerModals);
        });
        function reloadIframe() {
            $('#loadImg').hide();
            $('#iContent').show();
            reiniciarTimer();
        }

        $(document).ready(function () {
            $(document).on('focus', ':input', function () {
                $(this).attr('autocomplete', 'off');
            });
        });

    </script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/bav/jquery.timers.js") + "?t=" + DateTime.Now.Ticks %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/bav/SessionBAV.js") + "?t=" + DateTime.Now.Ticks %>"></script>

</head>
<body>
    <form id="form1" runat="server">


        <!--[if lte IE 7]>
                <p class="browsehappy">Usted esta utilizando un navegador <strong>desactualizado</strong>. Le sugerimos <a href="http://browsehappy.com/">actualizar su navegador</a> para mejorar su experiencia.</p>
            <![endif]-->
        <!--[if lt IE 8]>
			    <link href="css/bootstrap-ie7.css" rel="stylesheet">
		    <![endif]-->

        <header id="headerBanner" style="margin:0; padding:0; border-bottom:solid 1px #f9f9f9;">
            <div style="width:100%">
                <img src="images/header_ib.png" />
            </div>
        </header>
        
        
        <div id="side">
            <uc1:BAVMenu ID="BAVMenu1" runat="server" />
        </div>
        
        <div id="content">
            <style>
                #loadImg{position:absolute;z-index:999;height:100%}
                
                .center {
                    display:table-cell;
                    vertical-align:middle;
                    float:none;
                    width:100%;
                    text-align:center;
                }

            </style>
            <div class="row-fluid" style="height: 100%;">
                <div id="loadImg" class="center">
                    <div style="width:100%; text-align:center">
                        <img src="<%= ResolveUrl("~/images/loading.gif") %>" />
                    </div>
                </div>
				<!-- a  id="enTT" href="/IB/?logout">. </a --> 
                <iframe id="iContent" name="iContent" src="pages/consolidada.aspx" style="width: 100%; height: 100%; display: block;" frameborder="0" onload="reloadIframe()"></iframe>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="myModalLogout" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Inactividad en Sesión detectada</h4>
                        </div>
                        <div class="modal-body">
                            <i class="glyphicon glyphicon-time gi-4x" style="color:#336633"></i>
                            En treinta(30) segundos su sesión va a finalizar por falta de actividad.
                        </div>
                        <div class="modal-footer">
                            <button id="btnRefresh" type="button" class="btn btn-danger">
							    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span> Extender
						    </button>
						</div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="footer">
            <div class="container">
                <p class="text-muted">© Copyright 2015 / Banco Agrícola de Venezuela / RIF: G-20005795-5</p>
            </div>
        </footer>
    </form>
</body>
</html>
