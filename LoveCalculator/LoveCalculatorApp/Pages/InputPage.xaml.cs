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
using LoveCalculatorApp.LoveMagic;

namespace LoveCalculatorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для InputPage.xaml
    /// </summary>
    public partial class InputPage : Page
    {
        public InputPage()
        {
            InitializeComponent();
        }

        // hide fields used for combining
        public void ShowMatch()
        {
            secondDateLabel.Visibility = Visibility.Collapsed;
            secondDatePicker.Visibility = Visibility.Collapsed;
            CombineButton.Visibility = Visibility.Collapsed;
        }

        // hide fields used for matching
        public void ShowCombine()
        {
            MatchButton.Visibility = Visibility.Collapsed;
        }

        private void Combine_OnClick(object sender, RoutedEventArgs e)
        {
            Matcher matcher = new Matcher();

            DateTime? first = firstDatePicker.SelectedDate;
            DateTime? second = secondDatePicker.SelectedDate;
            if (first == null || second == null) return;

            double res = matcher.GetCompatibilityPercentage(first.Value, second.Value);

            OutputPage op = new OutputPage();
            op.ShowCompatibility(res);
            op.Message = $"Those who were born at {first.Value.ToString("MMMM", CultureInfo.InvariantCulture)} {first.Value.Day}" +
                         $" and {second.Value.ToString("MMMM", CultureInfo.InvariantCulture)} {second.Value.Day}" +
                         $" are compatible by {res:f2}%" +
                         $"\n\nvia Love Calculator";

            //set new page
            ((NavigationWindow) this.Parent).Content = op;
        }

        private void Match_OnClick(object sender, RoutedEventArgs e)
        {
            Matcher matcher = new Matcher();

            DateTime? person = firstDatePicker.SelectedDate;
            if (person == null) return;

            DateTime res = matcher.GetPerfectMatch(person.Value);

            OutputPage op = new OutputPage();
            op.Message =
                $"Perfect match for person who was born {person.Value.ToString("MMMM", CultureInfo.InvariantCulture)} {person.Value.Day}" +
                $" is person, who was born {res.ToString("MMMM", CultureInfo.InvariantCulture)} {res.Day}" +
                $"\n\nvia Love Calculator";
            op.ShowMatch(res);

            //set new page
            ((NavigationWindow) this.Parent).Content = op;
        }
    }
}