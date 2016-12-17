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
using System.Windows.Shapes;
using LoveCalculatorApp.VKInteraction;

namespace LoveCalculatorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для VKLoginWindow.xaml
    /// </summary>
    public partial class VKLoginWindow : Window
    {
        public VKLoginWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
        }

        //property to store message to post
        public string Message { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginTB.Text) || string.IsNullOrEmpty(passwordPB.Password)) return;

            Publisher p = new Publisher();
            try
            {
                p.Share(loginTB.Text, passwordPB.Password, Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }
    }
}
