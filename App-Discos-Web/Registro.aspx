<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="App_Discos_Web.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-4">
            <h2>Creá tu perfil Usuario</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtEmail"/>
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" TextMode="Password"/>
            </div>
            <asp:Button Text="Registrarse" cssclass="btn btn-primary" OnClick="btnRegistrarse_Click" ID="btnRegistrarse" runat="server" />
            <a href="/" class="btn btn-primary">Cancelar</a>
        </div>
    </div>
</asp:Content>
