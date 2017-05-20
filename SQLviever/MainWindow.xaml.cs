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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;

namespace SQLviever
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //while (dr.Read())
            //    Console.WriteLine(dr[2]);

            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Sells.ItemsSource = Base.getSells();
            Base.Close();
        }

        private void butProducts_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Products.ItemsSource = Base.getProducts();
            Base.Close();
        }

        private void butClients_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Clients.ItemsSource = Base.getClients();
            Base.Close();
        }

        private void butSelProd_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            SelProd.ItemsSource = Base.getSelProd();
            Base.Close();
            SelProd.Columns[0].Header = "Номер заказа";
            SelProd.Columns[1].Header = "Номер продажи";
            SelProd.Columns[2].Header = "Номер продукта";
        }

        private void butType_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Type.ItemsSource = Base.getType();
            Base.Close();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butAddClient_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddClients();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butAddType_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddDialogs.AddType();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butAddSelProd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butAddProd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddDialogs.AddProduct();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butAddSells_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
