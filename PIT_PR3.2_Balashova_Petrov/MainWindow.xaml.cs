using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PIT_PR3._2_Balashova_Petrov
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClearInputs();
            Closing += new CancelEventHandler(MainWindow_Closing); 
        }

        private void ClearInputs()
        {
            Operand1.Text = string.Empty;
            Operand2.Text = string.Empty;
            ResultBox.Text = string.Empty;

            Add.IsChecked = false;
            Subtract.IsChecked = false;
            Multiply.IsChecked = false;
            Divide.IsChecked = false;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Operand1.Text) || string.IsNullOrWhiteSpace(Operand2.Text))
            {
                MessageBox.Show("Ошибка: заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double operand1, operand2;

            if (!double.TryParse(Operand1.Text, out operand1) || !double.TryParse(Operand2.Text, out operand2))
            {
                MessageBox.Show("Ошибка: введите корректные числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double result = 0;

            if (Add.IsChecked == true)
            {
                result = operand1 + operand2;
            }
            else if (Subtract.IsChecked == true)
            {
                result = operand1 - operand2;
            }
            else if (Multiply.IsChecked == true)
            {
                result = operand1 * operand2;
            }
            else if (Divide.IsChecked == true)
            {
                if (operand2 == 0)
                {
                    MessageBox.Show("Ошибка: деление на ноль невозможно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                result = operand1 / operand2;
            }
            else
            {
                MessageBox.Show("Ошибка: выберите операцию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ResultBox.Text = result.ToString();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != "-" && e.Text != ",")
            {
                e.Handled = true;
            }
            if (e.Text == "-" && ((sender as TextBox).Text.Contains("-") || (sender as TextBox).Text.Length > 0))
            {
                e.Handled = true;
            }
            if (e.Text == "," && ((sender as TextBox).Text.Contains(",") || (sender as TextBox).Text.Length == 0))
            {
                e.Handled = true;
            }
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; 
            }
            else
            {
                MessageBox.Show("До встречи!", "Прощание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
