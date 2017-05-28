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

namespace SQLviever
{
    /// <summary>
    /// Логика взаимодействия для AddClients.xaml
    /// </summary>
    public partial class AddClients : Window
    {
        private bool isUpdate;
        private int ID;
        public AddClients(bool isUp, int _id)
        {
            isUpdate = isUp;
            ID = _id;
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                if (fname.Text.Length == 0 || adress.Text.Length == 0 || phone.Text.Length == 0 || name.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые поля!", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection cnn = new Connection();
                    cnn.InsertClients(fname.Text, adress.Text, phone.Text, Convert.ToBoolean(regularity.IsChecked), name.Text);
                    cnn.Close();
                    DialogResult = true;
                }
            }
            else
            {
                if (fname.Text.Length == 0 || adress.Text.Length == 0 || phone.Text.Length == 0 || name.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые поля!(update)", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection cnn = new Connection();
                    cnn.UpdateClients(fname.Text, adress.Text, phone.Text, Convert.ToBoolean(regularity.IsChecked), name.Text,ID);
                    cnn.Close();
                    DialogResult = true;
                }
            }
        }

        private void phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789-".IndexOf(e.Text) < 0;
        }
    }
}
