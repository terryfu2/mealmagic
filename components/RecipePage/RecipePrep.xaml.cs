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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mealmagic
{


    public partial class RecipePrep : Page
    {
        public RecipePrep()
        {
            InitializeComponent();

            int step = 1;
            Application.Current.Properties["CurrentStep"] = step;

            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            string photo = recipe.Photo;
            recipeImage.Height = 100;
            recipeImage.Source = new BitmapImage(new Uri(photo, UriKind.Relative));

            int totalCals = recipe.Servings * recipe.CalsPerServing;
            servingCalories.Content = totalCals.ToString();
            prepTime.Content = recipe.PrepTime.ToString(); 
            cookTime.Content = recipe.CookTime.ToString();

            updatePage(recipe, step);
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



        private void updatePage(Recipe recipe, int step)
        {
            clearPage();
            fillIngredients(recipe, step);
            fillDirections(recipe, step);

            int maxSteps = recipe.Directions.Count;

            currentStep.Content = step;
            totalSteps.Content = maxSteps;

            recipeProgressBar.Maximum = maxSteps;
            recipeProgressBar.Value = step;
        }



        private void RecipePrepBackBtn(object sender, RoutedEventArgs e)
        {
            Page prevPage = (Page)Application.Current.Properties["RecipePage"];

            int step = (int)Application.Current.Properties["CurrentStep"];
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            if (--step >= 1)
            {
                updatePage(recipe, step);
                Application.Current.Properties["CurrentStep"] = step;
            }
            else
            {
                this.NavigationService.Navigate(prevPage);
            }
        }



        private void RecipePrepForwardBtn(object sender, RoutedEventArgs e)
        {
            int step = (int)Application.Current.Properties["CurrentStep"];
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            if (++step <= recipe.Directions.Count)
            {
                updatePage(recipe, step);
                Application.Current.Properties["CurrentStep"] = step;
            }
            else 
            {
                finishRecipe();
            }
        }



        private void fillIngredients(Recipe recipe, int step)
        {
            int s = recipe.Servings;
            int i = 1;

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                if (ingredient.Step == step)
                {
                    // ref[1]
                    // print ingredient amount
                    var labelName = string.Format("ingAmt{0}", i);
                    var label = (Label)this.FindName(labelName);
                    double amount = Math.Max(0.5, Math.Round(ingredient.AmtPerServing * s * 2) / 2);          // round to 0.5
                    label.Content = amount.ToString("#.#");                                                 // format 1 decimal
                    label.Visibility = Visibility.Visible;

                    // print ingredient name
                    var TextBlockName = string.Format("ingName{0}", i);
                    var TextBlock = (TextBlock)this.FindName(TextBlockName);
                    TextBlock.Text = ingredient.IngredientName;
                    TextBlock.Visibility = Visibility.Visible;

                    i++;
                }
                prepIngAmt.Content = i - 1;
            }
        }



        private void fillDirections(Recipe recipe, int step)
        {

            List<string> directions = recipe.Directions[step];              

            int i = 1;
                      
            foreach (string dir in directions)
            {
                var DirTextBlockName = string.Format("Dir{0}", i);
                var DirNumLabelName = string.Format("dirNum{0}", i);
                var DirCheckboxName = string.Format("dirCheck{0}", i);

                var DirTextBlock = (TextBlock)this.FindName(DirTextBlockName);
                var DirNumLabel = (Label)this.FindName(DirNumLabelName);
                var DirCheckbox = (Viewbox)this.FindName(DirCheckboxName);

                DirTextBlock.Text = dir;
                DirTextBlock.Height = 65;

                DirNumLabel.Content = i + ".";
                DirNumLabel.Height = 65;

                DirTextBlock.Visibility = Visibility.Visible;
                DirNumLabel.Visibility = Visibility.Visible;
                DirCheckbox.Visibility = Visibility.Visible;

                i++;
            }
            prepDirAmt.Content = i-1;
        }



        private void clearPage()
        {
            // clear ingredients
            for (int i = 1; i <= 5; i++)
            {
                var labelName = string.Format("ingAmt{0}", i);
                var label = (Label)this.FindName(labelName);
                label.Content = "";     
                label.Visibility = Visibility.Collapsed;

                var TextBlockName = string.Format("ingName{0}", i);
                var TextBlock = (TextBlock)this.FindName(TextBlockName);
                TextBlock.Text = "";
                TextBlock.Visibility = Visibility.Collapsed;
            }

            // clear directions
            for (int i = 1; i <= 5; i++)
            {
                var DirTextBlockName = string.Format("Dir{0}", i);
                var DirNumLabelName = string.Format("dirNum{0}", i);
                var DirCheckboxName = string.Format("dirCheck{0}", i);
                var DirCBName = string.Format("cb{0}", i);


                var DirTextBlock = (TextBlock)this.FindName(DirTextBlockName);
                var DirNumLabel = (Label)this.FindName(DirNumLabelName);
                var DirCheckbox = (Viewbox)this.FindName(DirCheckboxName);
                var DirCB = (CheckBox)this.FindName(DirCBName);

                DirTextBlock.Text = "";
                DirNumLabel.Content = "";

                DirTextBlock.Visibility = Visibility.Collapsed;
                DirNumLabel.Visibility = Visibility.Collapsed;
                DirCheckbox.Visibility = Visibility.Collapsed;

                DirCB.IsChecked = false;
            }
        }



        private void finishRecipe()
        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];
            
            FinishedRecipePopUp.IsOpen = true;
            FinishedRecipePopUpRecipeTitle.Text = recipe.RecipeName + " Recipe Complete!";

            int rating = recipe.userRating;

            for (int i = 1; i <= rating; i++)
            {
                var RatingStarName = string.Format("rate{0}", i);
                var RatingStar = (Image)this.FindName(RatingStarName);
                RatingStar.Source = new BitmapImage(new Uri(@"\images\starfull.png", UriKind.Relative));
                recipe.userRating = rating;
            }
            for (int i = rating + 1; i <= 5; i++)
            {
                var RatingStarName = string.Format("rate{0}", i);
                var RatingStar = (Image)this.FindName(RatingStarName);
                RatingStar.Source = new BitmapImage(new Uri(@"\images\star.png", UriKind.Relative));
            }

            if (recipe.isFav)
            {
                     FinishedRecipeFavButton.Content = "View Favorites";
            }
            else
            {
                FinishedRecipeFavButton.Content = "Add to Favorites!";
            }
        }



        private void Back_to_Ingredients(object sender, RoutedEventArgs e)
        {
            Page IngredientPage = (Page)Application.Current.Properties["RecipePage"];
            this.NavigationService.Navigate(IngredientPage);
        }



        private void RateRecipe(object sender, RoutedEventArgs e)
        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            var ratingS = ((Button)sender).Tag.ToString();
            int rating = Int32.Parse(ratingS);

            if (rating == recipe.userRating)
            {
                // clear the rating
                for (int i = 1; i <= 5; i++)
                {
                    var RatingStarName = string.Format("rate{0}", i);
                    var RatingStar = (Image)this.FindName(RatingStarName);
                    RatingStar.Source = new BitmapImage(new Uri(@"\images\star.png", UriKind.Relative));
                    recipe.userRating = 0;
                }
            }
            else
            {
                for (int i = 1; i <= rating; i++)
                {
                    var RatingStarName = string.Format("rate{0}", i);
                    var RatingStar = (Image)this.FindName(RatingStarName);
                    RatingStar.Source = new BitmapImage(new Uri(@"\images\starfull.png", UriKind.Relative));
                    recipe.userRating = rating;
                }
                for (int i = rating + 1; i <= 5; i++)
                {
                    var RatingStarName = string.Format("rate{0}", i);
                    var RatingStar = (Image)this.FindName(RatingStarName);
                    RatingStar.Source = new BitmapImage(new Uri(@"\images\star.png", UriKind.Relative));
                }
            }
        }



        private void FinisedRecipeFavBtn(object sender, RoutedEventArgs e)
        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            if (!recipe.isFav)
            {
                FinishedRecipePopUpRecipeTitle.Text = recipe.RecipeName + " ADDED to favorites!";
                FinishedRecipeFavButton.Content = "View Favorites";
                recipe.isFav = true;
            }
            else
            {
                this.NavigationService.Navigate(new Uri("FavouritesPage.xaml", System.UriKind.Relative));
            }
        }



        private void Hide_Click(object sender, RoutedEventArgs e)

        {
            FinishedRecipePopUp.IsOpen = false;
        }
    }
}


