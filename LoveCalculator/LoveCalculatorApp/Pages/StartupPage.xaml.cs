using System;
using System.Collections.Generic;
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

namespace LoveCalculatorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartupPage.xaml
    /// </summary>
    public partial class StartupPage : Page
    {
        public StartupPage()
        {
            InitializeComponent();
        }

        private void Compatibility_OnClick(object sender, RoutedEventArgs e)
        {
            InputPage ip = new InputPage();
            ip.ShowCombine();
            ((NavigationWindow)this.Parent).Content = ip;
        }

        private void PerfectMatch_OnClick(object sender, RoutedEventArgs e)
        {
            InputPage ip = new InputPage();
            ip.ShowMatch();
            ((NavigationWindow)this.Parent).Content = ip;
        }

        private void StartupPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            // setting window size 
            NavigationWindow nw = ((NavigationWindow)this.Parent);
            nw.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
