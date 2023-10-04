<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DiscosLista.aspx.cs" Inherits="App_Discos_Web.DiscosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de Discos</h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label ID="lblFiltrar" runat="server" Text="Filtrar por Titulos:"></asp:Label>
                <asp:TextBox ID="txtFiltro" OnTextChanged="txtFiltro_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado"
                    CssClass="" ID="chkAvanzado" runat="server"
                    AutoPostBack="true" 
                    OnCheckedChanged="chkAvanzado_CheckedChanged"  /> <%--Evento--%>           
            </div>
        </div>

        <%if (chkAvanzado.Checked)
            { %>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged"> <%--Evento--%> 
                        <asp:ListItem Text="Titulo" />
                        <asp:ListItem Text="Genero" />
                        <asp:ListItem Text="Cantidad de Canciones" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Filtro" runat="server" />
                    <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Estado" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                        <asp:ListItem Text="Todos" />
                        <asp:ListItem Text="Activo" />
                        <asp:ListItem Text="Inactivo" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" /> <%--Evento--%>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <asp:GridView ID="dgvDiscos" runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false"
        OnPageIndexChanging="dgvDiscos_PageIndexChanging"
        OnSelectedIndexChanged="dgvDiscos_SelectedIndexChanged"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
            <asp:BoundField HeaderText="Cantidad de Canciones" DataField="CantidadCanciones" />
            <asp:BoundField HeaderText="Genero" DataField="Genero.Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>
    <a href="FormularioDisco.aspx" class="btn btn-primary">Agregar</a>

</asp:Content>

