using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnergyDataRetriever.Models
{
    public class EnergyData
    {
        public DateTime TimeStamp { get; set; }
        public double Yield { get; set; }
    }
}