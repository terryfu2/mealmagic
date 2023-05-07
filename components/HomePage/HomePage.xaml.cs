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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
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

        private void Filter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Recommended_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("RecipePage.xaml", System.UriKind.Relative));
        }
    }
}



