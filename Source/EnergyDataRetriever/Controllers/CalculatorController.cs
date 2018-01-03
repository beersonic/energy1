using EnergyDataRetriever.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace EnergyDataRetriever.Controllers
{
    public class CalculatorController : ApiController
    {
        const int TOTAL_SECOND_A_DAY = 24 * 3600;
        const double pricePerUnit = 1.4;
        DateTime PROJECT_START_DATE = new DateTime(2017, 10, 31);
        RawEnergyDataController _energyData = new RawEnergyDataController();

        [Route("api/GetTotal")]
        public double GetTotalProfit(int projectId)
        {
            double totalOnMonth = _energyData.GetAllById(projectId).Sum(x => x.Yield);
            double addOn = (DateTime.Now - PROJECT_START_DATE).TotalDays / 31 * 1200;
            return totalOnMonth + addOn;
        }
        [Route("api/GetStartDate")]
        public DateTime GetStartDate(int projectId)
        {
            return _energyData.GetProjectInfo(projectId).startDate;
        }

        [Route("api/GetProfitToday")]
        public AnalyticsData GetDailyProfit(int projectId)
        {
            return GetProfitByDateTimeTest(projectId, DateTime.Now);
        }

        [Route("api/GetProfitByDate")]
        public AnalyticsData GetProfitByDateYYYYMMDD(int projectId, String timestampYYYYMMDD)
        {
            Regex rx = new Regex(@"(\d{4})(\d{2})(\d{2})");
            Match sm;
            if (!(sm = rx.Match(timestampYYYYMMDD)).Success)
            {
                return null;
            }

            DateTime timestamp2 = new DateTime(int.Parse(sm.Groups[1].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[3].Value));
            return GetProfitByDateTimeTest(projectId, timestamp2);
        }
        private AnalyticsData GetProfitByDateTimeTest(int projectId, DateTime timestamp)
        {
            Models.EnergyData ed = _energyData.Get(projectId, timestamp.ToString("yyyyMMdd"));

            int secondFromMidnight = (timestamp.Hour * 3600) + (timestamp.Minute * 60) + timestamp.Second;
            double currentYield = ed.Yield * (double)secondFromMidnight / (double)TOTAL_SECOND_A_DAY;

            double profit = (currentYield * pricePerUnit);

            AnalyticsData ad = new AnalyticsData()
            {
                PricePerUnit = pricePerUnit
                ,
                Profit = profit
                ,
                UnitCount = currentYield
            };
            return ad;
        }

        public double GetPricePerUnit()
        {
            return pricePerUnit;
        }
    }
}
