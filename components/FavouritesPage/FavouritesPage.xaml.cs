using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace mealmagic
{
    /// <summary>
    /// Interaction logic for FavouritesPage.xaml
    /// </summary>
    public partial class FavouritesPage : Page
    {
        public FavouritesPage()
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




        private bool isFilled = false;


        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ImageBrush brush = button.Background as ImageBrush;
            if (brush.ImageSource.ToString().Contains("filledheart.png"))
            {
                brush.ImageSource = new BitmapImage(new Uri("C:\\Users\\aarai\\Source\\Repos\\mealmagic\\mealmagic\\images\\unfilledheart.png", UriKind.Absolute));
            }
            else
            {
                // Add a breakpoint or a debug statement here to see if this code is being executed
                Debug.WriteLine("Changing image to filledheart.png");
                brush.ImageSource = new BitmapImage(new Uri("C:\\Users\\aarai\\Source\\Repos\\mealmagic\\mealmagic\\images\\filledheart.png", UriKind.Absolute));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Back(object sender, RoutedEventArgs e)
        {
            Page prevPage = (Page)Application.Current.Properties["PrevPage"];
            this.NavigationService.Navigate(prevPage, System.UriKind.Relative);
        }

    }
}
