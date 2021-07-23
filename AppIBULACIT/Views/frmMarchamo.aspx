<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMarchamo.aspx.cs" Inherits="AppIBULACIT.Views.frmMarchamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <script type="text/javascript">
        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }
    </script>

    <h1><asp:Label Text="Mantenimiento de marchamos" runat="server"></asp:Label></h1>
    <asp:GridView ID="gvMarchamos" OnRowCommand="gvMarchamos_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
    <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Estado" DataField="Estado" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="MontoMarchamo" DataField="MontoMarchamo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="CodigoUsuario" DataField="CodigoUsuario" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:ButtonField HeaderText="Pagar" CommandName="Pagar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Pagar" />
        <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
    </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />


    <!--VENTANA DE MANTENIMIENTO -->
  <div id="myModalMantenimiento" class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><asp:Literal ID="ltrTituloMantenimiento" runat="server"></asp:Literal></h4>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblCodigo" ForeColor="Maroon" Visible="True" runat="server" Text=""/>
          <table style="width: 100%;">
              <tr>
                  <td><asp:Literal ID="ltrDescripcion" Text="Descripcion" runat="server" /></td>
                  <td><asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal Text="Estado" runat="server" /></td>
                  <td> <asp:DropDownList ID="ddlEstadoMant"  CssClass="form-control" runat="server">
                    <asp:ListItem Value="N">No Pagado</asp:ListItem>
                    <asp:ListItem Value="P">Pagado</asp:ListItem>
                </asp:DropDownList></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrMontoMarchamo" Text="MontoMarchamo" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoMarchamo" runat="server" CssClass="form-control" /></td>
              </tr>
          </table>
          <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
      </div>
      <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         
      </div>
    </div>
  </div>
</div>


</asp:Content>
    