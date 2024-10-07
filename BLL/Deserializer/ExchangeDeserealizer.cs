using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using Newtonsoft.Json;

namespace BLL.Deserializer
{
    public class ExchangeDeserializer
    {
        public List<ExchangeModel> Deserialize(string json)
        {
            var exchanges = JsonConvert.DeserializeObject<List<ExchangeModel>>(json);
            return exchanges;
        }
    }
}
