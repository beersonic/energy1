using EnergyDataRetriever.Controllers;
using EnergyDataRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnergyDataRetriever
{
    public partial class Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CalculatorController cc = new CalculatorController();
            AnalyticsData ad = cc.GetDailyProfit();
            LabelEnergyToday.Text = ad.UnitCount.ToString();
            LabelIncomeToday.Text = ad.Profit.ToString();
            LabelStatus.Text = "OK";
        }
    }
}