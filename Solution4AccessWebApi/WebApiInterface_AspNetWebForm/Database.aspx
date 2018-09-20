<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Database.aspx.cs" Inherits="WebApiInterface_AspNetWebForm.Database" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="Content/MyFormStyle.css" />

    <div>
        <p style="font-weight: 700">
            <asp:Button ID="BtnAskCreate" runat="server" Text="新增" CommandName="AskCreate" Class="message-btn trigger" OnClick="BtnAskCreate_Click" />
            <asp:Button ID="BtnAskUpdate" runat="server" Text="更新" CommandName="AskUpdate" OnClick="BtnAskUpdate_Click" />
            <asp:Button ID="BtnFinishDelete" runat="server" Text="刪除" CommandName="FinishDelete" OnClick="BtnFinishDelete_Click" />
            <asp:Button ID="BtnAskSearch" runat="server" Text="搜尋" CommandName="AskSearch" ClientIDMode="Static" OnClick="BtnAskSearch_Click" />
            <asp:Button ID="BtnCancel" runat="server" Text="取消" CommandName="Cancel" OnClick="BtnCancel_Click" />
        </p>
        <asp:GridView ID="GridView1" runat="server"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCreated="GridView1_RowCreated">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <%-- 
                            <asp:TemplateField HeaderText="選取">
                            <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" />
                            </ItemTemplate>
                            </asp:TemplateField>
                --%>
            </Columns>
        </asp:GridView>
        <asp:Panel ID="ClientCreateUserControl" runat="server">
            <div class="modal-overlay">
                <div class="modal">
                </div>
                <div class="modal-area">
                    <h1>This is Create User Control.</h1>
                    <asp:Button ID="BtnFinishCreate" runat="server" CommandName="FinishCreate" OnClick="BtnFinishCreate_Click" Text="新增" />
                    <asp:Panel ID="ClientCreateUserControlArea" runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="ClientUpdateUserControl" runat="server" OnInit="ClientUpdateUserControl_Init">
            <div class="modal-overlay"></div>
            <div class="modal">
                <div class="modal-area">
                    <h1>This is Update User Control.</h1>
                    <asp:Button ID="BtnFinishUpdate" runat="server" CommandName="FinishUpdate" OnClick="BtnFinishUpdate_Click" Text="更新" />
                    <asp:Panel ID="ClientUpdateUserControlArea" runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="ClientSearchUserControl" runat="server" OnInit="ClientSearchUserControl_Init">
            <div class="modal-overlay"></div>
            <div class="modal">
                <div class="modal-area">
                    <h1>This is Search User Control.</h1>
                    <asp:Button ID="BtnFinishSearch" runat="server" CommandName="FinishSearch" OnClick="BtnFinishSearch_Click" Text="搜尋" />
                    <asp:Panel ID="ClientSearchUserControlArea" runat="server" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
