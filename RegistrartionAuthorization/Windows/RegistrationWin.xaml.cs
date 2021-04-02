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
    /// Логика взаимодействия для RegistrationWin.xaml
    /// </summary>
    public partial class RegistrationWin : Window
    {
        public RegistrationWin()
        {
            InitializeComponent();


            if (File.Exists("Data.txt") == false)
            {
                File.Create("Data.txt");
            }
        }

        private void btnLogin1_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Data.txt") == true && txtFirstName.Text != "" && txtLastName.Text != " " && txtLogin.Text != "" && txtPassword.Text != "")
            {
                SaveFileClass.FileWriteLine($"{(File.ReadLines("Data.txt").Count()) + 1}@{txtFirstName.Text}@{txtLastName.Text}@{txtLogin.Text}@{txtPassword.Text}", "Data.txt");
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtPassword.Text = "";
                txtLogin.Text = "";

                WorkWin workwin = new WorkWin();
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //else
            //{
            //    MessageBox.Show($"{txtFirstName.Text} {txtLastName.Text} {txtAge.Text}", "Запись создана успешно!");
            //    SaveFileClass.FileWriteLine($"{txtFirstName.Text} {txtLastName.Text} {txtAge.Text}", "Data.txt");
            //}
        }


        private void txtLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int vai;
            if (int.TryParse(e.Text, out vai) || e.Text == "@")
            {
                e.Handled = true;
            }
        }

        private void txtFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int vai;
            if (int.TryParse(e.Text, out vai) || e.Text == "@")
            {
                e.Handled = true;
            }
        }

        private void txtPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "@")
            {
                e.Handled = true;
            }
        }

        private void txtLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "@")
            {
                e.Handled = true;
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWin auth = new AuthorizationWin();
            this.Hide();
            auth.ShowDialog();
            this.Close();
        }
    }
}
