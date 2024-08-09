using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfApplication1
{
    internal class T_Test
    {
        public static Dictionary<string, List<(int Predicted, int Actual)>> userResponses;
        public static void Run()
        {
            int count = 0;
            string filePath = MainWindow.filePath;

            try
            {
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                {
                    string line;
                    // Пропустить первую строку (заголовок)
                    streamReader.ReadLine();

                    userResponses = new Dictionary<string, List<(int Predicted, int Actual)>>();

                    // Чтение файла построчно
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] values = line.Split(';');
                        string user = values[1];
                        int Predicted = values[3] == "Да" ? 1 : (values[3] == "Нет" ? 0 : -1);
                        int Actual = int.Parse(values[4]);

                        if (Predicted != -1)
                        {
                            if (!userResponses.ContainsKey(user))
                            {
                                userResponses[user] = new List<(int Predicted, int Actual)>();
                            }
                            userResponses[user].Add((Predicted, Actual));
                            count++;
                        }
                        //if (count == 20) break;
                    }

                    var predicted = new List<double>();
                    var actual = new List<double>();
                    foreach (var responses in userResponses.Values)
                    {
                        foreach (var response in responses)
                        {
                            predicted.Add(response.Predicted);
                            actual.Add(response.Actual);
                        }
                    }

                    Result result = new Result(predicted.ToArray(), actual.ToArray());
                    result.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
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
        public static double[] CalculateUserAccuracies()
        {
            List<double> accuracies = new List<double>();

            foreach (var user in userResponses.Keys)
            {
                var responses = userResponses[user];
        
                // Расчет точности для каждого пользователя
                int correctPredictions = responses.Count(response => response.Predicted == response.Actual);
                int totalPredictions = responses.Count;

                if (totalPredictions > 0)
                {
                    double accuracy = (double)correctPredictions / totalPredictions;
                    accuracies.Add(accuracy);
                }
            }

            return accuracies.ToArray();
        }

    }
}
