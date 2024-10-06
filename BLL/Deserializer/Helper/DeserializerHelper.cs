using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Deserializer.Helper
{
    public class DeserializerHelper
    {
        public string GetStringValue(JToken item, string key)
        {
            if (item[key] != null)
            {
                return item[key].ToString();
            }
            return null; 
        }

        public decimal? GetDecimalValue(JToken item, string key)
        {
            var valueStr = item[key]?.ToString();
            if (string.IsNullOrEmpty(valueStr))
            {
                return null;
            }

            try
            {
                return decimal.Parse(valueStr);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }
        }
    }
}