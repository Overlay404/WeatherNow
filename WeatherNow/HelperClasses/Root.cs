﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherNow.HelperClasses
{
    // Структура для преобразования JSON в объект Root
    public class Root
    {
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public List<WeatherItem> Weather { get; set; }
    }

    public class WeatherItem
    {
        public string Icon { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double Pressure { get; set; }
    }

    public class Wind
    {
        public string Speed { get; set; }
        public int Deg { get; set; }
    }
}
