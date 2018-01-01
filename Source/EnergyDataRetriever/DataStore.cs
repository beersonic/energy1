using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using EnergyDataRetriever.Models;
using System.Web.Hosting;

namespace EnergyDataRetriever
{
    public class DataStore
    {
        Dictionary<int, ProjectData> _dictProjectIdToInfo = new Dictionary<int, ProjectData>();

        //List<EnergyData> _listEnergyData = new List<EnergyData>();
        static DataStore _instance = null;

        public static DataStore Get()
        {
            if (_instance == null)
            {
                _instance = new DataStore();
                _instance.Init();
            }
            return _instance;
        }
        public EnergyData GetByDate(int projectId, DateTime dt)
        {
            var projectInfo = _dictProjectIdToInfo.First(e => e.Key == projectId);
            EnergyData entry = projectInfo.Value.listEnergyData.First(e => e.TimeStamp.Day == dt.Day);
            return entry;
        }
        public IQueryable<ProjectData> GetAll()
        {
            return _dictProjectIdToInfo.Values.AsQueryable();
        }
        private void Init()
        {
            String sampleFile = HostingEnvironment.MapPath(@"\App_Data\sampleData.txt");
            StreamReader sr = new StreamReader(sampleFile);

            // mock data per day
            List<EnergyData> listEnergyData = new List<EnergyData>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] tokens = line.Split(new char[] { '\t' });

                string datetimeStr = tokens[0];
                Regex rx = new Regex(@"(\d+)-(\d+)-(\d+)");
                Match sm;
                if (!(sm = rx.Match(datetimeStr)).Success)
                {
                    throw new Exception(String.Format("Invalid timestamp, {0}", datetimeStr));
                }
                DateTime timestamp = new DateTime(int.Parse(sm.Groups[3].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[1].Value));
                double yield;
                if (!double.TryParse(tokens[1], out yield))
                {
                    yield = 0;
                }


                EnergyData ed = new EnergyData() { Yield = yield, TimeStamp = timestamp };
                listEnergyData.Add(ed);
            }

            // mock projects
            var projectInfo = new ProjectData() { projectSize = 100000, listEnergyData = listEnergyData };
            _dictProjectIdToInfo.Add(projectInfo.projectId, projectInfo);

            projectInfo = new ProjectData() { projectSize = 200000, listEnergyData = listEnergyData };
            _dictProjectIdToInfo.Add(projectInfo.projectId, projectInfo);

            projectInfo = new ProjectData() { projectSize = 500000, listEnergyData = listEnergyData };
            _dictProjectIdToInfo.Add(projectInfo.projectId, projectInfo);
            
        }
    }
}