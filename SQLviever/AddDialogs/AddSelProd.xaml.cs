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
    /// Логика взаимодействия для AddSelProd.xaml
    /// </summary>
    public partial class AddSelProd : Window
    {
        private bool isUpdate;
        private int ID;
        public AddSelProd(bool isUp, int _id)
        {
            isUpdate = isUp;
            ID = _id;
            InitializeComponent();
        }

        private void sels_ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                if (sels_ID.Text.Length == 0 || product_ID.Text.Length == 0)
                {
                    MessageBox.Show("Заполните дату проджи и номер клиента!", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection conn = new Connection();
                    conn.InsertSelProd(Convert.ToInt32(sels_ID.Text),Convert.ToInt32(product_ID.Text));
                    conn.Close();
                    DialogResult = true;
                }
            }
            else
            {
                if (sels_ID.Text.Length == 0 || product_ID.Text.Length == 0)
                {
                    MessageBox.Show("Заполните дату проджи и номер клиента! \n(Update)", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection conn = new Connection();
                    conn.UpdateSelProd(Convert.ToInt32(sels_ID.Text), Convert.ToInt32(product_ID.Text),ID);
                    conn.Close();
                    DialogResult = true;
                }
            }
        }
    }
}
