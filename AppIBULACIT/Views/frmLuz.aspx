<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmLuz.aspx.cs" Inherits="AppIBULACIT.Views.frmLuz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript">
        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }

        function openModal() {
            $('#myModal').modal('show'); //ventana de mensajes
        }

        function CloseModal() {
            $('#myModal').modal('hide');//cierra ventana de mensajes
        }

        </script>

    <h1><asp:Label Text="Mantenimiento de Pagos de Luz" runat="server"></asp:Label></h1>
    <asp:GridView ID="gvLuzFacturas" OnRowCommand="gvLuzFacturas_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
    <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
        <asp:BoundField HeaderText="CodigoUsuario" DataField="CodigoUsuario" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Estado" DataField="Estado" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Monto Energia" DataField="MontoEnergia" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Monto Variable" DataField="MontoVariable" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Monto Alumbrado" DataField="MontoAlumbrado" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:TemplateField HeaderText="Monto Total" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
            <ItemTemplate>
                <asp:Label ID="MontoTotal" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:ButtonField HeaderText="Pagar" CommandName="Pagar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Pagar" />
        <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
    </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />

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
                  <td><asp:Literal ID="ltrCuenta" Text="Cuenta" runat="server" Visible="false"/></td>
                  <td> <asp:DropDownList ID="CuentaDrop"  CssClass="form-control" runat="server" Visible="false">
                    <asp:ListItem Value="S">Seleccione cuenta</asp:ListItem>
                </asp:DropDownList></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrMontoEnergia" Text="Monto Energia" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoEnergia" runat="server" CssClass="form-control" /></td>
                  <td><asp:RegularExpressionValidator ID="revMontoEnergia" runat="server" ControlToValidate="txtMontoEnergia" ErrorMessage="Ingrese solo numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrMontoVariable" Text="Monto Variable" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoVariable" runat="server" CssClass="form-control" /></td>
                  <td><asp:RegularExpressionValidator ID="revMontoVariable" runat="server" ControlToValidate="txtMontoVariable" ErrorMessage="Ingrese solo numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrMontoAlumbrado" Text="Monto Alumbrado" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoAlumbrado" runat="server" CssClass="form-control" /></td>
                  <td><asp:RegularExpressionValidator ID="revMontoAlumbrado" runat="server" ControlToValidate="txtMontoAlumbrado" ErrorMessage="Ingrese solo numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrMontoTotal" Text="Monto Total" runat="server" Visible="false"/></td>
                  <td><asp:TextBox ID="txtMontoTotal" runat="server" CssClass="form-control" Visible="false"/></td>
              </tr>
          </table>
          
      </div>
      <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         
      </div>
    </div>
  </div>
</div>


       <!-- VENTANA MODAL ELIMINAR -->
  <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de Pago de Luz</h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
