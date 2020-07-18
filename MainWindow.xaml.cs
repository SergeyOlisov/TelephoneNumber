using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime;

namespace TelephoneNumber
{

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
            MySqlDataReader result = ConnectDB("SELECT name, surname, TelephoneNumber FROM TelephoneBook");
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
            ListBox.Items.Clear();
            string search = BoxSearching.Text;
            bool messageNoFound  =  false;
            MySqlDataReader result = ConnectDB($"SELECT * FROM TelephoneBook WHERE name = '{search}' OR surname = '{search}' OR TelephoneNumber = '{search}'");
            while (result.Read())
            { 
                string Name = result.GetString("name") + " ";
                string Surname = result.GetString("surname") + " ";
                string TelephoneNumber = result.GetString("TelephoneNumber") + " ";
                ListBox.Items.Add(Name + Surname + TelephoneNumber);
                messageNoFound = true;
            }
            if (!messageNoFound) 
            {
                MessageBox.Show("Нет совпадений по БД", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private MySqlDataReader ConnectDB(string sql) 
        {
            const string host = "mysql11.hostland.ru";
            const string database = "host1323541_itstep6";
            const string port = "3306";
            const string username = "host1323541_itstep";
            const string pass = "269f43dc";
            const string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
            db = new MySqlConnection(ConnString);
            db.Open();
            var query = new MySqlCommand { Connection = db, CommandText = sql };
            var result = query.ExecuteReader();
            return result;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            ShowDB();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedIndex != -1)
            {
                //FirstnameInput.Text = ListBox.SelectedItem.ToString();
                string tempLine = ListBox.SelectedItem.ToString();
                if (tempLine != null) 
                {
                    tempLine = tempLine.Trim();
                    string[] words = tempLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    /*var index = tempLine.IndexOf(" ");
                    var lastIndex = tempLine.LastIndexOf(" ");
                    FirstnameInput.Text = tempLine.Substring(0, index);
                    SurnameInput.Text = tempLine.Substring(index + 1, lastIndex);
                    TelephonNamberInput.Text = tempLine.Substring(lastIndex + 1, tempLine.Length - 1); */
                }


            }
        }
    }
}
