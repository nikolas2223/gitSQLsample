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
using System.Data;

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
            Sells.ItemsSource = Base.getSells().OrderBy(x=> x.ID);
            Base.Close();
            Sells.Columns[1].Header = "Заказанное \nКоличество";
            Sells.Columns[2].Header = "Дата продажи";
            Sells.Columns[3].Header = "Клиент \n(Номер, Имя, Фамилия)";
            Sells.Columns[4].Header = "Дата доставки";
        }

        private void butProducts_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Products.ItemsSource = Base.getProducts().OrderBy(x=>x.ID);
            Base.Close();
            Products.Columns[1].Header = "Наименование";
            Products.Columns[2].Header = "Цена";
            Products.Columns[3].Header = "Наличие";
            Products.Columns[4].Header = "Номер типа, товар";
            Products.Columns[5].Header = "Тип товара";
            Products.Columns[6].Header = "Количество на складе";
        }

        private void butClients_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Clients.ItemsSource = Base.getClients().OrderBy(x=>x.ID);
            Base.Close();
            Clients.Columns[0].Header = "ID";
            Clients.Columns[1].Header = "Имя";
            Clients.Columns[2].Header = "Фамилия";
            Clients.Columns[3].Header = "Адрес";
            Clients.Columns[4].Header = "Телефон";
            Clients.Columns[5].Header = "Признак \nпостоянного \nклиента";
        }

        private void butSelProd_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            SelProd.ItemsSource = Base.getSelProd().OrderBy(x=>x.ID);
            Base.Close();
            SelProd.Columns[0].Header = "Номер заказа";
            SelProd.Columns[1].Header = "Номер продажи";
            SelProd.Columns[2].Header = "Продукт и цена";
        }

        private void butType_Click(object sender, RoutedEventArgs e)
        {
            Connection Base = new Connection();
            Type.ItemsSource = Base.getType().OrderBy(x=>x.ID);
            Base.Close();
            Type.Columns[0].Header = "ID";
            Type.Columns[1].Header = "Описание";
            Type.Columns[2].Header = "Тип по \nсправке";
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butAddClient_Click(object sender, RoutedEventArgs e)
        {
            bool isUp = false;
            var dlg = new AddClients();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butAddType_Click(object sender, RoutedEventArgs e)
        {
            bool isUp = false;
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
            var dlg = new AddDialogs.AddProduct(false,-1);
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butAddSells_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddDialogs.AddSells(false,-1);
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butDelSells_Click(object sender, RoutedEventArgs e)
        {
            Sell t = Sells.SelectedItem as Sell;
            Connection Base = new Connection();
            Base.delSells(t.ID);
            Base.Close();
            MessageBox.Show("Продажа с номером: " + t.ID +"\nбыл удалён");
        }

        private void butDelProduct_Click(object sender, RoutedEventArgs e)
        {
            Product t = Products.SelectedItem as Product;
            Connection Base = new Connection();
            Base.delProducts(t.ID);
            Base.Close();
            MessageBox.Show("Товар с номером: " + t.ID + "\nбыл удалён");
        }
        private void butDelClient_Click(object sender, RoutedEventArgs e)
        {
            Client t = Clients.SelectedItem as Client;
            Connection Base = new Connection();
            Base.delClients(t.ID);
            Base.Close();
            MessageBox.Show("Клиент с номером: " + t.ID + "\nбыл удалён");
        }

        private void butDelSelProd_Click(object sender, RoutedEventArgs e)
        {
            SelProd t = SelProd.SelectedItem as SelProd;
            Connection Base = new Connection();
            Base.delSelProd(t.ID);
            Base.Close();
            MessageBox.Show("Отчет с номером: " + t.ID + "\nбыл удалён");
        }

        private void butDelType_Click(object sender, RoutedEventArgs e)
        {
            Type t = Type.SelectedItem as Type;
            Connection Base = new Connection();
            Base.delType(t.ID);
            Base.Close();
            MessageBox.Show("Тип с номером: " + t.ID + "\nбыл удалён");
        }

        private void butUpdateSell_Click(object sender, RoutedEventArgs e)
        {
            Sell t = Sells.SelectedItem as Sell;
            var dlg = new AddDialogs.AddSells(true, t.ID);
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }

        private void butUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            Product t = Products.SelectedItem as Product;
            var dlg = new AddDialogs.AddProduct(true, t.ID);
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show("Очевидно, добавлена строка");
            }
        }
    }
}
