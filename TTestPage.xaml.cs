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
            
            f1ScoreValue.Text = T_Test.CalculateMetric(predicted,actual).F1Score.ToString("F4");
            accuracyValue.Text = T_Test.CalculateMetric(predicted,actual).Accuracy.ToString("F4");
            
            meanActualValue.Text = Statistics.Mean(actual).ToString("F4");
            varianceActualValue.Text = Statistics.Variance(actual).ToString("F4");
        }
        private void PerformTTest(double[] sample1, double knownMean)
        {
            // Проверяем, пустая ли выборка
            if (sample1.Length == 0)
            {
                // Обновляем интерфейс значениями по умолчанию, если выборка пустая
                tStatisticValue.Text = "N/A";
                pValueValue.Text = "N/A";
                meanPredictedValue.Text = "N/A";
                variancePredictedValue.Text = "N/A";
                significanceResult.Text = "Sample is empty.";
                f1ScoreValue.Text = "N/A";
                accuracyValue.Text = "N/A";
                return;
            }

            // Вычисляем среднее значение выборки
            double meanSample = Statistics.Mean(sample1);
    
            // Вычисляем дисперсию выборки
            double varSample = Statistics.Variance(sample1);
    
            // Определяем размер выборки
            int n = sample1.Length;

            // Расчет T-статистики для одновыборочного T-теста
            double tStatistic = (meanSample - knownMean) / (varSample / Math.Sqrt(n));
    
            // Вычисляем количество степеней свободы
            double degreesOfFreedom = n - 1;

            // Создаем распределение Стьюдента с нулевым средним и рассчитанным числом степеней свободы
            var studentT = new StudentT(0, 1, degreesOfFreedom);
    
            // Вычисляем p-значение для двустороннего теста
            double pValue = 2 * (1.0 - studentT.CumulativeDistribution(Math.Abs(tStatistic)));

            // Обновляем интерфейс с результатами теста
            tStatisticValue.Text = tStatistic.ToString("F4");
            pValueValue.Text = pValue.ToString("F6");
            meanPredictedValue.Text = meanSample.ToString("F4");
            variancePredictedValue.Text = varSample.ToString("F4");

            // Определяем, является ли разница статистически значимой на основе уровня значимости
            bool isSignificant = pValue < MainWindow.significance_level;
            significanceResult.Text = isSignificant
                ? "Разница статистически значима."
                : "Разница не является статистически значимой.";
        }

    }
}