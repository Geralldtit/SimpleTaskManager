<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewPerson.aspx.cs" Inherits="TaskManager.Views.NewPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <p>&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="PersonIDLabel" runat="server" Text="PersonID" Width="150px"></asp:Label>
            <asp:Label ID="PersonIDField" runat="server" Text="" Width="150px"></asp:Label>
            <br />
            <asp:Label ID="PersonNameLabel" runat="server" Text="Person Name:" 
                Width="150px"></asp:Label>
            <asp:TextBox ID="PersonNameField" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="PersonNameField" 
                ErrorMessage="Поле Name не может быть пустым!">*</asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="PersonSonameLabel" runat="server" Text="Person Soname:" 
                Width="150px"></asp:Label>
            <asp:TextBox ID="PersonSonameField" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="PersonSonameField" 
                ErrorMessage="Поле Soname не может быть пустым!">*</asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="PersonSecNameLabel" runat="server" Text="Person SecondName:" 
                Width="150px"></asp:Label>
            <asp:TextBox ID="PersonSecNameField" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="PersonSecNameField" 
                ErrorMessage="Поле SecondName не может быть пустым!">*</asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="PersonPositionLabel" runat="server" Text="Person Position:" 
                Width="150px"></asp:Label>
            <asp:TextBox ID="PersonPositionField" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="PersonPositionField" 
                ErrorMessage="Поле Position не может быть пустым!">*</asp:RequiredFieldValidator>
            <br />
            <p>
                <asp:Button ID="SubmitPerson" runat="server" onclick="SubmitPerson_Click" 
                    Text="Submit" />
            </p>
        </p>
    
    
    
    </asp:Panel>
</asp:Content>
