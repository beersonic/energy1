<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnergyDataRetriever._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width-device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous" />

    <link href="Layout.css" rel="stylesheet" type="text/css" />

    <link href="Layout.css" rel="stylesheet" type="text/css" />

    <title></title>
</head>
<body style="background-color: antiquewhite">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="TimerUpdateLabel" runat="server" Interval="1000" OnTick="TimerUpdateLabel_Tick">
        </asp:Timer>

        <div class="container-fluid">
            <div id="banner" style="text-align: center">
                <h1>Energy Viewer</h1>
            </div>
            <div id="project">
                <asp:DropDownList ID="DropDownListProject" runat="server" OnSelectedIndexChanged="DropDownListProject_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TimerUpdateLabel" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <div id="labelGroup">
                        <div id="RowTodayYield" class="row" style="text-align: center">
                            Today Energy Yield:&nbsp;	
        <asp:Label ID="LabelEnergyToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh
                        </div>
                        <div id="RowTotalYield" class="row" style="text-align: center">
                            Total Yield:&nbsp;	
        <asp:Label ID="LabelEnergyTotal" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh<br />
                        </div>
                        <div id="RowTodayIncome" class="row" style="text-align: center">
                            Today Income:&nbsp;	
            <asp:Label ID="LabelIncomeToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;SEK<br />
                        </div>
                        <div id="RowSystemStatus" class="row" style="text-align: center">
                            System Status:&nbsp;	
            <asp:Label ID="LabelStatus" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
</body>
</html>
