using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Deserializer.Helper;
using BLL.Model;
using Newtonsoft.Json.Linq;

namespace BLL.Deserializer
{
    public class CandlesDeserializer
    {
        private DeserializerHelper _helper;

        public List<CandlestickModel> Deserialize (string json)
        {
            _helper = new DeserializerHelper();
            if (string.IsNullOrEmpty(json))
            {
                return new List<CandlestickModel>();
            }

            var jsonData = JArray.Parse(json);
            return convertToCandlestickModels(jsonData);
        }

        private List<CandlestickModel> convertToCandlestickModels (JToken data)
        {
            var CandelStickData = new List<CandlestickModel>();

            foreach (var item in data)
            {
                CandelStickData.Add(new CandlestickModel()
                {
                    Timestamp = _helper.GetLongValue(item, 0),  // Timestamp in milliseconds
                    Open = _helper.GetDecimalValue(item, 1),    // Open price
                    High = _helper.GetDecimalValue(item, 2),    // High price
                    Low = _helper.GetDecimalValue(item, 3),     // Low price
                    Close = _helper.GetDecimalValue(item, 4)    // Close price
                });
            }

            return CandelStickData;
        }
    }
}
