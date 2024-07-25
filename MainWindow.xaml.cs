using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string filePath;
        static public double significance_level = 0.01;

        public MainWindow()
        {
            InitializeComponent();
        }

        private OpenFileDialog fileDialog;
        private void OpenFile_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fileDialog = new OpenFileDialog();
                
                fileDialog.Title = "Выберите файл";
                fileDialog.Filter = "All Files|*.*"; 

        
                bool? result = fileDialog.ShowDialog();

                if (result == true)
                {
                    FilePathTextBlock.Text = fileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenFile_OnMouseEnter(object sender, MouseEventArgs e)
        {
            OpenFile.Background = Brushes.LightBlue;
        }

        private void OpenFile_OnMouseLeave(object sender, MouseEventArgs e)
        {
            OpenFile.Background = Brushes.Lavender;
        }

        private void LevelBorder_OnMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ThicknessAnimation moveRightAnimation = new ThicknessAnimation
                {
                    From = LevelGrid.Margin,
                    Duration = new Duration(TimeSpan.FromSeconds(0.25)) // Длительность анимации
                };

                if (significance_level == 0.01)
                {
                    moveRightAnimation.To = new Thickness(75, 0, 0, 0); // Новое значение Margin
                    significance_level = 0.05;
                }
                else
                {
                    moveRightAnimation.To = new Thickness(0, 0, 0, 0); // Новое значение Margin
                    significance_level = 0.01;
                }

                // Создаем Storyboard и добавляем анимацию
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(moveRightAnimation);

                // Устанавливаем цель анимации
                Storyboard.SetTarget(moveRightAnimation, LevelGrid);
                Storyboard.SetTargetProperty(moveRightAnimation, new PropertyPath(Grid.MarginProperty));

                // Запускаем анимацию
                storyboard.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при изменении уровня значимости: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartBorder_OnMouseEnter(object sender, MouseEventArgs e)
        {
            StartBorder.Background = Brushes.Green;
        }

        private void StartBorder_OnMouseLeave(object sender, MouseEventArgs e)
        {
            StartBorder.Background = Brushes.LightGreen;
        }

        private void StartBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                filePath = FilePathTextBlock.Text;
                T_Test.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выполнении теста: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
