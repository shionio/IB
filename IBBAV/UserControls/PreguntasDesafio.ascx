<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreguntasDesafio.ascx.cs" Inherits="IBBAV.UserControls.PreguntasDesafio" %>

    <asp:Repeater ID="rptPreguntas" runat="server" OnItemDataBound="rptPreguntas_ItemDataBound">
        <ItemTemplate>
            <div class="form-inline">
                <label for="ddlPreguntas" class="col-sm-4 control-label">Pregunta Nro. <asp:Literal ID="liNro" runat="server" ></asp:Literal></label>
                <div class="col-sm-7">
                    <asp:Literal ID="liId" runat="server" Visible="false"></asp:Literal>  
                    <asp:Literal ID="liPregunta" runat="server"></asp:Literal>   
                    <asp:DropDownList ID="ddlPreguntas" runat="server" Visible="false" CssClass="form-control" style="min-width: 250px; width: 350px;"></asp:DropDownList>
                </div>
            </div>
            <div class="form-inline">
                <label for="txtRespuesta" class="col-sm-4 control-label">Respuesta:</label>
                <div class="col-sm-7">
                    <asp:TextBox ID="txtRespuesta" runat="server" CssClass="form-control" style="min-width: 250px; width: 350px;"></asp:TextBox>
                </div>
            </div>            
        </ItemTemplate>
    </asp:Repeater>

    <asp:Repeater ID="rptPreguntas2" runat="server" OnItemDataBound="rptPreguntas_ItemDataBound">
        <HeaderTemplate>
            <table class="table table-responsive table-bav">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="fieldTxt">Pregunta Nro. <asp:Literal ID="liNro" runat="server" ></asp:Literal></td>
                <td class="fieldVal"><asp:Literal ID="liId" runat="server" Visible="false"></asp:Literal>  
                    <asp:Literal ID="liPregunta" runat="server"></asp:Literal>   
                    <asp:DropDownList ID="ddlPreguntas" runat="server" Visible="false" CssClass="form-control"></asp:DropDownList></td>
                <td class="fieldInf"></td>
            </tr>
            <tr>
                <td class="fieldTxt"></td>
                <td class="fieldVal"><asp:TextBox ID="txtRespuesta" runat="server" CssClass="form-control"></asp:TextBox></td>
                <td class="fieldInf"></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>


