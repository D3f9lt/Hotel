using Hotel.AppData;
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

namespace Hotel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

            BlockingUserByDate();
        }

        private void EntryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validation() == true)
            {
                Authentification();
                Authorization();
            }
        }
        public bool Validation()
        {
            if (string.IsNullOrEmpty(LoginTb.Text) && string.IsNullOrEmpty(PasswordPb.Password))
            {
                Feedback.Warning("Поля для ввода не ьдолжны быть пустыми. Введите логин и пароль!");
                return false;
            }
            else if (string.IsNullOrEmpty(LoginTb.Text))
            {
                Feedback.Warning("Поля для ввода логина не должны быть пустыми. Введите логин!"); return false;
            }
            else if (string.IsNullOrEmpty(PasswordPb.Password))
            {
                Feedback.Warning("Поля для ввода пароля не должны быть пустыми.Введите пароль!");
                return false;
            }
            return true;
        }

        public void Authentification()
        {
            App.currentUser = App.context.User.FirstOrDefault(user => user.Login == LoginTb.Text && user.Password == PasswordPb.Password); if (App.currentUser == null)
            {
                Feedback.Error("Вы ввели неверный логин или пароль. Пожалуйста проверьте еще раз введенные данные!");
            }
            else if (App.currentUser.IsBlocked == true)
            {
                Feedback.Error("Вы заблокированы. Обратитесь к администратору!");
            }
            else if (App.currentUser.IsActivated == false)
            {
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
                changePasswordWindow.ShowDialog();
            }
            else
            {
                Feedback.Information("Вы успешно авторизировались!");
            }
        }

        public void Authorization()
        {
            switch (App.currentUser.RoleId)
            {
                case 1:
                    AdministratorWindow adminisrtatorWindow = new AdministratorWindow();
                    adminisrtatorWindow.Show();
                    break;
                case 2: UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                    break;
                default:
                    Feedback.Error("Роль пользователя не найдена! Доступ запрещён.");
                    break;
            }

            Close();
        }

        public void BlockingUserByDate()
        {
            foreach (var user in App.context.User.Where(u => u.RoleId == 2))
            {
                if (user.RegistrationDate.AddMonths(1) < DateTime.Now.Date && !user.IsActivated)
                {
                    user.IsBlocked = true;
                }
            }

            App.context.SaveChanges();
        }
    }
}
