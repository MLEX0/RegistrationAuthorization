using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

namespace RegistrartionAuthorization.Windows
{
    /// <summary>
    /// Логика взаимодействия для WorkWin.xaml
    /// </summary>
    public partial class WorkWin : Window
    {
        List<UsersClass> users = new List<UsersClass>();

        public WorkWin()
        {
            InitializeComponent();

            if (File.Exists("Data.txt") == false)
            {
                File.Create("Data.txt");
            }

            if (File.Exists("Data.txt") == true)
            {
                using (StreamReader sr = new StreamReader("Data.txt"))
                {
                    string[] words;
                    for (int i = 0; i < File.ReadLines("Data.txt").Count(); i++)
                    {
                        string str = sr.ReadLine();
                        words = str.Split(new char[] { '@' });
                        users.Add(new UsersClass
                        {
                            Id = Convert.ToInt32(words[0]),
                            Name = words[1],
                            LastName = words[2],
                            Login = words[3],
                            Password = words[4]
                        });
                    }
                }

                DataGrid.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Отсутствует лист, перезапуск программы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        public WorkWin(UsersClass user)
        {
            InitializeComponent();


            if (File.Exists("Data.txt") == false)
            {
                File.Create("Data.txt");
            }

            if (File.Exists("Data.txt") == true)
            {
                using (StreamReader sr = new StreamReader("Data.txt"))
                {
                    string[] words;
                    for (int i = 0; i < File.ReadLines("Data.txt").Count(); i++)
                    {
                        string str = sr.ReadLine();
                        words = str.Split(new char[] { '@' });
                        users.Add(new UsersClass
                        {
                            Id = Convert.ToInt32(words[0]),
                            Name = words[1],
                            LastName = words[2],
                            Login = words[3],
                            Password = words[4]
                        });
                    }
                }

                DataGrid.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Отсутствует лист, перезапуск программы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
