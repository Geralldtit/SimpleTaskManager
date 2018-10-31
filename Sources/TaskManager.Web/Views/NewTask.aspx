<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="TaskManager.Web.Views.NewTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:Label ID="TaskIDLabel" runat="server" Text="Task ID:" Width="150px"></asp:Label>
        <asp:Label ID="TaskIDField" runat="server" Text="ID" Width="150px"></asp:Label>
        
        <br />
        <asp:Label ID="PrShortNameLabel" runat="server" Text="Проект:" Width="150px"></asp:Label>
        <asp:DropDownList ID="ProjectShortNameDrop" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Label ID="TaskNameLabel" runat="server" Text="Название:" Width="150px"></asp:Label>
        <asp:TextBox ID="TaskNameField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Необходимо указать название" 
            ControlToValidate="TaskNameField">*</asp:RequiredFieldValidator>
        <br />
        
        <asp:Label ID="TaskHoursLabel" runat="server" Text="Работа:" Width="150px"></asp:Label>
        <asp:TextBox ID="TaskHoursField" runat="server"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator4" runat="server" 
            ControlToValidate="TaskHoursField" ErrorMessage="Работу вводить в целых часах!" 
            Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
        <br />
        <asp:Label ID="TaskBeginDateLabel" runat="server" Text="Дата начала:" Width="150px"></asp:Label>
        <asp:TextBox ID="TaskBeginDateField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" 
            ErrorMessage="Необходимо указать дату начала" 
            ControlToValidate="TaskBeginDateField">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="TaskBeginDateField" 
            ErrorMessage="Неверный формат даты (гггг.мм.дд)" Operator="DataTypeCheck" 
            Type="Date"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ControlToCompare="TaskBeginDateField" ControlToValidate="TaskEndDateField" 
            ErrorMessage="Дата окончания не может быть раньше даты начала" 
            Operator="GreaterThanEqual"></asp:CompareValidator>
        <br />
        <asp:Label ID="TaskEndDateLabel" runat="server" Text="Дата окончания" Width="150px"></asp:Label>
        <asp:TextBox ID="TaskEndDateField" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="Необходимо указать дату окончания" 
            ControlToValidate="TaskEndDateField">*</asp:RequiredFieldValidator>

        <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ControlToValidate="TaskEndDateField" 
            ErrorMessage="Неверный формат даты (гггг.мм.дд)" Operator="DataTypeCheck" 
            Type="Date"></asp:CompareValidator>
        <br />
        <asp:Label ID="TaskStatusLabel" runat="server" Text="Статус" Width="150px"></asp:Label>
        <asp:DropDownList ID="TaskStatusDropFiled" runat="server" DataTextField="StatusTitle">
        </asp:DropDownList>
        <br />
        <asp:Label ID="TaskPersonLabel" runat="server" Text="Список исполнителей:" Width="150px"></asp:Label>
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="200" Width="400">
        
        <asp:CheckBoxList ID="PerformersList" runat="server" Height="10px">
        </asp:CheckBoxList>
        </asp:Panel>        
        <br />
        <p>
            <asp:Button ID="SubmitButton" runat="server" Text="Submit" 
                onclick="SubmitButton_Click" />
        </p>
    </asp:Panel>
</asp:Content>
