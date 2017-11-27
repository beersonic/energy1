using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyCalculator.Models
{
    public class DailyDataItem
    {
        static int _id = 0;
        
        public long Id { get; set; }
        public DateTime timestamp;
        public double yield;

        public DailyDataItem()
        {
            Id = ++_id;
        }
    }
}
