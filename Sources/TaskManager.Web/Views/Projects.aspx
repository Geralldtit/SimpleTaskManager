<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Projects.aspx.cs" Inherits="TaskManager.Web.Views.Projects"%>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
      
    <asp:Panel ID="Panel1" runat="server">
        <asp:GridView ID="GridProjects" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProjectID" />
                <asp:BoundField DataField="PrName" />
                <asp:BoundField DataField="PrShortName" />
                <asp:BoundField DataField="Description" />
                <asp:HyperLinkField DataNavigateUrlFields="ProjectID" 
                    DataNavigateUrlFormatString="~/Views/NewProject.aspx?id={0}" Text="Edit" />
                <asp:HyperLinkField DataNavigateUrlFields="ProjectID" 
                    DataNavigateUrlFormatString="~/Views/Projects.aspx?del=1&amp;id={0}" 
                    Text="Delte" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>
    <p><asp:LinkButton ID="CreateNewProjectLink" runat="server" 
            onclick="CreateNewProjectLink_Click">Create New Project >></asp:LinkButton></p>

</asp:Content>
