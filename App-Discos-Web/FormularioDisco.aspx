<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioDisco.aspx.cs" Inherits="App_Discos_Web.FormularioDisco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Script para usar UpdatePanel--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtTitulo" class="form-label">Titulo: </label>
                <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha: </label>
                <asp:TextBox runat="server" ID="txtFecha" TextMode="Date" CssClass="form-control" />
            </div>          
            <div class="mb-3">
                <label for="ddlGenero" class="form-label">Genero: </label>
                <asp:DropDownList ID="ddlGenero" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlEdicion" class="form-label">Tipo de Edicion:</label>
                <asp:DropDownList ID="ddlEdicion" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
                <a class="btn btn-primary" href="DiscosLista.aspx">Cancelar</a>
                <asp:Button ID="btnInactivar" OnClick="btnInactivar_Click" CssClass="btn btn-warning" runat="server" Text="Inactivar" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" runat="server" />              
                    </div>
                    <%if (ConfirmaEliminacion){%>
                        <div class="mb-3">             
                            <asp:CheckBox Text="Confirmar Eliminación" ID="chkConfirmaEliminacion" runat="server" />
                            <asp:Button Text="Eliminar" ID="btnConfirmaEliminar" OnClick="btnConfirmaEliminar_Click" CssClass="btn btn-outline-danger" runat="server" />              
                        </div>                         
                    <%}%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="col-6">
             <div class="mb-3">
                <label for="txtCantidadCanciones" class="form-label">Cantidad Canciones: </label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtCantidadCanciones" CssClass="form-control" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtUrlImagen" class="form-label">Url Imagen</label>
                        <asp:TextBox runat="server" ID="txtUrlImagen" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" />
                    </div>
                    <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                        runat="server" ID="imgDisco" Width="60%" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
