using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        // PropertyChanged для ListView в котором будут размещены сохранённые города
        public List<City> Cities
        {
            get => (List<City>)GetValue(CitiesProperty);
            set => SetValue(CitiesProperty, value);
        }

        public static readonly DependencyProperty CitiesProperty =
            DependencyProperty.Register("Cities", typeof(List<City>), typeof(MainWindow));


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
            Cities = new List<City>()
            {
                new City
                {
                    Name = "Москва",
                    Coordinates = "55,45, 37,36"
                }
            };

            InitializeComponent();
            Instance = this;

            // Происходит при выборе города из списка
            CitiesList.SelectionChanged += async (sender, e) =>
            {
                // Переменная для хранения выбранного города
                City itemCitiesList;
                // Переменные для хранения широты и долготы
                string lat, lon;

                CityPreparing(out itemCitiesList);
                // Метод для подготовки данных для запроса
                CoordinatesPreparing(itemCitiesList, out lat, out lon);
                // Запись ответа полученных данных в объект
                var weather = await GetWeather(lat, lon);
                // Запись значения данных запроса в переменную хранящую значение города
                City = itemCitiesList.Name;
                // Запись значения данных запроса в переменную хранящую значение температуры
                Temperature = Math.Round(weather.Main.Temp - 273.15).ToString();
                // Запись значения данных запроса в переменную хранящую значение описания погоды
                DescriptionWeather = weather.Weather[0].Description;
                // Запись значения данных запроса в переменную хранящую значение иконки погоды
                IconWeather = GetImage($"https://openweathermap.org/img/wn/{weather.Weather[0].Icon}@4x.png");
                // Запись значения данных запроса в переменную хранящую значение температуры по ощущению
                TemperatureFeels = Math.Round(weather.Main.Feels_Like - 273.15).ToString();
                // Запись значения данных запроса в переменную хранящую значение скорости ветра
                WindSpeed = weather.Wind.Speed;
                // Запись значения данных запроса в переменную хранящую значение давления
                Pressure = Math.Round(weather.Main.Pressure / 1.333).ToString();
                // Запись значения данных запроса в переменную хранящую значение угла направления ветра
                WindDirection = weather.Wind.Deg;
            };

            // Просиходит при нажатии на кнопку добавления города
            AddingCityBtn.Click += (sender, e) =>
            {
                // Открытие окна с возможностью добовления города
                new AddingCitiesWindow() { Owner = this }.ShowDialog();
            };

            // Происходит при загрузке всех компонентов на странице
            Loaded += (sender, e) =>
            {
                // Выбирает первый элемент из списка городов
                CitiesList.SelectedIndex = 0;
            };
        }

        private void CoordinatesPreparing(City itemCitiesList, out string lat, out string lon)
        {
            // Регулярное выражения для преобразования строки координат в переменные
            Regex coordinatesRegex = new Regex(@"\d+.\d+");
            //Выбираем все значения координат
            MatchCollection listCoordinates = coordinatesRegex.Matches(itemCitiesList.Coordinates);
            // Записываем в переменную широту
            lat = listCoordinates[0].Value;
            // Записываем в переменную долготу
            lon = listCoordinates[1].Value;
        }

        private void CityPreparing(out City itemCitiesList)
        {
            // Записываем выбранный город из списка в переменную
            itemCitiesList = CitiesList.SelectedItem as City;
        }

        // Метод отправки Get запроса и преобразование его в объект
        public async Task<Root> GetWeather(string lat, string lon)
        {
            try
            {
                // Создание экземпляра класса HttpClient
                HttpClient client = new HttpClient();
                // Url для отправки Get запроса
                string url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}&lang=ru";
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
