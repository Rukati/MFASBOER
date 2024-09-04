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
        static public string UsePredict = "Users";

        public MainWindow()
        {
            InitializeComponent();
        }

        private OpenFileDialog fileDialog;

        private void LevelBorder_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (((Border)sender).Name == "PredictBorder")
            {
                try
                {
                    ThicknessAnimation moveRightAnimation = new ThicknessAnimation
                    {
                        From = PredictGrid.Margin,
                        Duration = new Duration(TimeSpan.FromSeconds(0.25)) // Длительность анимации
                    };
                    DoubleAnimation widthAnimation = new DoubleAnimation
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(0.25)) 
                    };
                    
                    if (UsePredict == "Users")
                    {
                        moveRightAnimation.To = new Thickness(100, 0, 0, 0); // Новое значение Margin
                        widthAnimation.To = 75;
                        UsePredict = "GPT";
                    }
                    else
                    {
                        moveRightAnimation.To = new Thickness(0, 0, 0, 0); // Новое значение Margin
                        widthAnimation.To = 100;
                        UsePredict = "Users";
                    }

                    // Создаем Storyboard и добавляем анимацию
                    Storyboard storyboard = new Storyboard();
                    storyboard.Children.Add(moveRightAnimation);
                    storyboard.Children.Add(widthAnimation);

                    // Устанавливаем цель анимации
                    Storyboard.SetTarget(moveRightAnimation, PredictGrid);
                    Storyboard.SetTargetProperty(moveRightAnimation, new PropertyPath(Grid.MarginProperty));
                    
                    Storyboard.SetTarget(widthAnimation, PredictGrid);
                    Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Grid.WidthProperty));

                    // Запускаем анимацию
                    storyboard.Begin();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при выборе предсказания: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
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
                    MessageBox.Show($"Произошла ошибка при изменении уровня значимости: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StartAnalysis_OnClick(object sender, RoutedEventArgs e)
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

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {  try
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
    }
}
