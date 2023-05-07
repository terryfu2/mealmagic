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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class CustomInputDialog : Window
    {
        public string UserInput { get; set; }
        public event TextChangedEventHandler InputTextChanged; // Add this line

        public CustomInputDialog()
        {
            InitializeComponent();
            txtInput.TextChanged += TxtInput_TextChanged; // Add this line

        }
        private void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputTextChanged?.Invoke(sender, e);
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            UserInput = txtInput.Text;
            DialogResult = true;
        }

        // Add this missing event handler
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


    }

}
