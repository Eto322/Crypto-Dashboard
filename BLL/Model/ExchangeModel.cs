using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class ExchangeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public int? CoinId { get; set; }
        
        public string? TradeUrl { get; set; }
    }

    
    
}
