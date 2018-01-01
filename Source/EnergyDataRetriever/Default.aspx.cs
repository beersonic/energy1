using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;
using EnergyDataRetriever.Controllers;
using EnergyDataRetriever.Models;
using System.Linq;

namespace EnergyDataRetriever
{
    public partial class _Default : System.Web.UI.Page
    {
        const int ID_NOT_INIT = -1;

        int _currentId = ID_NOT_INIT;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownListProject.Items.Count == 0)
            {
                InitDropDownProject();
            }
        }

        void InitDropDownProject()
        {
            DailyDataController ddc = new DailyDataController();
            var projects = ddc.GetAll();
            var pList = from p
                      in projects
                        select new
                        {
                            p.projectId
                            ,
                            p.projectSize
                            ,
                            DisplayField = String.Format("{0} : {1}", p.projectId, p.projectSize)
                        };

            pList.ToList().Insert(0, new { projectId = -1, projectSize = 0, DisplayField = "Select project" });
            DropDownListProject.DataSource = pList.ToList();
            DropDownListProject.DataValueField = "projectId";
            DropDownListProject.DataTextField = "DisplayField";
            DropDownListProject.DataBind();
        }

        protected void TimerUpdateLabel_Tick(object sender, EventArgs e)
        {
            bool isValidId = _currentId != ID_NOT_INIT;
            TimedPanel.Visible = isValidId;
            if (isValidId)
            {
                TimedPanel.Visible = true;
                UpdateLabel();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            /*
            if (Request.Browser.IsMobileDevice)
            {
                MasterPageFile = "~/Mobile.Master";
            }
            */
        }
        void UpdateLabel()
        {
            CalculatorController cc = new CalculatorController();
            AnalyticsData ad = cc.GetDailyProfit(_currentId);
            LabelEnergyToday.Text = ad.UnitCount.ToString("N3");
            LabelIncomeToday.Text = ad.Profit.ToString("N3");

            LabelEnergyTotal.Text = cc.GetTotalProfit(_currentId).ToString("N3");

            LabelStatus.Text = "OK";
        }

        protected void DropDownListProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = int.Parse(DropDownListProject.SelectedValue);
            if (projectId >= 0)
            {
                _currentId = projectId;               
            }
        }
    }
}