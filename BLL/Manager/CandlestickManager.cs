using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public class CandlestickManager
    {
        private List<CandlestickModel> _candlesticks; 

        public CandlestickManager()
        {
            _candlesticks = new List<CandlestickModel>();
        }

        
        public void AddCandlestick(CandlestickModel candlestick)
        {
            if (candlestick != null)
            {
                _candlesticks.Add(candlestick);
            }
        }

        
        public void LoadCandlestickModels(List<CandlestickModel> candlestickModels)
        {
            if (candlestickModels != null)
            {
                _candlesticks.Clear(); // Clear existing data to load new models
                _candlesticks.AddRange(candlestickModels);
            }
        }

        
        public void ClearCandlesticks()
        {
            _candlesticks.Clear(); 
        }

        
        public List<CandlestickModel> GetCandlestickModels()
        {
            return new List<CandlestickModel>(_candlesticks);
        }
    }
}

