using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherNow.HelperClasses
{
    // Структура для преобразования JSON в объект Root
    public class Root
    {
        public List<Weather> list { get; set; }
    }

    public class Weather
    {
        public long dt { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public List<WeatherItem> weather { get; set; }
    }

    public class WeatherItem
    {
        public string Icon { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public double Feels_Like { get; set; }
        public double Pressure { get; set; }
    }

    public class Wind
    {
        public string Speed { get; set; }
        public int Deg { get; set; }
    }
}
