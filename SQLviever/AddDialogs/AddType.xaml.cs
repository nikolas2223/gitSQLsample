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
    /// Логика взаимодействия для AddType.xaml
    /// </summary>
    public partial class AddType : Window
    {
        public bool type;
        private bool isUpdate;
        private int ID;
        public AddType(bool isUp, int _id)
        {
            isUpdate = isUp;
            ID = _id;
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if(it0.IsSelected)
            {
                type = true;
            }
            else
            {
                type = false;
            }

            if (!isUpdate)
            {
                if (description.Text.Length > 0 && description.Text[0] == ' ' || description.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые поля! \n Нельзя начинать строку с пробела.", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection con = new Connection();
                    con.InsertType(description.Text, type);
                    con.Close();
                    DialogResult = true;
                }
            }
            else
            {
                if (description.Text.Length > 0 && description.Text[0] == ' ' || description.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые поля! \n Нельзя начинать строку с пробела.", " WTF?!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Connection con = new Connection();
                    con.UpdateType(description.Text, type,ID);
                    con.Close();
                    DialogResult = true;
                }
            }
        }
    }
}
