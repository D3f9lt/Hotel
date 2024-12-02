using Hotel.AppData;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        List<Category> categories = App.context.Category.ToList();
        public RoomsPage()
        {
            InitializeComponent();

            RoomsLB.ItemsSource = App.context.Room.ToList();
            categories.Insert(0, new Category { Name = "Все категории" });
            FilterCMB.ItemsSource = categories;
            FilterCMB.DisplayMemberPath = "Name";
            FilterCMB.SelectedValuePath = "Id";
            FilterCMB.SelectedIndex = 0;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTB.Text))
            {
                try
                {
                    int roomNumber = Convert.ToInt32(SearchTB.Text);
                    RoomsLB.ItemsSource = App.context.Room.Where(room => room.Number == roomNumber).ToList();
                }
                catch (FormatException exception)
                {
                    Feedback.Error($"{exception.Message} Используйте числовые символы для поиска.");
                }
                catch (Exception exception)
                {
                    Feedback.Error(exception.Message);
                }
            }
            else
            {
                RoomsLB.ItemsSource = App.context.Room.ToList();
            }
        }

        private void FilterCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterCMB.SelectedIndex != 0)
            {
                RoomsLB.ItemsSource = App.context.Room.Where(room => room.CategoryId == FilterCMB.SelectedIndex).ToList();
            }
            else
            {
                RoomsLB.ItemsSource = App.context.Room.ToList();
            }

            CountRoomsByStatusTBL.Text = CountRoomsByStatus();
        }

        private void RoomsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public string CountRoomsByStatus()
        {
            

            return $"";
        }

        private void BoolingBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
