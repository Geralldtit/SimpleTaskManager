<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Persons.aspx.cs" Inherits="TaskManager.Web.Views.Persons" %>
<%@ Import Namespace="TaskManager.Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelForPersons" runat=server>
        <asp:GridView ID="GridPersons" runat="server" EnableModelValidation="True" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="PersonID" ReadOnly="True"
                    SortExpression="PersonID" />
                <asp:BoundField DataField="Soname" ReadOnly="True" />
                <asp:BoundField DataField="Name" ReadOnly="True" />
                <asp:BoundField DataField="SecondName" ReadOnly="True" />
                <asp:BoundField DataField="Position" ReadOnly="True" />
                <asp:HyperLinkField DataNavigateUrlFields="PersonID" Text="Edit " 
                    DataNavigateUrlFormatString="~/Views/NewPerson.aspx?id={0}" />
                <asp:HyperLinkField DataNavigateUrlFields="PersonID" 
                    DataNavigateUrlFormatString="~/Views/Persons.aspx?del=1&amp;id={0}" 
                    Text=" Delete" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>    
    </asp:Panel>  

    <p>
            <asp:LinkButton ID="lnkCreatePerson" runat="server" onclick="LnkCreatePersonClick">Create New Person >></asp:LinkButton>       
    </p>
    
    
</asp:Content>
