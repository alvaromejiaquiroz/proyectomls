<%@ Page Language="C#" MasterPageFile="~/siteINIT.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login1t" Title="Antares" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    
    
    <table width="100%">
        
        <tr >
                <td style="height:100%;width:100%; vertical-align:middle; text-align:center">
                        <asp:Login ID="Login1" runat="server" 
                        BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid"
                        BorderWidth="1px" 
                        FailureText="No esta autorizado para ingresar" Font-Names="Verdana"
                        Font-Size="1em" 
                        InstructionText="Ingrese el usuario y contraseña"
                        LoginButtonText="Ingresar" 
                        LoginButtonType="button" 
                        OnAuthenticate="Login1_Authenticate"
                        PasswordLabelText="Contraseña:" 
                        PasswordRequiredErrorMessage="Ingrese su clave:"
                        RememberMeText="Recordar login" 
                        TitleText="Acceso restringido" 
                        UserNameLabelText="Usuario:"
                        UserNameRequiredErrorMessage="Necesita el nombre de usuario"  
                            Width="400px" 
                            BorderPadding="4" 
                            ForeColor="#333333"
                            >
                        <TitleTextStyle BackColor="#FFC515" Font-Bold="True" ForeColor="White" Font-Size="0.9em" />
                        <TextBoxStyle Font-Size="0.8em" />
                        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Verdana" Font-Size="10px" ForeColor="#284775" />
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        
                    </asp:Login>
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#E0E0E0" Width="312px"></asp:Label>
                 </td>
        </tr>
     </table>
</asp:Content>

