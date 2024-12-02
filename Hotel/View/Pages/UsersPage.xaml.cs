using Hotel.AppData;
using Hotel.Model;
using Hotel.View.Windows;
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

namespace Hotel.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {


        const int userRoleId = 2;

        public UsersPage()
        {
            InitializeComponent();

            // Загрузка пользователей в список при открытии страницы
            UsersLV.ItemsSource = App.context.User.Where(u => u.RoleId == userRoleId).ToList();
        }
        private void AddUserBTN_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            if (addUserWindow.ShowDialog() == true)
            {
                UsersLV.ItemsSource = App.context.User.ToList();
            }
        }
        private void UsersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDetailsGrid.DataContext = UsersLV.SelectedItem as User;
        }

        private void DaveChangesBTN_Click(object sender, RoutedEventArgs e)
        {
            App.context.SaveChanges();

            Feedback.Information("Информация успешно сохранена!");  
        }
    }
}
