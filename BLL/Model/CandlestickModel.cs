using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{

    public class CandlestickModel
    {
        public long? Timestamp { get; set; }  // Time in milliseconds
        public decimal? Open { get; set; }    // Opening price
        public decimal? High { get; set; }    // Highest price
        public decimal? Low { get; set; }     // Lowest price
        public decimal? Close { get; set; }   // Closing price
    }
}
