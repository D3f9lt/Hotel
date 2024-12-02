using Hotel.AppData;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void AddUserBTN_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        public void AddUser()
        {
            try
            {
                if (string.IsNullOrEmpty(FullNameTB.Text) || string.IsNullOrEmpty(LoginTB.Text) || string.IsNullOrEmpty(PasswordPB.Password))
                {
                    Feedback.Warning("Все поля обязательны для заполнения! Заполните каждое поле!");
                }
                else
                {
                    User newUser = new User()
                    {
                        FullName = FullNameTB.Text,
                        Login = LoginTB.Text,
                        Password = PasswordPB.Password,
                        RegistrationDate = DateTime.Now.Date,
                        IsActivated = false,
                        IsBlocked = false,
                        RoleId = 2
                    };

                    App.context.User.Add(newUser);
                    App.context.SaveChanges();
                    Feedback.Information("Пользователь успешно добавлен!");
                    DialogResult = true;
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                Feedback.Error($"Пользователь с таким логином уже существет. Придумайте новый. {dbUpdateException.Message}");
            }

            catch (Exception exception)
            {
                Feedback.Error($"Ошибка при добавлении пользоваетля. {exception.Message}");
            }
        }
    }
}
