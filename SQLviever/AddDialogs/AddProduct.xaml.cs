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

namespace SQLviever.AddDialogs
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 .".IndexOf(e.Text) < 0;
        }

        private void btn_OK_Click_1(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length == 0 || price.Text.Length == 0 || id_type.Text.Length == 0 || count.Text.Length == 0)
            {
                MessageBox.Show("Заполните все текстовые поля!", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Connection cnn = new Connection();
                cnn.InsertProduct(name.Text, price.Text, Convert.ToBoolean(existing.IsChecked), Convert.ToInt32(id_type.Text), Convert.ToInt32(count.Text));
                cnn.Close();
                DialogResult = true;
            }
        }
    }
}
