using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mealmagic
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class FilterPage : Page
    {
        public FilterPage()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["PrevPage"] = Application.Current.Properties["CurrentPage"];
            Application.Current.Properties["CurrentPage"] = this;
        }

        private void navigate(object sender, RoutedEventArgs e)
        {
            string page = ((Button)sender).Tag.ToString();


            switch (page)
            {
                case "Home":
                    this.NavigationService.Navigate(new Uri("HomePage.xaml", System.UriKind.Relative));
                    break;

                case "Trending":
                    this.NavigationService.Navigate(new Uri("TrendingPage.xaml", System.UriKind.Relative));
                    break;

                case "Favorites":
                    this.NavigationService.Navigate(new Uri("FavouritesPage.xaml", System.UriKind.Relative));
                    break;

                case "Shopping":
                    this.NavigationService.Navigate(new Uri("ShoppingPage.xaml", System.UriKind.Relative));
                    break;

                case "Account":
                    this.NavigationService.Navigate(new Uri("AccountPage.xaml", System.UriKind.Relative));
                    break;
            }
        }


        private void CookSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider cookSlider = (Slider)sender;

            var Textbox = (TextBox)this.FindName("currentCookingSliderValue");

            if (Textbox != null)
                Textbox.Text = cookSlider.Value.ToString() + " (mins)";

        }
        private void CalSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider calSlider= (Slider)sender;

            var Textbox = (TextBox)this.FindName("currentCalSliderValue");

            if (currentCalSliderValue != null)
                currentCalSliderValue.Text = calSlider.Value.ToString() + " (Cals)";


        }
     

        private void Back(object sender, RoutedEventArgs e)
        {
            Page prevPage = (Page)Application.Current.Properties["PrevPage"];
            this.NavigationService.Navigate(prevPage, System.UriKind.Relative);
        }

        //private void ratings_Click(object sender, RoutedEventArgs e)
        //{
        //    // Change the background color of the button
        //    ratings.Background = Brushes.Green;
        //}
        private bool isButtonClicked = false;

        private void ratings_Click(object sender, RoutedEventArgs e)
        {

            Button ratings = (Button)sender;

            if (!isButtonClicked)
            {
                ratings.Background = new SolidColorBrush(Colors.Green);
                isButtonClicked = true;
            }
            else
            {
                //ratings.ClearValue(Button.BackgroundProperty);
                //isButtonClicked = false;
                ratings.Background = Brushes.Transparent;
                isButtonClicked = false;
            }
        }

        private bool isButtonClicked1 = false;

        private void calories_Click(object sender, RoutedEventArgs e)
        {

            Button calories = (Button)sender;

            if (!isButtonClicked1)
            {
                calories.Background = new SolidColorBrush(Colors.Blue);
                isButtonClicked1 = true;
            }
            else
            {
                //ratings.ClearValue(Button.BackgroundProperty);
                //isButtonClicked = false;
                calories.Background = Brushes.Transparent;
                isButtonClicked1 = false;
            }
        }

        private bool isButtonClicked2 = false;

        private void lowcarb_Click(object sender, RoutedEventArgs e)
        {
            Button lowcarb = (Button)sender;

            if (!isButtonClicked2)
            {
                lowcarb.Background = new SolidColorBrush(Colors.Red);
                isButtonClicked2 = true;
            }
            else
            {
                lowcarb.Background = Brushes.Transparent;
                isButtonClicked2 = false;
            }
        }

        private bool isButtonClicked3 = false;

        private void vegan_Click(object sender, RoutedEventArgs e)
        {
            Button vegan = (Button)sender;

            if (!isButtonClicked3)
            {
                vegan.Background = new SolidColorBrush(Colors.LightGreen);
                isButtonClicked3 = true;
            }
            else
            {
                vegan.Background = Brushes.Transparent;
                isButtonClicked3 = false;
            }
        }

        private bool isButtonClicked4 = false;

        private void dairy_Click(object sender, RoutedEventArgs e)
        {
            Button dairy = (Button)sender;

            if (!isButtonClicked4)
            {
                dairy.Background = new SolidColorBrush(Colors.DarkBlue);
                isButtonClicked4 = true;
            }
            else
            {
                dairy.Background = Brushes.Transparent;
                isButtonClicked4 = false;
            }
        }

        private bool isButtonClicked5 = false;

        private void gluten_Click(object sender, RoutedEventArgs e)
        {
            Button gluten = (Button)sender;

            if (!isButtonClicked5)
            {
                gluten.Background = new SolidColorBrush(Colors.DeepPink);
                isButtonClicked5 = true;
            }
            else
            {
                gluten.Background = Brushes.Transparent;
                isButtonClicked5 = false;
            }
        }

        private void Apply(object sender, RoutedEventArgs e)
        {

        }
    }
}