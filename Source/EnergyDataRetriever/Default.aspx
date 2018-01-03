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
                <h1>Sunabler Portal</h1>
            </div>
            <div id="project">
                Project:
                <asp:DropDownList ID="DropDownListProject" runat="server" OnSelectedIndexChanged="DropDownListProject_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <asp:UpdatePanel runat="server" ID="TimedPanelProject1" UpdateMode="Conditional">
                    <ContentTemplate>
                        Total share:
                        <asp:Label runat="server" ID="LabelTotalShareAmount"></asp:Label>
                        <br />
                        Price per share:
                        <asp:Label ID="LabelPricePerShare" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

                Input your invest (SEK):
                <asp:TextBox ID="TextBoxInvest" runat="server" TextMode="Number" OnTextChanged="TextBoxInvest_TextChanged"></asp:TextBox>
                <br />
                <asp:UpdatePanel runat="server" ID="TimedPanelProject2" UpdateMode="Conditional">
                    <ContentTemplate>
                        Your share:
                        <asp:Label ID="LabelYourShare" runat="server" Font-Bold="true"></asp:Label>

                        &nbsp;(
                        <asp:Label ID="LabelYourSharePct" runat="server" Font-Bold="true"></asp:Label>
                        &nbsp;%)

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <hr />
            <div>
                <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TimerUpdateLabel" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="RowTotalYield">
                            Total Energy Yield:&nbsp;	
        <asp:Label ID="LabelEnergyTotal" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh<br />
                        </div>
                        <div id="RowTotalIncome">
                            Total Income :&nbsp;	
            <asp:Label ID="LabelIncomeTotal" runat="server" Text="N/A"></asp:Label>
                            &nbsp;SEK<br />
                        </div>
                        <div id="RowTodayYield" >
                            Today Energy Yield:&nbsp;	
        <asp:Label ID="LabelEnergyToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh
                        </div>
                        <div id="RowTodayIncome" >
                            Today Income:&nbsp;	
        <asp:Label ID="LabelIncomeToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;SEK
                        </div>
                        <div id="RowSystemStart">
                            System Status:&nbsp;	
            <asp:Label ID="LabelSystemStart" runat="server" Text=""></asp:Label>
                            &nbsp;(<asp:Label ID="LabelSystemUpTime" runat="server" Text=""></asp:Label>
                            )</div>
                        <div id="RowSystemStatus">
                            System Status:&nbsp;	
            <asp:Label ID="LabelStatus" runat="server" Text="N/A"></asp:Label>
                        </div>
                        <hr />
                        <div id="RowYourYieldToday" style="font-weight: bold">
                            Your Energy Yield Today:&nbsp;	
            <asp:Label ID="LabelYourYieldToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh<br />
                        </div>
                        <div id="RowYourIncomeToday" style="font-weight: bold">
                            Your Income Today:&nbsp;	
            <asp:Label ID="LabelYourIncomeToday" runat="server" Text="N/A"></asp:Label>
                            &nbsp;SEK<br /> <br />
                        </div>
                        <div id="RowYourTotalYield" style="font-weight: bold">
                            Your Total Energy Yield:&nbsp;	
            <asp:Label ID="LabelYourTotalYield" runat="server" Text="N/A"></asp:Label>
                            &nbsp;kWh<br />
                        </div>
                        <div id="RowYourTotalIncome" style="font-weight: bold">
                            Your Total Income:&nbsp;	
            <asp:Label ID="LabelYourTotalIncome" runat="server" Text="N/A"></asp:Label>
                            &nbsp;SEK<br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
</body>
</html>
