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
        /*
        public double GetTotalProfit()
        {
            AnalyticsData ad = new AnalyticsData();

            (new DailyDataController()).GetAll().Sum()
        }
        */
        [Route("api/GetProfitToday")]
        public AnalyticsData GetDailyProfit()
        {
            return GetProfitByDate(DateTime.Now);
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
            return GetProfitByDate(timestamp2);
        }
        private AnalyticsData GetProfitByDate(DateTime timestamp)
        {
            Models.EnergyData ed = (new DailyDataController()).Get(timestamp.ToString("yyyyMMdd"));

            int investerCount = 10;
            double pricePerUnit = 1.4;

            double profit = (ed.Yield * pricePerUnit) / investerCount;

            AnalyticsData ad = new AnalyticsData()
            {
                NumberOfInvester = investerCount
                ,
                PricePerUnit = pricePerUnit
                ,
                Profit = profit
                ,
                UnitCount = ed.Yield
            };
            return ad;
        }
    }
}
