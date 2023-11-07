using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp33
{
    internal class Charts
    {
        private SeriesCollection seriesCollection;

        public LiveCharts.WinForms.CartesianChart cartesianChart1;

        private LineSeries setpointSeries = new LineSeries
        {
            Title = "Setpoint",
            Values = new ChartValues<double> { 10, 10 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };
        private LineSeries outputSeries = new LineSeries
        {
            Title = "Output",
            Values = new ChartValues<double> { 12, 12 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };
        private LineSeries outputPIDSeries = new LineSeries
        {
            Title = "Output PID",
            Values = new ChartValues<double> { 12, 12 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };


        public void InitializeChart()
        {
            cartesianChart1.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 100,
            });

            seriesCollection = new SeriesCollection(setpointSeries);
            cartesianChart1.Series = seriesCollection;

            seriesCollection.Add(setpointSeries);
            seriesCollection.Add(outputSeries);
            seriesCollection.Add(outputPIDSeries);
        }

        public void UpdateModel(double targetValue, double temperat, double temperatPID)
        {
            //cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            setpointSeries.Values.Add(targetValue);
            outputSeries.Values.Add(temperat);
            outputPIDSeries.Values.Add(temperatPID);

            if (setpointSeries.Values.Count > 120)
            {
                setpointSeries.Values.RemoveAt(0);

                outputSeries.Values.RemoveAt(0);
                outputPIDSeries.Values.RemoveAt(0);

            }

            cartesianChart1.Update();
        }
    }
}
