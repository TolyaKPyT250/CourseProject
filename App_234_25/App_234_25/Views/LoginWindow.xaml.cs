using App_234_25.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace App_234_25.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            StatusText.Text = "Проверка...";
            StatusText.Foreground = Brushes.Gray;

            using (user25Entities context = new user25Entities())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                    if (user != null)
                    {
                        StatusText.Text = "Доступ разрешен!";
                        StatusText.Foreground = Brushes.Green;

                        // Небольшая задержка перед закрытием для красоты
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        ShowLoginError("Неверный логин или пароль");
                    }
                }
                catch (Exception ex)
                {
                    ShowLoginError("Ошибка сети/БД");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        // Перетаскивание окна
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Свернуть
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Закрыть
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SignIn_Click(this, new RoutedEventArgs());
            }
        }
        private void ShowLoginError(string msg)
        {
            StatusText.Text = msg;
            StatusText.Foreground = Brushes.Red;

            // Запуск анимации из ресурсов LoginCard
            if (LoginCard.Resources["ErrorAnimation"] is Storyboard sb)
            {
                sb.Begin();
            }
        }
    }
}
