using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using WeatherNow.HelperClasses;

namespace WeatherNow
{
    /// <summary>
    /// Главное окно с информацией о погоде
    /// </summary>
    public partial class MainWindow : Window
    {
        // Ключ для запроса
        private readonly string key = "89b2e69a9a1fe0f4d160554c5b9ff114";

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
        public string TemperatureMax
        {
            get => (string)GetValue(TemperatureMaxProperty);
            set => SetValue(TemperatureMaxProperty, value);
        }

        public static readonly DependencyProperty TemperatureMaxProperty =
            DependencyProperty.Register("TemperatureMax", typeof(string), typeof(MainWindow));
        
        
        
        // PropertyChanged для температуры
        public string TemperatureMin
        {
            get => (string)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("TemperatureMin", typeof(string), typeof(MainWindow));




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



        // PropertyChanged для угла направления ветра
        public int WindDirection
        {
            get { return (int)GetValue(WindDirectionProperty); }
            set { SetValue(WindDirectionProperty, value); }
        }

        public static readonly DependencyProperty WindDirectionProperty =
            DependencyProperty.Register("WindDirection", typeof(int), typeof(MainWindow));


        // Singleton
        public static MainWindow Instance { get; private set; }


        public MainWindow()
        {
            // Добавляем в список Москву с координатами "55.45, 37.36";

            InitializeComponent();
            Instance = this;

            SettingBtn.Click += (sender, e) =>
            {
                new Setting().ShowDialog();
            };

            // Происходит при выборе города из списка
            //CitiesList.SelectionChanged += async (sender, e) =>
            //{
            //    // Переменная для хранения выбранного города
            //    City itemCitiesList;
            //    // Переменные для хранения широты и долготы
            //    string lat, lon;

            //    CityPreparing(out itemCitiesList);
            //    // Метод для подготовки данных для запроса
            //    CoordinatesPreparing(itemCitiesList, out lat, out lon);
            //    // Запись ответа полученных данных в объект
            //    var weather = await GetWeather(lat, lon);
            //    // Запись значения данных запроса в переменную хранящую значение города
            //    City = itemCitiesList.Name;
            //    // Запись значения данных запроса в переменную хранящую значение температуры
            //    Temperature = Math.Round(weather.Main.Temp - 273.15).ToString();
            //    // Запись значения данных запроса в переменную хранящую значение описания погоды
            //    DescriptionWeather = weather.Weather[0].Description;
            //    // Запись значения данных запроса в переменную хранящую значение иконки погоды
            //    IconWeather = GetImage($"https://openweathermap.org/img/wn/{weather.Weather[0].Icon}@4x.png");
            //    // Запись значения данных запроса в переменную хранящую значение температуры по ощущению
            //    TemperatureFeels = Math.Round(weather.Main.Feels_Like - 273.15).ToString();
            //    // Запись значения данных запроса в переменную хранящую значение скорости ветра
            //    WindSpeed = weather.Wind.Speed;
            //    // Запись значения данных запроса в переменную хранящую значение давления
            //    Pressure = Math.Round(weather.Main.Pressure / 1.333).ToString();
            //    // Запись значения данных запроса в переменную хранящую значение угла направления ветра
            //    WindDirection = weather.Wind.Deg;
            //};

            //// Происходит при загрузке всех компонентов на странице
            //Loaded += (sender, e) =>
            //{
            //    // Выбирает первый элемент из списка городов
            //    CitiesList.SelectedIndex = 0;
            //};

            DaysList.SelectionChanged += (sender, e) =>
            {
                if(DaysList.SelectedItem == null) return;

                var weather = (DaysList.SelectedItem as Days).DataWeather;

                TemperatureMax = Math.Round(weather.main.Temp_Max).ToString();
                TemperatureMin = Math.Round(weather.main.Temp_Min).ToString();
                DescriptionWeather = weather.weather.First().Description;
                IconWeather = GetImage($"https://openweathermap.org/img/wn/{weather.weather.First().Icon}@4x.png");
                TemperatureFeels = Math.Round(weather.main.Feels_Like).ToString();
                WindSpeed = weather.wind.Speed;
                Pressure = Math.Round(weather.main.Pressure / 1.333).ToString();
                WindDirection = weather.wind.Deg;
            };

            Loaded += async (sender, e) =>
            {
                await InitWeather();
            };
        }

        public async Task InitWeather()
        {
            var AllWeather = await GetWeather();

            var itemsWeatherList = AllWeather.list.GroupBy(l => FormatingDate(l.dt)).Take(7).Select(w => new Days
            {
                IconWeather = GetImage($"https://openweathermap.org/img/wn/{w.Select(we => we.weather.First().Icon).First()}@4x.png"),
                Name = w.Key.ToString("dddd"),
                TemperatureMax = Math.Round(w.Average(weat => weat.main.Temp_Max)).ToString(),
                DataWeather = w.Select(root => root).First()
            });

            DaysList.ItemsSource = itemsWeatherList;

            DaysList.Items.Refresh();
            DaysList.SelectedIndex = 0;
        }

        private static DateTime FormatingDate(long date)
        {
            return DateTimeOffset.FromUnixTimeSeconds(date).LocalDateTime.Date;
        }

        //private void CoordinatesPreparing(City itemCitiesList, out string lat, out string lon)
        //{
        //    // Регулярное выражения для преобразования строки координат в переменные
        //    Regex coordinatesRegex = new Regex(@"\d+.\d+");
        //    //Выбираем все значения координат
        //    MatchCollection listCoordinates = coordinatesRegex.Matches(itemCitiesList.Coordinates);
        //    // Записываем в переменную широту
        //    lat = listCoordinates[0].Value;
        //    // Записываем в переменную долготу
        //    lon = listCoordinates[1].Value;
        //}

        //private void CityPreparing(out City itemCitiesList)
        //{
        //    // Записываем выбранный город из списка в переменную
        //    itemCitiesList = CitiesList.SelectedItem as City;
        //}

        // Метод отправки Get запроса и преобразование его в объект
        public async Task<Root> GetWeather()
        {
            try
            {
                // Создание экземпляра класса HttpClient
                HttpClient client = new HttpClient();
                // Url для отправки Get запроса
                string url = $"http://api.openweathermap.org/data/2.5/forecast?lat={App.Lat}&lon={App.Lon}&appid={key}&units=metric&lang=ru";
                // Асинхронное отправка Get запроса
                HttpResponseMessage response = await client.GetAsync(url);
                // Асинхронное получение ответа в виде JSON и преобразование его в объект типа Root
                var responseBody = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
                //Возрат объекта
                return responseBody;
            }
            catch (Exception ex)
            {
                // В случае ошибки вывод сообщения о ней
                MessageBox.Show($"Ошибка :{ex.Message} ");
                return null;
            }
        }

        // Метод для поучения картинки по ссылке и преобразования в тип BitmapImage
        public BitmapImage GetImage(string uriImage)
        {
            try
            {
                // Получение ответа от интернет ресурса
                var responseStream = WebRequest.Create(uriImage).GetResponse().GetResponseStream();
                // Инициализация объекта типа Bitmap
                Bitmap loadedBitmap = new Bitmap(responseStream);
                // Экземпляр MemoryStream
                MemoryStream ms = new MemoryStream();
                // Сохранение изображения в нужный формат
                loadedBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                // Экземпляр класса BitmapImage
                BitmapImage image = new BitmapImage();
                // Начало инициализации
                image.BeginInit();
                // Задаем начало потока
                ms.Seek(0, SeekOrigin.Begin);
                // Задаем MemoryStream для BitmapSourse
                image.StreamSource = ms;
                // Конец инициализации
                image.EndInit();
                // Возвращение картинки
                return image;
            }
            catch (Exception ex)
            {
                // В случае ошибки вывод сообщения о ней
                MessageBox.Show($"Ошибка :{ex.Message} ");
                return null;
            }
        }
    }
}
