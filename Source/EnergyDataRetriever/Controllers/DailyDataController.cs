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
    public class DailyDataController : ApiController
    {
        [Route("api/Test")]
        public IHttpActionResult GetTest()
        {
            return Ok("Hello World WEB API");
        }

        [Route("api/GetByDate")]
        public EnergyData Get(String datetimeYYYYMMDD)
        {
            EnergyData result = null;

            Regex rx = new Regex(@"(\d{4})(\d{2})(\d{2})");
            Match sm;
            if (!(sm = rx.Match(datetimeYYYYMMDD)).Success)
            {
                return null;
            }

            DateTime timestamp = new DateTime(int.Parse(sm.Groups[1].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[3].Value));
            result = DataStore.Get().GetByDate(timestamp);

            return result;
        }

        [Route("api/GetAll")]
        public IQueryable<EnergyData> GetAll()
        {
            return DataStore.Get().GetAll();
        }
    }
}
