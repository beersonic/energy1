﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;
using EnergyDataRetriever.Controllers;
using EnergyDataRetriever.Models;

namespace EnergyDataRetriever
{
    public partial class _Default : System.Web.UI.Page
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