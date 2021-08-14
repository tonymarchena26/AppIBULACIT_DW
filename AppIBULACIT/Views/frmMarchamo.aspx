<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMarchamo.aspx.cs" Inherits="AppIBULACIT.Views.frmMarchamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=gvMarchamos]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                'aoColumnDefs': [{ 'bSortable': false, 'aTargets': [0] }],
                'iDisplayLength': 5,
                buttons: [
                    { extend: 'copy', text: 'Copy to clipboard', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'excel', text: 'Export to Excel', className: 'exportExcel', filename: 'Marchamo_Excel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'csv', text: 'Export to CSV', className: 'exportExcel', filename: 'Marchamo_Csv', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'pdf', text: 'Export to PDF', className: 'exportExcel', filename: 'Marchamo_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
                ]
            });
        });
    </script>

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
                  <td><asp:Literal ID="ltrMontoMarchamo" Text="Monto Marchamo" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoMarchamo" runat="server" CssClass="form-control" /></td>
                  <td><asp:RegularExpressionValidator ID="revMontoMarchamo" runat="server" ControlToValidate="txtMontoMarchamo" ErrorMessage="Ingrese solo numeros" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
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
        <h4 class="modal-title">Mantenimiento de Marchamos</h4>
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

    <div class="row">
        <div class="col-sm">
        <div id="canvas-holder" style="width:40%">
		            <canvas id="vistas-chart"></canvas>
	            </div>
                <script >
                    new Chart(document.getElementById("vistas-chart"), {
                        type: 'bar',
                        data: {
                            labels: [<%= this.labelsGraficoVistasGlobal %>],
                            datasets: [{
                                label: "Marchamos por monto",
                                backgroundColor: [<%= this.backgroundcolorsGraficoVistasGlobal %>],
                        data: [<%= this.dataGraficoVistasGlobal %>]
                            }]
                        },
                        options: {
                            title: {
                                display: true,
                                text: 'Marchamos por monto'
                            }
                        }
                    });
                </script>
            </div>
        </div>
    
</asp:Content>
    