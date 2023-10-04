<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="App_Discos_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hola!</h1>
    <p>Esta es tu coleccion de Discos Musicales</p>

    <%--<div class="row row-cols-1 row-cols-md-3 g-4">
        <%  // C# Abre
            foreach (dominio.Disco disco in ListaDisco)             // Repite la cantidad de Discos que tenga DB.
            {
        %>  
                <div class="col">
                    <div class="card">
                        <img src="<%: disco.UrlImagen %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%: disco.Titulo %></h5>
                            <p class="card-text"><%: disco.CantidadCanciones %></p>
                            <a href="DetalleDisco.aspx?=id<%: disco.Id %>">Ver Detalle</a> 
                        </div>
                    </div>
                </div> 
        <%  // C# Cierra
            }
        %>         
    </div>--%>

    <%--Ejemplo con Repeater:--%>
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Titulo")%></h5>
                            <p class="card-text"><%#Eval("CantidadCanciones")%> Canciones</p>                            
                            <a href="DetalleDisco.aspx?=id<%#Eval("Id")%>" class="btn btn-primary">Ver Detalle</a>                            
                            <asp:Button ID="btnEjemplo" Text="Ejemplo" CssClass="btn btn-primary" CommandArgument='<%#Eval("Id") %>' CommandName="DiscoId" OnClick="btnEjemplo_Click" runat="server"/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
