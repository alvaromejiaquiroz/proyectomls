<%@ Control Language="C#"  AutoEventWireup="true"  CodeFile="FechayHora.ascx.cs" Inherits="Controles_FechayHora" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
        <asp:Panel ID="pnlCombos" runat="server" Visible="True"  >
              
                        Fecha:  
                        <uc1:jDatePick ID="Cal1" runat="server"  />
                                <asp:DropDownList ID="cmbhora" runat="server"  > </asp:DropDownList>
                                <asp:TextBox ID="txtMin" runat="server" width="30px"  ></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMin" Display="None"  ErrorMessage=" Inserte Minutos del 0 al 59" MaximumValue="59" MinimumValue="0" SetFocusOnError="True"></asp:RangeValidator>
                                <cc1:ValidatorCalloutExtender ID="Range_V_Min_E" runat="server" TargetControlID="RangeValidator1" >  </cc1:ValidatorCalloutExtender> 
                                <asp:RequiredFieldValidator ID="Rq_V_Min" runat="server"  ControlToValidate="txtMin" Display="None" ErrorMessage="Ingrese los minutos"> </asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="Rq_V_Min_E" runat="server"  TargetControlID="Rq_V_Min"> </cc1:ValidatorCalloutExtender>
              
                                
                      
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>