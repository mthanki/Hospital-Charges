using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_Charges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
            vm.CreateDirectory();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            vm.Calculate();
        }

        private void Days_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            e.Handled = !int.TryParse(tb.Text + e.Text, out _);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool isDecimal = decimal.TryParse(tb.Text + e.Text, out decimal result);
            if (isDecimal)
            {
                if (result >= 0)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void DarkModeToggle_Changed(object sender, RoutedEventArgs e)
        {
            vm.ChangeTheme();
        }

        private void Recipt_Click(object sender, RoutedEventArgs e)
        {
            vm.GenerateRecipt();
            var message = "Recipt Generated.";
            RecieptSnackbar.MessageQueue.Enqueue(message);
        }

    }
}
