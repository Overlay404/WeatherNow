using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;

namespace WeatherNow.HelperClasses
{
    // Структура для списка городов
    public class Days
    {
        public string Name { get; set; }
        public string TemperatureMax { get; set; }
        public string TemperatureMin { get; set; }
        public ImageSource IconWeather { get; set; }
        public Weather DataWeather { get; set; }
    }
}
