using System;
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
            
        }

        protected void TimerUpdateLabel_Tick(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                MasterPageFile = "~/Mobile.Master";
            }
        }
        void UpdateLabel()
        {
            CalculatorController cc = new CalculatorController();
            AnalyticsData ad = cc.GetDailyProfit();
            LabelEnergyToday.Text = ad.UnitCount.ToString("N3");
            LabelIncomeToday.Text = ad.Profit.ToString("N3");

            LabelEnergyTotal.Text = cc.GetTotalProfit().ToString("N3");

            LabelStatus.Text = "OK";
        }
    }
}