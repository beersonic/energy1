using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnergyDataRetriever.Models
{
    public class ProjectData
    {
        const double PRICE_PER_SHARE = 100;

        static int currentId = 0;

        public double pricePerShare { get; set; }
        public int projectId { get; set; }
        public int projectSize { get; set; }
        public List<EnergyData> listEnergyData { get; set; }

        public ProjectData()
        {
            projectId = currentId++;
            pricePerShare = PRICE_PER_SHARE;
        }
    }
}