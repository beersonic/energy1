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

        int _currentId
        {
            get { return (int)ViewState["currentId"]; }
            set { ViewState["currentId"] = value; }
        }

        int _investSek
        {
            get { return (int)ViewState["investSek"]; }
            set { ViewState["investSek"] = value; }
        }

        int _projectSize
        {
            get { return (int)ViewState["projectSize"]; }
            set { ViewState["projectSize"] = value; }
        }

        bool _pageInit
        {
            get { return (bool)ViewState["pageInit"]; }
            set { ViewState["pageInit"] = value; }
        }
        double _yourShare
        {
            get
            {
                double ret = 0;
                if (ViewState["yourShare"] != null)
                {
                    ret = (double)ViewState["yourShare"];
                }
                return ret;
            }
            set { ViewState["yourShare"] = value; }
        }
        double _totalShare
        {
            get { return (double)ViewState["totalShare"]; }
            set { ViewState["totalShare"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (DropDownListProject.Items.Count == 0)
            {
                InitDropDownProject();

                _currentId = -1;
            }
            _pageInit = true;
        }

        struct ProjectDropDownItem
        {
            public int projectId { get; set; }
            public int projectSize { get; set; }
            public string displayField { get; set; }
        }

        void InitDropDownProject()
        {
            DailyDataController ddc = new DailyDataController();
            var projects = ddc.GetAll();
            var pList = from p
                      in projects
                        select new ProjectDropDownItem
                        {
                            projectId = p.projectId
                            ,
                            projectSize = p.projectSize
                            ,
                            displayField = String.Format("{0} : {1}", p.projectId, p.projectSize)
                        };

            var newList = pList.ToList();
            newList.Insert(0, new ProjectDropDownItem { projectId = -1, projectSize = 0, displayField = "Select project" });

            DropDownListProject.DataSource = newList;
            DropDownListProject.DataValueField = "projectId";
            DropDownListProject.DataTextField = "displayField";
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

        }
        void UpdateLabel()
        {
            CalculatorController cc = new CalculatorController();
            AnalyticsData ad = cc.GetDailyProfit(_currentId);

            double totalProfit = cc.GetTotalProfit(_currentId);
            double pricePerUnit = cc.GetPricePerUnit();
            double pctShare = GetYourSharePercent();

            LabelEnergyToday.Text = ad.UnitCount.ToString("N3");
            LabelEnergyTotal.Text = totalProfit.ToString("N3");

            LabelIncomeToday.Text = (ad.Profit * pctShare).ToString("N2");
            LabelIncomeTotal.Text = (totalProfit * pricePerUnit * pctShare).ToString("N2");

            LabelStatus.Text = "OK";
        }

        double GetYourSharePercent()
        {
            return _yourShare / _totalShare;
        }
        protected void DropDownListProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = int.Parse(DropDownListProject.SelectedValue);
            if (projectId >= 0)
            {
                _currentId = projectId;

                var pd = GetCurrentProjectData();
                _totalShare = pd.projectSize / pd.pricePerShare;
                LabelTotalShareAmount.Text = _totalShare.ToString();

                LabelPricePerShare.Text = pd.pricePerShare.ToString();

                TimedPanelProject1.Update();
            }
        }

        private ProjectData GetCurrentProjectData()
        {
            DailyDataController dc = new DailyDataController();
            return dc.GetProjectInfo(_currentId);
        }
        protected void TextBoxInvest_TextChanged(object sender, EventArgs e)
        {
            int investSek = 0;
            if (int.TryParse(TextBoxInvest.Text, out investSek))
            {
                _investSek = investSek;

                var pd = GetCurrentProjectData();
                _yourShare = investSek / pd.pricePerShare;
                LabelYourShare.Text = _yourShare.ToString();

                LabelYourSharePct.Text = (GetYourSharePercent() * 100.0).ToString();

                TimedPanelProject2.Update();
            }
        }
    }
}