using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MathNet.Numerics;

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
            
            var data = T_Test.userResponses.Select(user => new UserDataRow
            {
                Name = user.Key,
                Accuracy = CalculateAccuracy(user.Value).ToString("F4") // Форматирование до 4 знаков после запятой
            }).ToList();

            UsersDataGrid.ItemsSource = data;
        }

        private double CalculateAccuracy(List<(int Predicted, int Actual)> responses)
        {
            int correctPredictions = responses.Count(response => response.Predicted == response.Actual);
            int totalPredictions = responses.Count;
            return totalPredictions > 0 ? (double)correctPredictions / totalPredictions : 0;
        }
    }

    public class UserDataRow
    {
        public string Name { get; set; }
        public string Accuracy { get; set; }
    }
}