﻿using Newtonsoft.Json.Linq;
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

        //Overloaded method for working with nested objects.
        public string GetStringValue(JToken item, string parentKey, string childKey)
        {
            if (item[parentKey] != null && item[parentKey][childKey] != null)
            {
                return item[parentKey][childKey].ToString();
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
        // Overloaded method for index-based extraction(CoinGecko candles)
        public decimal? GetDecimalValue(JToken item, int index)
        {
            var valueStr = item?[index]?.ToString();
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
        public long? GetLongValue(JToken item, string key)
        {
            var valueStr = item?[key]?.ToString();
            if (string.IsNullOrEmpty(valueStr))
            {
                return null;
            }

            try
            {
                return long.Parse(valueStr);
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

        // Overloaded GetLongValue for index-based extraction(CoinGecko candles)
        public long? GetLongValue(JToken item, int index)
        {
            var valueStr = item?[index]?.ToString();
            if (string.IsNullOrEmpty(valueStr))
            {
                return null;
            }

            try
            {
                return long.Parse(valueStr);
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
