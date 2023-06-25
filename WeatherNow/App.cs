using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WeatherNow.Model;

namespace WeatherNow
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static WeatherNowEntities db = new WeatherNowEntities();
        public static string Lat;
        public static string Lon;

        public App()
        {
            db.City.Load();
            Lat = db.City.First().Lat;
            Lon = db.City.First().Lot;
        }
    }
}
