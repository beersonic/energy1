using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnergyCalculator.Models
{
    public class DailyDataContext : DbContext
    {
        public DailyDataContext(DbContextOptions<DailyDataContext> options) : base(options)
        {

        }

        public DbSet<DailyDataItem> SeriesOfDailyData { get; set; }
    }
}
