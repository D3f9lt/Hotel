using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary> 
        /// Представляет контекст данных для взаимодействия с базой данных. 
        /// </summary> 
        public static HotelEntities context = new HotelEntities();

        /// <summary> 
        /// Представляет пользователя для хранения пользователя вошедшего в систему. 
        /// </summary> 
        public static User currentUser;
    }
}
