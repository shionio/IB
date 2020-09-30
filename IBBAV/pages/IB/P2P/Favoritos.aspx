<%@ Page Title="" EnableEventValidation="false" ValidateRequest="false" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="IBBAV.pages.IB.P2P.Favoritos" %>
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
      
         <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-1.11.0.js") %>"></script>
      
  
       <script type="text/javascript" src="<%= ResolveUrl("~/js//bav/jquery.timers.js?t=636472282765927653") %>"></script>


	<script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>">
	<script type="text/javascript" src="<%= ResolveUrl("~/js/bav/SessionBAVIframe.js") %>">
	
	
	
	</script>
	
	
	
	
    </script>
    
    
    

	
	
    
    
    <script>





        function onMyFrameLoad() {
            var url = document.URL;
            var split1 = url.split("?")[1];
            var split2 = split1.split("=")[1];
            var split3 = split2.split("&")[0];



           
            
            
            
            
            
            
            
            
            $(document).stopTime();

            cargarTimers();
            reiniciarTimer();
            //debugger;

            $.ajax({
               
                type: "POST",
                url: "/IB/ws/Data.asmx/ActualizarSesion",
                contentType: "application/json; charset=utf-8",
                data: "{ \"sesion\" : \"" + split3 + "\" }",
                dataType: "json",
               
                success: function(res) {
                     //alert(JSON.stringify(res));
                    res = res.d;
                    if (res.Resultado == "OK") {

                       // alert("resulto fue ok");
                    }

                },
              
                error: function(res) {

                    //that.trigger('load-error', res.status);
                },
                complete: function() {
                 

                }
            }); 
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         
         

        };
     
        
        
        
        
        
 function reiniciarTimer()
{
    $(document).stopTime();
    cargarTimers();
}

        
        
 
function cargarTimers()
{    

	var timeSession = 150000;

	$(document).oneTime(timeSession, function() {  
        //my_window = window.open(urlMensaje,"InactividadIBBAV","status=1,width=450,height=380");
        //my_window.focus();        
        $('#myModalLogout').modal('show');
            $(document).oneTime(timeSessionClose, function () {
            //window.location.href = urlSalir;            
            window.parent.location.href = urlSalir;
        });
    });
    


}
        

    
  
  
        
    </script>
  
  
   
    
    <style>
        .gi-2x{font-size: 1em;}
        .gi-3x{font-size: 2em;}
        .gi-4x{font-size: 3em;}
        .gi-5x{font-size: 4em;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div >

<iframe  name="myframe" id="myframe"  src=""  
      onload="onMyFrameLoad(this);menuLinkChanger();"  scrolling="no" frameborder="0" width="100%" height="800px" allowfullscreen style="overflow:auto"></iframe>



      <script>

          var url = document.URL;
          var split1 = url.split("?")[1];
          var split2 = split1.split("=")[1];
          var split3 = split2.split("&")[0]
          document.getElementById("myframe").src = "https://afiliacion.bav.com.ve/P2PAF/listFavorites.zul?a=" + split3
    
   
    </script>

      
     <script>
         var varBoolLoad = true;
    </script>
 
       
        <asp:Panel ID="panelDatos" runat="server" Visible="false">
            <table id="tabla" class="table-striped" data-height="299" data-cache="false" ">
                <thead>
                    <tr>
                        <th class="col-md-1" data-field="dCompDate" data-sortable="true" data-align="center" >Fecha Afiliacion</th>
                        <th class="col-md-2" data-field="TipoDescripcion" data-sortable="true" data-align="center">Tipo</th>
                        <th class="col-md-2" data-field="NumeroInstrumento" data-sortable="true" data-align="center">Numero Cuenta/Servicios</th>
                        <th class="col-md-4" data-field="Beneficiario" data-sortable="true" data-align="left">Beneficiario</th>
                        <th class="col-md-2" data-field="Descripcion" data-sortable="true" data-align="left">Descripcion</th>
                        <th class="col-md-1" data-formatter="operateFormatter" data-events="operateEvents" data-align="center"></th>
                    </tr>
                </thead>
            </table>
            <center>
                <asp:LinkButton ID="LinkButton1"  runat="server" CssClass="btn btn-danger" OnClick="btnNew_Click"    >
                    <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>Agregar
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalir"  runat="server" CssClass="btn btn-danger" OnClick="btnSalir_Click"     >
                    Salir
                </asp:LinkButton>
        
            </center>
            <script>


                $(function () {


                    loadData();
                    $(window).resize(function () {
                        $('#tabla').bootstrapTable('resetView');
                    });



                    function loadData() {
                        $('#tabla').bootstrapTable({
                            query: [{
                                sesion: '<%= base.SessionId %>'
                            }],
                            method: "post",
                            url: '<%= ResolveUrl("~/ws/Data.asmx/MenuFavoritos") %>',
                        });
                    }

                    function updateData() {

                        $('#tabla').bootstrapTable('refresh', {
                            query: [{
                                sesion: '<%= base.SessionId %>'
                            }],
                            method: "post",
                            url: '<%= ResolveUrl("~/ws/Data.asmx/MenuFavoritos") %>'

                        });
                    }
                });
                function dateFormatter(value) {
                    return moment(value).format("DD/MM/YYYY");

                }
                function operateFormatter(value, row, index) {
                    return [
                        '<a class="modify ml10" href="javascript:void(0)" title="Modificar">',
                            '<i class="glyphicon glyphicon-pencil gi-2x"></i>',
                        '</a>&nbsp;',
                        '<a class="remove ml10" href="javascript:void(0)" title="Eliminar">',
                            '<i class="glyphicon glyphicon-remove gi-2x"></i>',
                        '</a>'
                    ].join('');
                }
                window.operateEvents = {
                    'click .modify': function (e, value, row, index) {
                        if (confirm("¿Desea Modificar este Favorito?")) {
                            delete row["__type"];
                            $("#<%= hData.ClientID%>").val(JSON.stringify(row));
                            <%= ClientScript.GetPostBackEventReference(btnModificar,null) %>;
                            return;
                        }
                    },
                    'click .remove': function (e, value, row, index) {
                        if (confirm("¿Desea Eliminar este Favorito?")) {
                            delete row["__type"];
                            $("#<%= hData.ClientID%>").val(JSON.stringify(row));
                            <%= ClientScript.GetPostBackEventReference(btnEliminar,null) %>;
                            return;
                        }
                    }
                };


                
            </script>
            
        </asp:Panel>
        <asp:HiddenField ID="hData" runat="server" />
        <asp:Button ID="btnModificar" runat="server" Visible="false" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Visible="false" OnClick="btnEliminar_Click" />
    </div>
    
</asp:Content>
