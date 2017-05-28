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
    /// Логика взаимодействия для AddSells.xaml
    /// </summary>
    public partial class AddSells : Window
    {
        private bool isUpdate;
        private int ID;
        public AddSells(bool isUp, int _id)
        {
            ID = _id;
            isUpdate = isUp;
            InitializeComponent();
        }

        private void client_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                if (dateSell.Text.Length == 0 || client_id.Text.Length == 0)
                {
                    MessageBox.Show("Заполните дату проджи и номер клиента!", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection conn = new Connection();
                    if (dateDelivery.Text.Length != 0 && count.Text.Length == 0)
                    {
                        conn.InsertSells(Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text), Convert.ToDateTime(dateDelivery.SelectedDate));
                    }
                    else if (dateDelivery.Text.Length != 0 && count.Text.Length != 0)
                    {
                        conn.InsertSells(Convert.ToInt32(count.Text), Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text), Convert.ToDateTime(dateDelivery.SelectedDate));
                    }
                    else if (dateDelivery.Text.Length == 0 && count.Text.Length != 0)
                    {
                        conn.InsertSells(Convert.ToInt32(count.Text), Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text));
                    }
                    else
                    {
                        conn.InsertSells(Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text));
                    }
                    conn.Close();
                    DialogResult = true;
                }
            }
            else
            {
                if (dateSell.Text.Length == 0 || client_id.Text.Length == 0)
                {
                    MessageBox.Show("Заполните дату проджи и номер клиента!(update)", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection conn = new Connection();
                    if (dateDelivery.Text.Length != 0 && count.Text.Length == 0)
                    {
                        conn.UpdateSells(Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text), Convert.ToDateTime(dateDelivery.SelectedDate),ID);
                    }
                    else if (dateDelivery.Text.Length != 0 && count.Text.Length != 0)
                    {
                        conn.UpdateSells(Convert.ToInt32(count.Text), Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text), Convert.ToDateTime(dateDelivery.SelectedDate),ID);
                    }
                    else if (dateDelivery.Text.Length == 0 && count.Text.Length != 0)
                    {
                        conn.UpdateSells(Convert.ToInt32(count.Text), Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text), ID);
                    }
                    else
                    {
                        conn.UpdateSells(Convert.ToDateTime(dateSell.SelectedDate), Convert.ToInt32(client_id.Text),ID);
                    }
                    conn.Close();
                    DialogResult = true;
                }
            }
        }

        private void count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
