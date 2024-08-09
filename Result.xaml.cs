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
            
            contentFrame.Navigate(new TTestPage( predicted, actual));
        }
        private void ShowUsersPage(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new UsersPage());
        }

        private void ShowTTestPage(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(new TTestPage( predicted, actual));
        }
    }
}