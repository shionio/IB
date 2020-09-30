<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BAVMenu.ascx.cs" Inherits="IBBAV.UserControls.Menu.BAVMenu" %>
<div id="gw-sidebar" class="gw-sidebar">
    <div class="nano-content">
        <ul class="gw-nav gw-nav-list">
            <li class="init-un-active active">
                <a href="pages/consolidada.aspx" onclick="goPage(this, event)">
                    <span class="gw-menu-text"><i class="glyphicon glyphicon-home" style="top: -2px !important; margin-right:5px"></i>Posicion Consolidada</span>
                </a>
            </li>
            <asp:Repeater ID="rptMenuLvl1" runat="server" OnItemDataBound="rptMenuLvl1_ItemDataBound" >
                <ItemTemplate>
                    
                        <li id="liChilds"  runat="server"  visible="false" class="init-arrow-down">
                            <a href="javascript:void(0)"><span class="gw-menu-text"><%# Eval("Nombre") %></span> <b class="gw-arrow"></b></a>
                            <ul class="gw-submenu">
                                <asp:Repeater ID="rptMenuLvl2" runat="server" OnItemDataBound="rptMenuLvl2_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Literal ID="liItem" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                        <asp:Literal ID="liItem" runat="server"></asp:Literal>
                            
                    
                </ItemTemplate>
            </asp:Repeater>
            <li>
                <a id="logout" href="javascript:void(0);">
                    <span class="gw-menu-text"><i class="glyphicon glyphicon-log-out" style="top: -2px !important; margin-right:5px"></i>Salir</span>
                </a>
            </li>
        </ul>
        <script>

            function goPage(x, e) {
                if(e)
                    e.preventDefault();
                
                var op = $(x);
                console.log(op);
                $("#loadImg").show();
                var iframe = $("#iContent");
                iframe.hide();
                /*if (navigator.appName == 'Microsoft Internet Explorer') {
                    iframe.document.execCommand('Stop');
                } else {
                    iframe.stop();
                }*/iframe.attr('src', 'about:blank');
                iframe.attr("src", op.attr("href"));
                console.log(iframe.attr("src"));
            }

            $(document).ready(function () {
                $("#logout").on("click", function (e) {

                window.parent.location.href = "<%= ResolveClientUrl("~/logout.aspx")%>";
                e.preventDefault();
            });
        });
    </script>
    </div>
</div>

