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
    /// Логика взаимодействия для AuthorizationWin.xaml
    /// </summary>
    public partial class AuthorizationWin : Window
    {
        List<UsersClass> UserList = new List<UsersClass>();// создание Листа по классу
        int cpActivate = 0;
        int errorCounter = 0;
        int errorOfRead = 0;

        public AuthorizationWin()
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
                        UserList.Add(new UsersClass
                        {
                            Id = Convert.ToInt32(words[0]),
                            Name = words[1],
                            LastName = words[2],
                            Login = words[3],
                            Password = words[4]
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show("Отсутствует лист, перезапустите программу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (File.Exists("UserData.txt") == true)// защита от дауна
            {
                if (SaveFileClass.FileRead("UserData.txt") != null)
                {
                    try
                    {
                        Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")); // проверка правильности данных в файле
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка сохранения пользователя, повторите вход!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        errorOfRead = 1;
                        File.Delete("UserData.txt");
                    }

                    if (errorOfRead == 0)
                    {
                        if (Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")) > UserList.Count() || Convert.ToInt32(SaveFileClass.FileRead("UserData.txt")) < 0)// Проверка id 
                        {
                            MessageBox.Show("Сохранённый пользователь перестал существовать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            File.Delete("UserData.txt");
                        }
                        else
                        {
                            cbxRemind.IsChecked = true;
                            int saveId = Convert.ToInt32(SaveFileClass.FileRead("UserData.txt"));
                            var user = UserList.Where(u => u.Id == saveId).FirstOrDefault();
                            txtLogin.Text = user.Login;
                            pswPassword.Password = user.Password;
                        }
                    }
                }
            }
            else if (File.Exists("UserData.txt") == false)// Если файла не существует, создаёт файл
            {
                File.Create("UserData.txt");
            }
        }

        private void CapchaGet()//Получает новую капчу и присваивает её текстовому полю! 
        {
            string CpString = "";
            CpString = CapchaGenClass.CapchaGenerate();
            txbCapchaEnter.Text = CpString;
        }

        private void CapchaShow()//открывает капчу на окне
        {
            btnClose1.Visibility = Visibility.Hidden;
            btnLogin1.Visibility = Visibility.Hidden;
            btnClose2.Visibility = Visibility.Visible;
            btnLogin2.Visibility = Visibility.Visible;
            txtCapcha.Visibility = Visibility.Visible;
            txbCapcha.Visibility = Visibility.Visible;
            btnRegistration1.Visibility = Visibility.Hidden;
            btnRegistration2.Visibility = Visibility.Visible;
            txbCapchaEnter.Visibility = Visibility.Visible;
            btnCapchaReboot.Visibility = Visibility.Visible;
            brdCapcha.Visibility = Visibility.Visible;
            this.Height = 600;
        }

        private void Login()// Метод входа в приложение
        {
            var user = UserList.Where(u => u.Login == txtLogin.Text && u.Password == pswPassword.Password).FirstOrDefault();// Поиск по логину и паролю

            if (user != null && txbCapchaEnter.Text.ToLower() == txtCapcha.Text.ToLower())// проверка правильности ввода капчи и пароля
            {
                if (File.Exists("UserData.txt") == true)// Проверка существования файла!
                {
                    if (cbxRemind.IsChecked == true && SaveFileClass.FileRead("UserData.txt") == null && File.Exists("UserData.txt") == true)
                    {
                        SaveFileClass.FileReWrite(Convert.ToString(user.Id), "UserData.txt");// записывает id пользователя в файл
                    }
                    else if (cbxRemind.IsChecked == false)// удаление файла 
                    {
                        File.Delete("UserData.txt");
                    }
                }
                else
                {
                    if (cbxRemind.IsChecked == true && File.Exists("UserData.txt") == false)// Полная шляпа, когда пользователь трогает сраный файл!!!
                    {
                        //using (StreamWriter sr = new StreamWriter("UserData.txt"))
                        //{
                        //    File.Create("UserData.txt");
                        //}
                        //if (cbxRemind.IsChecked == true && FileSaveClass.FileRead("UserData.txt") == null && File.Exists("UserData.txt") == true)
                        //{
                        //    FileSaveClass.FileWrite(Convert.ToString(user.Id), "UserData.txt");// записывает id пользователя в файл
                        //}
                        //else if (cbxRemind.IsChecked == false)// удаление файла 
                        //{
                        //    File.Delete("UserData.txt");
                        //}
                        MessageBox.Show("Внимание! \nИсполняемый файл занят системным процессом! " +
                            "\nПри следующей авторизации вам придётся ещё раз ввести ваши данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                WorkWin workwin = new WorkWin(user); // Переход на рабочее окно
                this.Hide();
                workwin.ShowDialog();
                this.Close();
            }
            else// при неправильном вводе пароля
            {
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                errorCounter++;// счёт ошибок

                if (txbCapchaEnter.Text.ToLower() != txtCapcha.Text.ToLower() && cpActivate == 1)// неправильно введена капча
                {
                    MessageBox.Show("Неправильно введена капча!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (errorCounter > 2)// Открытие капчи при трёх ошибках
                {
                    CapchaShow();
                    cpActivate = 1;
                }
            }

            if (cpActivate == 1)// Получение новой капчи при первом открытии
            {
                CapchaGet();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();// Вход по кнопке
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие по кнопке
        }

        private void btnCapchaReboot_Click(object sender, RoutedEventArgs e)
        {
            CapchaGet(); // Обновление капчи по нажатию кнопки
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)// Выход при нажатии Esc
            {
                this.Close();
            }

            if (e.Key == Key.Enter)// Вход при нажатии Enter
            {
                Login();
            }
        }

        private void btnRegistration1_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWin rgwin = new RegistrationWin();
            this.Hide();
            rgwin.ShowDialog();
            this.Close();
        }
    }
}
