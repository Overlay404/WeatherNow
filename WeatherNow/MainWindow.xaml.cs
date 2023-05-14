using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WeatherNow
{
    /// <summary>
    /// Главное окно с информацией о погоде
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string key = "89b2e69a9a1fe0f4d160554c5b9ff114";

        // PropertyChanged для ListView в котором будут размещены сохранённые города
        public IEnumerable<City> Cities
        {
            get => (IEnumerable<City>)GetValue(CitiesProperty);
            set => SetValue(CitiesProperty, value);
        }

        public static readonly DependencyProperty CitiesProperty =
            DependencyProperty.Register("Cities", typeof(IEnumerable<City>), typeof(MainWindow));


        // PropertyChanged для иконки погоды
        public ImageSource IconWeather
        {
            get => (ImageSource)GetValue(IconWeatherProperty);
            set => SetValue(IconWeatherProperty, value);
        }

        public static readonly DependencyProperty IconWeatherProperty =
            DependencyProperty.Register("IconWeather", typeof(ImageSource), typeof(MainWindow));



        // PropertyChanged для описания погоды
        public string DescriptionWeather
        {
            get => (string)GetValue(DescriptionWeatherProperty);
            set => SetValue(DescriptionWeatherProperty, value);
        }

        public static readonly DependencyProperty DescriptionWeatherProperty =
            DependencyProperty.Register("DescriptionWeather", typeof(string), typeof(MainWindow));



        // PropertyChanged для названия города
        public string City
        {
            get => (string)GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }

        public static readonly DependencyProperty CityProperty =
            DependencyProperty.Register("City", typeof(string), typeof(MainWindow));



        // PropertyChanged для температуры
        public string Temperature
        {
            get => (string)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(string), typeof(MainWindow));




        // PropertyChanged для температурного параметра, который определяет человеческое восприятие погоды
        public string TemperatureFeels
        {
            get => (string)GetValue(TemperatureFeelsProperty);
            set => SetValue(TemperatureFeelsProperty, value);
        }

        public static readonly DependencyProperty TemperatureFeelsProperty =
            DependencyProperty.Register("TemperatureFeels", typeof(string), typeof(MainWindow));




        // PropertyChanged для скорости ветра
        public string WindSpeed
        {
            get => (string)GetValue(WindSpeedProperty);
            set => SetValue(WindSpeedProperty, value);
        }

        public static readonly DependencyProperty WindSpeedProperty =
            DependencyProperty.Register("WindSpeed", typeof(string), typeof(MainWindow));




        // PropertyChanged для атмосферного давления
        public string Pressure
        {
            get => (string)GetValue(PressureProperty);
            set => SetValue(PressureProperty, value);
        }

        public static readonly DependencyProperty PressureProperty =
            DependencyProperty.Register("Pressure", typeof(string), typeof(MainWindow));



        public MainWindow()
        {
            // Добавляем в список Москву с координатами "55.45, 37.36";
            Cities = new List<City>()
            {
                new WeatherNow.City
                {
                    Name = "Москва",
                    Coordinates = "55,45, 37,36"
                }
            };

            InitializeComponent();

            CitiesList.SelectionChanged += async (sender, e) =>
            {
                Regex coordinatesRegex = new Regex(@"\d+.\d+");

                // Записываем выбранный город из списка в переменную
                City itemCitiesList = CitiesList.SelectedItem as City;

                //Выбираем все значения координат
                MatchCollection listCoordinates = coordinatesRegex.Matches(itemCitiesList.Coordinates);
                // Записываем в переменную широту
                string lat = listCoordinates[0].Value;
                // Записываем в переменную долготу
                string lon = listCoordinates[1].Value;

                // Выводим значение города
                City = itemCitiesList.Name;

                var objectWeather = await GetWeather(lat, lon);
                MessageBox.Show(objectWeather.Main.Temp.ToString());
            };
        }

        public async Task<WeatherData> GetWeather(string lat, string lon)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}";
                HttpResponseMessage response = await client.GetAsync(url);
                var responseBody = JsonConvert.DeserializeObject<WeatherData>(await response.Content.ReadAsStringAsync());

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Message :{e.Message} ");
                return null;
            }
        }

    }

    public class City
    {
        public string Name { get; set; }
        public string Coordinates { get; set; }
    }

    public class WeatherData
    {
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
    }
}
