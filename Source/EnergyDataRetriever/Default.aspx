﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnergyDataRetriever._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        Energy:
        <asp:Label ID="LabelEnergyToday" runat="server" Text="N/A"></asp:Label>
        &nbsp;kWh<br />
        Total:
        <asp:Label ID="LabelEnergyTotal" runat="server" Text="N/A"></asp:Label>
&nbsp;kWh<br />
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" >
            Income:
            <asp:Label ID="LabelIncomeToday" runat="server" Text="N/A"></asp:Label>
            &nbsp;SEK<br />
            System Status:
            <asp:Label ID="LabelStatus" runat="server" Text="N/A"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>
