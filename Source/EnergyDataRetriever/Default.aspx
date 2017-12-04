<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnergyDataRetriever._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="TimerUpdateLabel" runat="server" Interval="1000" OnTick="TimerUpdateLabel_Tick">
        </asp:Timer>

        <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TimerUpdateLabel" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                Today Energy Yield:
        <asp:Label ID="LabelEnergyToday" runat="server" Text="N/A"></asp:Label>
                &nbsp;kWh<br />
                Total Yield:
        <asp:Label ID="LabelEnergyTotal" runat="server" Text="N/A"></asp:Label>
                &nbsp;kWh<br />
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid">
                    Today Income:
            <asp:Label ID="LabelIncomeToday" runat="server" Text="N/A"></asp:Label>
                    &nbsp;SEK<br />
                    System Status:
            <asp:Label ID="LabelStatus" runat="server" Text="N/A"></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
