using System;
using System.Windows;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;

namespace WpfApplication1
{
    public partial class Result : Window
    {
        private double[] predicted;
        private double[] actual;

        public Result(double[] predicted, double[] actual)
        {
            InitializeComponent();
            this.predicted = predicted;
            this.actual = actual;

            // Выполнить начальный T-Test
            PerformInitialTTest(predicted, actual);
        }

        private void InitialTTestButton_Click(object sender, RoutedEventArgs e)
        {
            PerformInitialTTest(predicted, actual);
        }
        
        private void AccuracyButton_Click(object sender, RoutedEventArgs e)
        {
            PerformTTestForAccuracy(predicted, actual);
        }
        
        private void PerformTTestForAccuracy(double[] predicted, double[] actual)
        {
            double[] accuracies = new double[predicted.Length];

            // Рассчитываем Accuracy для каждого предсказания
            for (int i = 0; i < predicted.Length; i++)
            {
                accuracies[i] = predicted[i] == actual[i] ? 1.0 : 0.0;
            }

            // Среднее значение Accuracy выборки
            double meanAccuracy = Statistics.Mean(accuracies);
    
            // Дисперсия выборки
            double varianceAccuracy = Statistics.Variance(accuracies);

            // Эталонное значение Accuracy (например, 1.0)
            double benchmarkAccuracy = 1.0;

            // Рассчитываем t-статистику
            double tStatistic = (meanAccuracy - benchmarkAccuracy) / (Math.Sqrt(varianceAccuracy / accuracies.Length));

            // Степени свободы: размер выборки - 1
            double df = accuracies.Length - 1;


            // Проводим t-тест
            var studentT = new StudentT(0, 1, df);
            double pValue = 2 * (1.0 - studentT.CumulativeDistribution(Math.Abs(tStatistic)));

            // Выводим результаты
            tStatisticValue.Text = tStatistic.ToString("F4");
            degreesOfFreedomValue.Text = df.ToString("F4");
            pValueValue.Text = pValue.ToString("F6");
            f1ScoreValue.Text = "N/A"; // Не применимо для Accuracy
            accuracyValue.Text = meanAccuracy.ToString("F4");

            bool isSignificant = pValue <= MainWindow.significance_level;
            significanceResult.Text = isSignificant
                ? "Разница статистически значима."
                : "Разница не является статистически значимой.";
        }
        private void PerformInitialTTest(double[] predicted, double[] actual)
        {
            PerformTTest(predicted, actual);
        }
        private void PerformTTest(double[] sample1, double[] sample2)
        {
            if (sample1.Length == 0 || sample2.Length == 0)
            {
                // Оповестить пользователя о том, что выборки пусты
                tStatisticValue.Text = "N/A";
                degreesOfFreedomValue.Text = "N/A";
                pValueValue.Text = "N/A";
                meanPredictedValue.Text = "N/A";
                meanActualValue.Text = "N/A";
                variancePredictedValue.Text = "N/A";
                varianceActualValue.Text = "N/A";
                significanceResult.Text = "One or both of the samples are empty.";
                f1ScoreValue.Text = "N/A";
                accuracyValue.Text = "N/A";
                return;
            }

            // Вычисление средних значений и дисперсий
            double meanSample1 = Statistics.Mean(sample1);
            double meanSample2 = Statistics.Mean(sample2);
            double varSample1 = Statistics.Variance(sample1);
            double varSample2 = Statistics.Variance(sample2);

            // Размеры выборок
            int n1 = sample1.Length;
            int n2 = sample2.Length;

            // Вычисление T-статистики и степеней свободы
            double pooledVariance = ((varSample1 / n1) + (varSample2 / n2));
            double tStatistic = (meanSample1 - meanSample2) / Math.Sqrt(pooledVariance);
            double degreesOfFreedom = (sample1.Length - 1) + (sample2.Length - 1);

            // Вычисление p-значения
            var studentT = new StudentT(0, 1, degreesOfFreedom);
            double pValue = 2 * (1.0 - studentT.CumulativeDistribution(Math.Abs(tStatistic)));

            // Обновление текстовых блоков с результатами
            tStatisticValue.Text = tStatistic.ToString("F4");
            degreesOfFreedomValue.Text = degreesOfFreedom.ToString("F4");
            pValueValue.Text = pValue.ToString("F6");
            meanPredictedValue.Text = meanSample1.ToString("F4");
            meanActualValue.Text = meanSample2.ToString("F4");
            variancePredictedValue.Text = varSample1.ToString("F4");
            varianceActualValue.Text = varSample2.ToString("F4");

            // Определение значимости
            bool isSignificant = pValue < MainWindow.significance_level;
            significanceResult.Text = isSignificant
                ? "Разница статистически значима."
                : "Разница не является статистически значимой.";

            f1ScoreValue.Text = CalculateMetric(sample1, sample2).F1Score.ToString("F4");
            accuracyValue.Text = CalculateMetric(sample1, sample2).Accuracy.ToString("F4");
        }

        public static (double F1Score, double Accuracy) CalculateMetric(double[] predicted, double[] actual)
        {
            int tp = 0; // True Positives
            int tn = 0; // True Negatives
            int fp = 0; // False Positives
            int fn = 0; // False Negatives

            for (int i = 0; i < predicted.Length; i++)
            {
                if (actual[i] == 1 && predicted[i] == 1)
                    tp++;
                if (actual[i] == 0 && predicted[i] == 0)
                    tn++;
                if (actual[i] == 0 && predicted[i] == 1)
                    fp++;
                if (actual[i] == 1 && predicted[i] == 0)
                    fn++;
            }

            // Accuracy calculation
            double accuracy = (tp + tn) / (double)(predicted.Length);

            // Precision and Recall calculation
            double precision = tp + fp > 0 ? tp / (double)(tp + fp) : 0;
            double recall = tp + fn > 0 ? tp / (double)(tp + fn) : 0;

            // F1 Score calculation
            double f1Score = (precision + recall > 0) ? 2 * (precision * recall) / (precision + recall) : 0;

            return (f1Score, accuracy);
        }
    }
}