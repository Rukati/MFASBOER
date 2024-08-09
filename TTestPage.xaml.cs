using System;
using System.Windows.Controls;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace WpfApplication1
{
    public partial class TTestPage : Page
    {
        public TTestPage(double[] predicted, double[] actual)
        {
            InitializeComponent();
            PerformInitialTTest(predicted, actual);
        }
        private void PerformInitialTTest(double[] predicted, double[] actual)
        {
            PerformTTest(T_Test.CalculateUserAccuracies(), T_Test.CalculateMetric(predicted,actual).Accuracy);
            
            f1ScoreValue.Text = T_Test.CalculateMetric(predicted,actual).F1Score.ToString();
            accuracyValue.Text = T_Test.CalculateMetric(predicted,actual).Accuracy.ToString();
            
            meanActualValue.Text = Statistics.Mean(actual).ToString("F4");
            varianceActualValue.Text = Statistics.Variance(actual).ToString("F4");
        }
        private void PerformTTest(double[] sample1, double knownMean)
        {
            if (sample1.Length == 0)
            {
                tStatisticValue.Text = "N/A";
                pValueValue.Text = "N/A";
                meanPredictedValue.Text = "N/A";
                variancePredictedValue.Text = "N/A";
                significanceResult.Text = "Sample is empty.";
                f1ScoreValue.Text = "N/A";
                accuracyValue.Text = "N/A";
                return;
            }

            double meanSample = Statistics.Mean(sample1);
            double varSample = Statistics.Variance(sample1);
            int n = sample1.Length;

            double tStatistic = (meanSample - knownMean) / (varSample / Math.Sqrt(n));
            double degreesOfFreedom = n - 1;

            var studentT = new StudentT(0, 1, degreesOfFreedom);
            double pValue = 2 * (1.0 - studentT.CumulativeDistribution(Math.Abs(tStatistic)));

            tStatisticValue.Text = tStatistic.ToString("F4");
            pValueValue.Text = pValue.ToString("F6");
            meanPredictedValue.Text = meanSample.ToString("F4");
            variancePredictedValue.Text = varSample.ToString("F4");

            bool isSignificant = pValue < MainWindow.significance_level;
            significanceResult.Text = isSignificant
                ? "Разница статистически значима."
                : "Разница не является статистически значимой.";
            
        }
    }
}