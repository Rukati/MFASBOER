using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WpfApplication1
{
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Проверяем, есть ли данные в словаре
            if (T_Test.userResponses == null || !T_Test.userResponses.Any())
            {
                UsersDataGrid.ItemsSource = new List<UserDataRow>(); // Пустой список, если данных нет
                return;
            }

            // Преобразуем словарь в список для отображения
            var data = T_Test.userResponses.Select(user => new UserDataRow
            {
                Name = user.Key,
                Accuracy = CalculateAccuracy(user.Value),
                GptAccuracy = CalculateGptAccuracy(user.Value)
            }).ToList();

            UsersDataGrid.ItemsSource = data;
        }

        private double CalculateAccuracy(List<(int Predicted, int Actual)> responses)
        {
            int correctPredictions = responses.Count(response => response.Predicted == response.Actual);
            int totalPredictions = responses.Count;
            return totalPredictions > 0 ? (double)correctPredictions / totalPredictions : 0;
        }

        private double CalculateGptAccuracy(List<(int Predicted, int Actual)> responses)
        {
            // Пример расчета, замените на вашу логику для GPT
            int correctPredictions = responses.Count(response => response.Predicted == response.Actual);
            int totalPredictions = responses.Count;
            return totalPredictions > 0 ? (double)correctPredictions / totalPredictions : 0;
        }
    }

    public class UserDataRow
    {
        public string Name { get; set; }
        public double Accuracy { get; set; }
        public double GptAccuracy { get; set; }
    }
}