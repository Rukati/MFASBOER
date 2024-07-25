using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows;

namespace WpfApplication1
{
    internal class T_Test
    {
        public static void Run()
        {
            string filePath = MainWindow.filePath;

            try
            {
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                {
                    string line;
                    // Пропустить первую строку (заголовок)
                    streamReader.ReadLine();

                    Dictionary<string, List<(int Predicted, int Actual)>> userResponses = new Dictionary<string, List<(int Predicted, int Actual)>>();

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
                        }
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
    }
}
