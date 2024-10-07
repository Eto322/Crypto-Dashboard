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
        public static PlotModel CreateCandlestickPlotModel(List<CandleStickItemModel> candlestickItems, bool isDarkTheme)
        {
            var plotModel = new PlotModel { Title = "Candlestick Chart" };

            if (isDarkTheme)
            {
               
                plotModel.Background = OxyColor.FromArgb(255, 50, 50, 50); 
                plotModel.TextColor = OxyColors.White; 
            }
            else
            {
                plotModel.Background = OxyColor.FromArgb(255, 255, 255, 255); 
                plotModel.TextColor = OxyColors.Black; 
            }

            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd HH:mm",
                IntervalType = DateTimeIntervalType.Minutes,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalLength = 60, 
                Angle = 45, 
            });

            var candlestickSeries = new CandleStickSeries
            {
                DataFieldX = "X",
                DataFieldHigh = "High",
                DataFieldLow = "Low",
                DataFieldOpen = "Open",
                DataFieldClose = "Close",
                TrackerFormatString = "High: {2:0.00}\nLow: {3:0.00}\nOpen: {4:0.00}\nClose: {5:0.00}",
                ItemsSource = candlestickItems
            };

            plotModel.Series.Add(candlestickSeries);
            return plotModel;
        }

    }
}
