using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LoveCalculatorApp.VKInteraction;

namespace LoveCalculatorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для OutputPage.xaml
    /// </summary>
    public partial class OutputPage : Page
    {
        public OutputPage()
        {
            InitializeComponent();
        }
        public string Message { get; set; }
        public void ShowCompatibility(double percent)
        {
            MatchLabel.Visibility = Visibility.Collapsed;
            ResultLabel.Content = percent.ToString("f2") + "%";
        }

        public void ShowMatch(DateTime match)
        {
            CompLabel.Visibility = Visibility.Collapsed;
            ResultLabel.Content = $"{match.Day} {match.ToString("MMMM", CultureInfo.InvariantCulture)}";
        }

        private void Share_OnClick(object sender, RoutedEventArgs e)
        {
            VKLoginWindow vkLogin = new VKLoginWindow
            {
                Message = Message,
                Background = Background
            };
            
            vkLogin.ShowDialog();
        }

        private void Repeat_OnClick(object sender, RoutedEventArgs e)
        {
            ((NavigationWindow)this.Parent).Content = new StartupPage();
        }
    }
}
