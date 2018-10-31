<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TaskManager.Web.Views.Tasks" %>
<%@ Import Namespace="TaskManager.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:Panel ID="Panel1" runat="server">
    <asp:GridView ID="GridTasks" runat="server" EnableModelValidation="True" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="TaskId" />
                    <asp:BoundField DataField="TaskName" />
                    <asp:BoundField DataField="Hours" />
                    <asp:BoundField DataField="BeginTime" />
                    <asp:BoundField DataField="EndTime" />
                    <asp:TemplateField>                    
                        <ItemTemplate>                     
                            <asp:BulletedList ID="bulTAskPersons" runat=server 
                                 DataTextField="Soname" DataSource="<%# ((Task)Container.DataItem).TaskPersons %>"></asp:BulletedList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Status" />
                    <asp:HyperLinkField DataNavigateUrlFields="TaskId" 
                        DataNavigateUrlFormatString="~/Views/NewTask.aspx?id={0}" 
                        Text="Edit" />
                    <asp:HyperLinkField DataNavigateUrlFields="TaskId" 
                        DataNavigateUrlFormatString="~/Views/Tasks.aspx?del=1&amp;id={0}" 
                        Text="Delete" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
    </asp:Panel>

    <p><asp:LinkButton ID="CreateTaskLink" runat="server" 
            onclick="CreateTaskLink_Click">Create New Task >></asp:LinkButton></p>
    
</asp:Content>
