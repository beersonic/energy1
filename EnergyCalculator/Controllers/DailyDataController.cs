using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnergyCalculator.Models;
using System.IO;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyCalculator.Controllers
{
    [Route("api/energy")]
    public class DailyDataController : Controller
    {
        private readonly DailyDataContext _context;
        public DailyDataController(DailyDataContext context)
        {
            _context = context;

            if (_context.SeriesOfDailyData.Count() == 0)
            {
                String sampleFile = Path.Combine(Directory.GetCurrentDirectory(), @"Files", @"sampleData.txt");

                StreamReader sr = new StreamReader(sampleFile);
                while(!sr.EndOfStream)
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

                    DailyDataItem ddi = new DailyDataItem() { yield = yield, timestamp = timestamp };
                    _context.SeriesOfDailyData.Add(ddi);
                }
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<DailyDataItem> GetAll()
        {
            return _context.SeriesOfDailyData.ToList();
        }

        [HttpGet("ByDate")]
        public IActionResult GetByDate(String datetimeYYYYMMDD)
        {
            Regex rx = new Regex(@"(\d{4})(\d{2})(\d{2})");
            Match sm;
            if (!(sm = rx.Match(datetimeYYYYMMDD)).Success)
            {
                return Content("Invalid datetime input, must be in format YYYYMMDD");
            }

            DateTime timestamp = new DateTime(int.Parse(sm.Groups[1].Value), int.Parse(sm.Groups[2].Value), int.Parse(sm.Groups[3].Value));

            var item = _context.SeriesOfDailyData.First(i => i.timestamp == timestamp);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}
