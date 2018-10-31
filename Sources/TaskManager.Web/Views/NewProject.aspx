<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewProject.aspx.cs" Inherits="TaskManager.Web.Views.NewProject" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TaskManager.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="ProjectLabel" runat="server" Text="Project ID:" Width="150px"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label ID="ProjectField" runat="server" Text="ID" Width="150px"></asp:Label>
        <br />
        <asp:Label ID="ProjectNameLabel" runat="server" Text="Название:" Width="150px"></asp:Label>
        <asp:TextBox ID="ProjectNameField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredNameFieldValidator" runat="server" 
            ControlToValidate="ProjectNameField" 
            ErrorMessage="Проект не может существовать без названия">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="ProjectShortNameLabel" runat="server" Text="Сокращенное название:" Width="150px"></asp:Label>
        <asp:TextBox ID="PrShortNameField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredPrShortNameFieldValidator" 
            runat="server" ControlToValidate="PrShortNameField" 
            ErrorMessage="Укажите сокращенное название">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="ProjectDescriptionLabel" runat="server" Text="Описание:" Width="150px"></asp:Label>
        <asp:TextBox ID="ProjectDescriptionField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredDescriptionValidator" runat="server" 
            ControlToValidate="ProjectDescriptionField" 
            ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="ProjectListLabel" runat="server" Text="Список задач проекта:" Width="150px"></asp:Label>
        <br />
        <asp:Panel ID="TasksPanel" runat="server">
            <asp:GridView ID="GridProjectTasks" runat="server" EnableModelValidation="True" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="TaskId" />
                    <asp:BoundField DataField="TaskName" />
                    <asp:BoundField DataField="BeginTime" />
                    <asp:BoundField DataField="EndTime" />
                    <asp:TemplateField>                    
                        <ItemTemplate>                     
                            <asp:BulletedList ID="bulTAskPersons" runat=server 
                                 DataTextField="Soname" DataSource="<%# ((Task)Container.DataItem).TaskPersons %>"></asp:BulletedList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Status" />
                    <asp:HyperLinkField DataNavigateUrlFields="TaskId,ProjectId" 
                        DataNavigateUrlFormatString="~/Views/NewTask.aspx?id={0}&amp;pr={1}" 
                        Text="Edit" />
                    <asp:HyperLinkField DataNavigateUrlFields="TaskId,ProjectID" 
                        DataNavigateUrlFormatString="~/Views/Tasks.aspx?del=1&amp;id={0}&amp;pr={1}" 
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
        <p>
        <asp:LinkButton ID="AddNewTaskLink" runat="server" 
            onclick="AddNewTaskLink_Click">Add new task >></asp:LinkButton>
            </p>        
</asp:Panel>
   <p> <asp:Button ID="SubmitButton" runat="server" Text="Сохранить" 
        onclick="SubmitButton_Click" />
        </p>
</asp:Content>
