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

        [Route("api/GetTotal")]
        public double GetTotalProfit()
        {
            AnalyticsData ad = new AnalyticsData();

            double totalOnMonth = (new DailyDataController()).GetAll().Sum(x => x.Yield);
            double addOn = (DateTime.Now - (new DateTime(2017, 10, 31))).TotalDays / 31 * 1200;
            return totalOnMonth + addOn;
        }

        [Route("api/GetProfitToday")]
        public AnalyticsData GetDailyProfit()
        {
            return GetProfitByDateTimeTest(DateTime.Now);
        }
        [Route("api/GetProfitByDate")]
        public AnalyticsData GetProfitByDateYYYYMMDD(String timestampYYYYMMDD)
        {
            Regex rx = new Regex(@"(\d{4})(\d{2})(\d{2})");
            Match sm;
            if (!(sm = rx.Match(timestampYYYYMMDD)).Success)
            {
                return null;
            }

            DateTime timestamp2 = new DateTime(int.Parse(sm.Groups[1].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[3].Value));
            return GetProfitByDateTimeTest(timestamp2);
        }
        private AnalyticsData GetProfitByDateTimeTest(DateTime timestamp)
        {
            Models.EnergyData ed = (new DailyDataController()).Get(timestamp.ToString("yyyyMMdd"));

            int investerCount = 10;
            double pricePerUnit = 1.4;

            int secondFromMidnight = (timestamp.Hour * 3600) + (timestamp.Minute * 60) + timestamp.Second;
            double currentYield = ed.Yield * (double)secondFromMidnight / (double)TOTAL_SECOND_A_DAY;

            double profit = (currentYield * pricePerUnit) / investerCount;

            AnalyticsData ad = new AnalyticsData()
            {
                NumberOfInvester = investerCount
                ,
                PricePerUnit = pricePerUnit
                ,
                Profit = profit
                ,
                UnitCount = currentYield
            };
            return ad;
        }
    }
}
