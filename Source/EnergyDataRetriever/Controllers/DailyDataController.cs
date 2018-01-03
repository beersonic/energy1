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
    public class RawEnergyDataController : ApiController
    {
        [Route("api/Test")]
        public IHttpActionResult GetTest()
        {
            return Ok("Hello World WEB API");
        }

        [Route("api/GetProjectInfo")]
        public ProjectData GetProjectInfo(int projectId)
        {
            return DataStore.Get().GetAll().First(p => p.projectId == projectId);
        }

        [Route("api/GetByDate")]
        public EnergyData Get(int id, String datetimeYYYYMMDD)
        {
            EnergyData result = null;

            Regex rx = new Regex(@"(\d{4})(\d{2})(\d{2})");
            Match sm;
            if (!(sm = rx.Match(datetimeYYYYMMDD)).Success)
            {
                return null;
            }

            DateTime timestamp = new DateTime(int.Parse(sm.Groups[1].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[3].Value));
            result = DataStore.Get().GetByDate(id, timestamp);

            return result;
        }

        [Route("api/GetAll")]
        public IQueryable<ProjectData> GetAll()
        {
            return DataStore.Get().GetAll();
        }

        [Route("api/GetAllById")]
        public IQueryable<EnergyData> GetAllById(int projectId)
        {
            var listProjectData = DataStore.Get().GetAll();
            var projectInfo = listProjectData.First(e => e.projectId == projectId);
            return projectInfo.listEnergyData.AsQueryable();
        }
    }
}
