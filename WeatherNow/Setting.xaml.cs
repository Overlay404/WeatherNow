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
using WeatherNow.Model;

namespace WeatherNow
{
    /// <summary>
    /// Логика взаимодействия для Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {

        public static Setting Instance { get; set; }


        public IEnumerable<City> Cities
        {
            get { return (IEnumerable<City>)GetValue(CitiesProperty); }
            set { SetValue(CitiesProperty, value); }
        }
        public static readonly DependencyProperty CitiesProperty =
            DependencyProperty.Register("Cities", typeof(IEnumerable<City>), typeof(Setting));



        public Setting()
        {
            Cities = App.db.City.Local;

            InitializeComponent();
            Instance = this;
            // Событие при нажатии на изображение крестика
            ExitBtn.MouseDown += (sender, e) =>
            {
                Close();
            };

            RootElement.MouseDown += (sender, e) =>
            {
                DragMove();
            };

            AddCityBtn.Click += (sender, e) =>
            {
                new AddingCitiesWindow().ShowDialog();
            };

            CBSities.SelectionChanged += async (sender, e) =>
            {
                if (Cities == null) return;

                var city = CBSities.SelectedItem as City;

                App.Lat = city.Lat;
                App.Lon = city.Lot;

                await MainWindow.Instance.InitWeather();
            };
        }
    }
}
