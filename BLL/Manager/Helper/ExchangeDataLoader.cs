using BLL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL.Deserializer;

namespace BLL.Manager.Helper
{
    public static class ExchangeDataLoader
    {
        private static List<ExchangeModel> _cachedExchangeModels;
        private static readonly ExchangeDeserializer _deserializer = new ExchangeDeserializer();
        private static string _cachedjson;
        public static List<ExchangeModel> LoadExchangeModels()
        {
            if (_cachedExchangeModels != null)
            {
                return _cachedExchangeModels;
            }
            

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BLL.Model.exchanges.json";

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        throw new FileNotFoundException("Embedded resource not found: " + resourceName);

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        _cachedExchangeModels = _deserializer.Deserialize(json); 
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error loading exchange data: {ex.Message}");
                throw;
            }

            return _cachedExchangeModels;
        }

        public static string LoadJson()
        {
            if (!string.IsNullOrEmpty(_cachedjson)) return _cachedjson;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BLL.Model.exchanges.json";

            try
            {
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) throw new FileNotFoundException("Embedded resource not found: " + resourceName);

                    using (var reader = new StreamReader(stream))
                    {
                        _cachedjson = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON data: {ex.Message}");
                throw;
            }

            return _cachedjson;
        }
    }

}
