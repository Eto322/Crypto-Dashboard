using BLL.Model;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot.Axes;

namespace UI.Model
{
   
     public static class  CandlestickPlotModel
    {
        public static PlotModel CreateCandlestickPlotModel(List<CandleStickItemModel> candlestickItems)
        {
            var plotModel = new PlotModel { Title = "Candlestick Chart" };
            

            var candlestickSeries = new CandleStickSeries
            {
                Color = OxyColors.Black,
                IncreasingColor = OxyColors.DarkGreen,
                DecreasingColor = OxyColors.Red,
                DataFieldX = "X",         
                DataFieldHigh = "High",   
                DataFieldLow = "Low",     
                DataFieldOpen = "Open",   
                DataFieldClose = "Close",  
                TrackerFormatString = "High: {2:0.00}\nLow: {3:0.00}\nOpen: {4:0.00}\nClose: {5:0.00}",
                ItemsSource = candlestickItems
            };

            plotModel.Series.Add(candlestickSeries);
            var asd = plotModel.Series.Count();
            return plotModel;
        }

    }
}
