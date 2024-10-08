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
       

        public List<CandlestickModel> Deserialize (string json)
        {
            
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
                    Timestamp = DeserializerHelper.GetLongValue(item, 0),  // Timestamp in milliseconds
                    Open = DeserializerHelper.GetDecimalValue(item, 1),    // Open price
                    High = DeserializerHelper.GetDecimalValue(item, 2),    // High price
                    Low = DeserializerHelper.GetDecimalValue(item, 3),     // Low price
                    Close = DeserializerHelper.GetDecimalValue(item, 4)    // Close price
                });
            }

            return CandelStickData;
        }
    }
}
