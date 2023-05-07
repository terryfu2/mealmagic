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
using System.Windows.Shapes;

namespace mealmagic
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShoppingPage : Page
    {
        public ShoppingPage()
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            CustomInputDialog dialog = new CustomInputDialog();
            dialog.Owner = this; // Set the owner of the CustomInputDialog to Window1

            // Calculate the position to center the CustomInputDialog on top of Window1
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            dialog.Left = this.Left + (this.Width - dialog.Width) / 2;
            dialog.Top = this.Top + (this.Height - dialog.Height) / 2;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string userInput = dialog.UserInput;
                if (!string.IsNullOrEmpty(userInput))
                {
                    listBoxItems.Items.Add(userInput);
                    listBoxItems.ScrollIntoView(userInput);
                }
            }*/
        }

        private void Dialog_InputTextChanged(object sender, TextChangedEventArgs e)
        {/*
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string userInput = textBox.Text;
                if (!string.IsNullOrEmpty(userInput))
                {
                    listBoxItems.Items.Clear(); // Clears the listBoxItems before adding new item
                    listBoxItems.Items.Add(userInput);
                    listBoxItems.ScrollIntoView(userInput);
                }
            }*/
        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListBoxItem listBoxItem = FindParent<ListBoxItem>(button);
            if (listBoxItem != null)
            {
                int quantity = GetQuantity(listBoxItem);
                quantity++;
                listBoxItem.Tag = quantity;
                UpdateQuantityTextBlock(listBoxItem, quantity);
            }
        }

        private void Decrement_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListBoxItem listBoxItem = FindParent<ListBoxItem>(button);
            if (listBoxItem != null)
            {
                int quantity = GetQuantity(listBoxItem);
                if (quantity > 0)
                {
                    quantity--;
                    listBoxItem.Tag = quantity;
                    UpdateQuantityTextBlock(listBoxItem, quantity);
                }
            }
        }

        private int GetQuantity(ListBoxItem listBoxItem)
        {
            if (listBoxItem != null && listBoxItem.Tag != null && int.TryParse(listBoxItem.Tag.ToString(), out int quantity))
            {
                return quantity;
            }
            return 0;
        }

        private void UpdateQuantityTextBlock(ListBoxItem listBoxItem, int quantity)
        {
            if (listBoxItem != null)
            {
                TextBlock quantityTextBlock = FindChild<TextBlock>(listBoxItem, "quantityTextBlock");
                if (quantityTextBlock != null)
                {
                    quantityTextBlock.Text = quantity.ToString();
                }
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            if (parent is T) return (T)parent;
            return FindParent<T>(parent);
        }

        private static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T childType && (child as FrameworkElement).Name == childName)
                {
                    foundChild = childType;
                    break;
                }
                foundChild = FindChild<T>(child, childName);
                if (foundChild != null) break;
            }
            return foundChild;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}