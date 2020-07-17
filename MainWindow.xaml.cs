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
using MySql.Data.MySqlClient;

namespace TelephoneNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MySqlConnection db;
        public MainWindow()
        {
            InitializeComponent();
            ShowDB();

        }

        private void ShowDB() 
        {
            const string host = "mysql11.hostland.ru";
            const string database = "host1323541_itstep6";
            const string port = "3306";
            const string username = "host1323541_itstep";
            const string pass = "269f43dc";
            const string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;

            db = new MySqlConnection(ConnString);
            db.Open();

            string sql = "SELECT name,surname, TelephoneNumber FROM TelephoneBook";
            var query = new MySqlCommand { Connection = db, CommandText = sql };
            var result = query.ExecuteReader();

            while (result.Read())
            {
                string Name = result.GetString("name") + " ";
                string Surname = result.GetString("surname") + " ";
                string TelephoneNumber = result.GetString("TelephoneNumber") + " ";
                ListBox.Items.Add(Name + Surname + TelephoneNumber);
            }
            result.Close();
            db.Close();
        }

        private void Access_Click(object sender, RoutedEventArgs e)
        {
            db.Open();
            string name = FirstnameInput.Text;
            string surname = SurnameInput.Text;
            string tel = TelephonNamberInput.Text;

            string sql = $"insert TelephoneBook(name, surname, TelephoneNumber) \n values('{name}','{surname}','{tel}');";
            var query = new MySqlCommand { Connection = db, CommandText = sql };
            var result = query.ExecuteNonQuery();
            MessageBox.Show("Внесенны данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            db.Close();
            ListBox.Items.Clear();
            ShowDB();
        }

        private void ButtonSearching_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSearching.Items.Clear();
            string search = BoxSearching.Text;


            bool messageNoFound  =  false;
            const string host = "mysql11.hostland.ru";
            const string database = "host1323541_itstep6";
            const string port = "3306";
            const string username = "host1323541_itstep";
            const string pass = "269f43dc";
            const string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
            db = new MySqlConnection(ConnString);
            db.Open();

            string sql  = $"SELECT * FROM TelephoneBook WHERE name = '{search}' OR surname = '{search}' OR TelephoneNumber = '{search}'";
            var query = new MySqlCommand { Connection = db, CommandText = sql };
            var result = query.ExecuteReader();
            while (result.Read())
            { 
                string Name = result.GetString("name") + " ";
                string Surname = result.GetString("surname") + " ";
                string TelephoneNumber = result.GetString("TelephoneNumber") + " ";
                ListBoxSearching.Items.Add(Name + Surname + TelephoneNumber);
                messageNoFound = true;
            }

            if (!messageNoFound) 
            {
                MessageBox.Show("Нет совпадений по БД", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ListBoxSearching_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if( ListBoxSearching.SelectedIndex!=-1)
            {
                RedactName.Text  =  ListBoxSearching.SelectedItem.ToString();
            }
        }
    }
}
