<%@ Page Language="C#" MasterPageFile="~/siteINIT.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login_Login1t" Title="Antares" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <div style="text-align:center;width:100%">
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid"
            BorderWidth="1px" FailureText="No esta autorizado para ingresar." Font-Names="Verdana"
            Font-Size="10px" InstructionText="Ingrese el usuario y contrase�a" LoginButtonText="Ingresar"
            LoginButtonType="button" OnAuthenticate="Login1_Authenticate" PasswordLabelText="Contrase�a"
            PasswordRequiredErrorMessage="Ingrese la contrase�a." RememberMeText="Recordar login"
            TitleText="Acceso restringido" UserNameLabelText="Usuario" UserNameRequiredErrorMessage="Ingrese el usuario."
            Width="400px" BorderPadding="4" ForeColor="#333333">
            <TitleTextStyle CssClass="header_custom" />
            <TextBoxStyle CssClass="text_custom" />
            <LoginButtonStyle CssClass="button_custom" />
        </asp:Login>
    </div>
    <div style="text-align:center;width:100%">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="#E0E0E0"></asp:Label>
    </div>
</asp:Content>
