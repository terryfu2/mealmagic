using mealmagic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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


    public class Recipe
    {
        public string RecipeName;
        public int PrepTime;
        public int CookTime;
        public int Servings;
        public int CalsPerServing;
        public string Photo;
        public bool isFav;
        public int userRating;
        public List<Ingredient> Ingredients;
        public Dictionary<int, List<string>> Directions;


        public Recipe(string recipeName, int prepTime, int cookTime, int servings, int calsPerServing, string photo)
        {
            RecipeName = recipeName;
            PrepTime = prepTime;
            CookTime = cookTime;
            Servings = servings;                // default servings to show
            CalsPerServing = calsPerServing;
            Photo = photo;
            isFav = false;
            userRating = 0;
            Ingredients = new List<Ingredient>();
            Directions = new Dictionary<int, List<string>>();     // <Recipe Step: <Directions #: Direction>>
        }
    }

    public class Ingredient
    {
        public string IngredientName;
        public double AmtPerServing;
        public int Step;

        public Ingredient(string ingredientName, double atmPerServing, int step)
        {
            IngredientName = ingredientName;
            AmtPerServing = atmPerServing;
            Step = step;
        }
    }


    public partial class RecipePage : Page
    {
        public RecipePage()
        {
            InitializeComponent();

            // create recipes
            Recipe CajunPeppers = initCajunPeppers();
            Recipe SpinichLasagna = initLasagna();


            //// set up page
            //Recipe curretRecipe = CajunPeppers;
            Recipe curretRecipe = SpinichLasagna;

            // set image
            recipeImage.Height = 170;
            string photo = curretRecipe.Photo;
            recipeImage.Source = new BitmapImage(new Uri(photo, UriKind.Relative));

            int totalCals = curretRecipe.Servings * curretRecipe.CalsPerServing;

            recipeTitle.Text = curretRecipe.RecipeName;
            servingCount.Text = curretRecipe.Servings.ToString();
            servingCalories.Content = totalCals.ToString();
            prepTime.Content = curretRecipe.PrepTime.ToString();
            cookTime.Content = curretRecipe.CookTime.ToString();


            // check if favorite
            if (curretRecipe.isFav)
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheartfull.png", UriKind.Relative));
            }
            else
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheart.png", UriKind.Relative));
            }


            // set the global "current recipe"
            Application.Current.Properties["CurrentRecipe"] = curretRecipe;
            Application.Current.Properties["RecipePage"] = this;


            // update page
            updateIngredients(curretRecipe, curretRecipe.Servings);

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            updateFavoriteBtn();

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



        private void ViewRecipeBackBtn(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("", System.UriKind.Relative));
        }



        private void updateFavoriteBtn()
        {
            Recipe currentRecipe = (Recipe)Application.Current.Properties["CurrentRecipe"];


            // check if favorite
            if (currentRecipe.isFav && currentRecipe != null)
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheartfull.png", UriKind.Relative));
            }
            else
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheart.png", UriKind.Relative));
            }
        }



        private void start_Recipe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("RecipePrep.xaml", System.UriKind.Relative));
        }



        private void increaseServing(object sender, RoutedEventArgs e)
        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            string s = servingCount.Text.ToString();
            int i = Convert.ToInt32(s);

            i = i + 1;
            int totalCals = recipe.CalsPerServing * i;

            servingCalories.Content = totalCals.ToString();
            servingCount.Text = i.ToString();
            recipe.Servings = i;
            updateIngredients(recipe, i);

        }



        private void decreaseServing(object sender, RoutedEventArgs e)
        {

            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];
            string s = servingCount.Text.ToString();
            int i = Convert.ToInt32(s);
            int totalCals = 0;

            if (i > 1)
            {
                i = i - 1;
                totalCals = recipe.CalsPerServing * i;
                servingCalories.Content = totalCals.ToString();
                servingCount.Text = i.ToString();
                recipe.Servings = i;
                updateIngredients(recipe, i);
            }
        }



        private void AddRecipeFavs(object sender, RoutedEventArgs e)
        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            AddtoFavsPopUp.IsOpen = true;

            if (!recipe.isFav)
            {
                popUpRecipeTitle.Text = recipe.RecipeName + " ADDED to Favorites!";
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheartfull.png", UriKind.Relative));
                recipe.isFav = true;
            }
            else
            {
                popUpRecipeTitle.Text = recipe.RecipeName + " REMOVED from favorites!";
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheart.png", UriKind.Relative));
                recipe.isFav = false;
            }

        }



        private void UndoAddFav(object sender, RoutedEventArgs e)

        {
            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            if (!recipe.isFav)
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheartfull.png", UriKind.Relative));
                recipe.isFav = true;
            }
            else
            {
                recipeFavBtn.Source = new BitmapImage(new Uri(@"\images\favheart.png", UriKind.Relative));
                recipe.isFav = false;
            }

            Hide_Click(sender, e);

        }



        private void Hide_Click(object sender, RoutedEventArgs e)

        {

            AddtoFavsPopUp.IsOpen = false;
            AddtoCartPopUp.IsOpen = false;

        }


        private void updateIngredients(Recipe recipe, int servings)
        {
            int s = servings;
            int i = 1;

            clearIngredients();

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                // ref[1]
                // print ingredient amount
                var labelName = string.Format("ingAmt{0}", i);
                var label = (Label)this.FindName(labelName);
                double amount = Math.Max(0.5, Math.Round(ingredient.AmtPerServing * s * 2) / 2);          // round to 0.5
                label.Content = amount.ToString("#.#");                                                    // format 1 decimal

                // print ingredient name
                var TextBlockName = string.Format("ingName{0}", i);
                var TextBlock = (TextBlock)this.FindName(TextBlockName);
                TextBlock.Text = ingredient.IngredientName;

                var ButtonName = string.Format("ingCart{0}", i);
                var Button = (Button)this.FindName(ButtonName);

                label.Visibility = Visibility.Visible;
                TextBlock.Visibility = Visibility.Visible;
                Button.Visibility = Visibility.Visible;

                i++;
            }
        }



        private void clearIngredients()
        {
            // clear ingredients
            for (int i = 1; i <= 17; i++)
            {
                var labelName = string.Format("ingAmt{0}", i);
                var label = (Label)this.FindName(labelName);
                label.Content = "";
                label.Visibility = Visibility.Collapsed;

                var TextBlockName = string.Format("ingName{0}", i);
                var TextBlock = (TextBlock)this.FindName(TextBlockName);
                TextBlock.Text = "";
                TextBlock.Visibility = Visibility.Collapsed;

                var ButtonName = string.Format("ingCart{0}", i);
                var Button = (Button)this.FindName(ButtonName);
                Button.Visibility = Visibility.Collapsed;
            }
        }



        private void addToCart(object sender, RoutedEventArgs e)
        {

            AddtoCartPopUp.IsOpen = true;


            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];


            var getIngNum = ((Button)sender).Tag.ToString();
            int ingNum = Int32.Parse(getIngNum);
            var ingTextBlockName = string.Format("ingName{0}", ingNum);
            var ingTextBlock = (TextBlock)this.FindName(ingTextBlockName);
            string ingName = ingTextBlock.Text;

            Ingredient thisIng = recipe.Ingredients.Find(x => x.IngredientName.Contains(ingName));
            string ingAmt = (thisIng.AmtPerServing * recipe.Servings).ToString("#.#");

            AddtoCartPopUpRecipeTitle.Text = ingAmt + " " + ingName + " added to shopping cart!";
        }


        private void addAllIngredients(object sender, RoutedEventArgs e)
        {


            Recipe recipe = (Recipe)Application.Current.Properties["CurrentRecipe"];

            AddtoCartPopUpRecipeTitle.Text = "All Ingredients for " + recipe.RecipeName + " added to shopping cart!";
        }



        private Recipe initCajunPeppers()
        {
            /// CAJUN STUFFED PEPPERS
            /// ref: https://www.allrecipes.com/recipe/38215/cajun-style-stuffed-peppers/
            Recipe CajunPeppers = new Recipe("Cajun Style Stuffed Peppers", 45, 15, 6, 307, "\\images\\CajunStuffedPeppers.jpg");

            // Create ingredients
            Ingredient BellPepper = new Ingredient("Large Bell Peppers", 1, 2);
            Ingredient OliveOil = new Ingredient("Tablespoons Olive Oil", 0.5, 3);
            Ingredient Onion = new Ingredient("Onion, Diced", .167, 3);
            Ingredient Garlic = new Ingredient("Cloves Garlic, Minced", .334, 4);
            Ingredient Oregeno = new Ingredient("Teaspoon Dried Oregano", .0834, 4);
            Ingredient Creole = new Ingredient("Tablespoon Creole Seasoning", .167, 4);
            Ingredient BlackPepper = new Ingredient("Teaspoon Black Pepper", .167, 4);
            Ingredient Shrimp = new Ingredient("Pound Shrimp, Peeled & Deveined", .125, 5);
            Ingredient Sausage = new Ingredient("Andouille Sausage", .25, 5);
            Ingredient Rice = new Ingredient("Cup Long-grain Rice", .167, 6);
            Ingredient Chicken = new Ingredient("Cups Chicken Broth", .4167, 7);
            Ingredient Tomato = new Ingredient("(8 Ounce) Can Tomato Sauce", .167, 7);

            // Add to recipe
            CajunPeppers.Ingredients.Add(BellPepper);
            CajunPeppers.Ingredients.Add(OliveOil);
            CajunPeppers.Ingredients.Add(Onion);
            CajunPeppers.Ingredients.Add(Garlic);
            CajunPeppers.Ingredients.Add(Oregeno);
            CajunPeppers.Ingredients.Add(Creole);
            CajunPeppers.Ingredients.Add(BlackPepper);
            CajunPeppers.Ingredients.Add(Shrimp);
            CajunPeppers.Ingredients.Add(Sausage);
            CajunPeppers.Ingredients.Add(Rice);
            CajunPeppers.Ingredients.Add(Chicken);
            CajunPeppers.Ingredients.Add(Tomato);

            // Create Directions    
            // step 1
            List<string> step1 = new List<string>() {
                "Preheat oven to 325 degrees F (165 degrees C)",
                "Grease an 8x12 inch baking dish",
                "Bring a large pot of water to a boil"};
            CajunPeppers.Directions.Add(1, step1);

            List<string> step2 = new List<string>() {
                "Remove Tops and Seeds from Peppers",
                "Place in boiling water for 3 minutes",
                "Dry on paper towels"};
            CajunPeppers.Directions.Add(2, step2);

            List<string> step3 = new List<string>() {
                "Heat olive oil in a large skillet over medium heat)",
                "Saute onion (until translucent)"};
            CajunPeppers.Directions.Add(3, step3);

            List<string> step4 = new List<string>() {
                "Stir in Garlic",
                "Season with Oregeno",
                "Add Creole Seasoning",
                "Season with Black Pepper to taste" };
            CajunPeppers.Directions.Add(4, step4);

            List<string> step5 = new List<string>() {
                "Add Shrimp",
                "Stir in Sausage",
                "Cook until shrimp turns pink (~5 mins)"};
            CajunPeppers.Directions.Add(5, step5);

            List<string> step6 = new List<string>() {
                "Stir in Rice",
                "Cook 1 minute" };
            CajunPeppers.Directions.Add(6, step6);

            List<string> step7 = new List<string>() {
                "Pour in Chicken Broth",
                "Pour in Tomato Sauce",
                "Cook until think (15-20 mins)", };
            CajunPeppers.Directions.Add(7, step7);

            return CajunPeppers;
        }


        private Recipe initLasagna()
        {
            /// Spinich Lasagna
            /// ref: https://www.allrecipes.com/recipe/22729/spinach-lasagna-iii/
            Recipe SpinichLasagna = new Recipe("Spinich Lasagna", 20, 75, 12, 289, "\\images\\SpinichLasagna.png");

            // Create ingredients
            Ingredient LasagnaNoodles = new Ingredient("Lasagna Noodles", 1.25, 2);
            Ingredient OliveOil = new Ingredient("Tablespoons Olive Oil", 0.167, 3);
            Ingredient Mushrooms = new Ingredient("Cup Chopped Mushrooms", 0.083, 3);
            Ingredient Onion = new Ingredient("Cup Chopped Onion", .083, 3);
            Ingredient Garlic = new Ingredient("Tablespoon Garlic, Minced", .083, 3);
            Ingredient Spinich = new Ingredient("Cups Spinich", .167, 4);
            Ingredient RicottaCheese = new Ingredient("Cups Ricotta Cheese", .25, 5);
            Ingredient RomanoCheese = new Ingredient("Cups Grated Romano Cheese", .0556, 5);
            Ingredient Egg = new Ingredient("Egg", .083, 5);
            Ingredient Salt = new Ingredient("Teaspoon Salt", .083, 5);
            Ingredient Oregeno = new Ingredient("Teaspoon Dried Oregano", .083, 5);
            Ingredient Basil = new Ingredient("Tablespoon Dried Basil Leaves", .083, 5);
            Ingredient BlackPepper = new Ingredient("Teaspoon Black Pepper", .0417, 5);
            Ingredient MozzarellaCheese = new Ingredient("Cups Shredded Mozzarella Cheese", .25, 6);
            Ingredient PastaSauce = new Ingredient("Cups Pasta Sauce", .25, 6);
            Ingredient ParmesanCheese = new Ingredient("Cup Parmesan Cheese", .083, 6);

            // Add to recipe
            SpinichLasagna.Ingredients.Add(LasagnaNoodles);
            SpinichLasagna.Ingredients.Add(OliveOil);
            SpinichLasagna.Ingredients.Add(Mushrooms);
            SpinichLasagna.Ingredients.Add(Onion);
            SpinichLasagna.Ingredients.Add(Garlic);
            SpinichLasagna.Ingredients.Add(Spinich);
            SpinichLasagna.Ingredients.Add(RicottaCheese);
            SpinichLasagna.Ingredients.Add(RomanoCheese);
            SpinichLasagna.Ingredients.Add(Egg);
            SpinichLasagna.Ingredients.Add(Salt);
            SpinichLasagna.Ingredients.Add(Oregeno);
            SpinichLasagna.Ingredients.Add(Basil);
            SpinichLasagna.Ingredients.Add(BlackPepper);
            SpinichLasagna.Ingredients.Add(MozzarellaCheese);
            SpinichLasagna.Ingredients.Add(PastaSauce);
            SpinichLasagna.Ingredients.Add(ParmesanCheese);

            // Create Directions    
            // step 1
            List<string> step1 = new List<string>() {
                "Preheat oven to 325 degrees F (165 degrees C)",
                "Grease an 8x12 inch baking dish",
                "Bring a large pot of water to a boil"};
            SpinichLasagna.Directions.Add(1, step1);

            List<string> step2 = new List<string>() {
                "Remove Tops and Seeds from Peppers",
                "Place in boiling water for 3 minutes",
                "Dry on paper towels"};
            SpinichLasagna.Directions.Add(2, step2);

            List<string> step3 = new List<string>() {
                "Heat olive oil in a large skillet over medium heat)",
                "Saute onion (until translucent)"};
            SpinichLasagna.Directions.Add(3, step3);

            List<string> step4 = new List<string>() {
                "Stir in Garlic",
                "Season with Oregeno",
                "Add Creole Seasoning",
                "Season with Black Pepper to taste" };
            SpinichLasagna.Directions.Add(4, step4);

            List<string> step5 = new List<string>() {
                "Add Shrimp",
                "Stir in Sausage",
                "Cook until shrimp turns pink (~5 mins)"};
            SpinichLasagna.Directions.Add(5, step5);

            List<string> step6 = new List<string>() {
                "Stir in Rice",
                "Cook 1 minute" };
            SpinichLasagna.Directions.Add(6, step6);

            List<string> step7 = new List<string>() {
                "Pour in Chicken Broth",
                "Pour in Tomato Sauce",
                "Cook until think (15-20 mins)", };
            SpinichLasagna.Directions.Add(7, step7);

            return SpinichLasagna;
        }
    }
}


// [1] https://stackoverflow.com/questions/16920027/looping-over-xaml-defined-labels
