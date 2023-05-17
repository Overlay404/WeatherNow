using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherNow.HelperClasses;

namespace WeatherNow
{
    /// <summary>
    /// Окно с возможность добалять новые города в список для просмотра
    /// </summary>
    public partial class AddingCitiesWindow : Window
    {
        public AddingCitiesWindow()
        {
            InitializeComponent();
            // Событие при нажатии на изображение крестика
            ExitBtn.MouseDown += (sender, e) =>
            {
                // Закрытие окна
                Close();
            };
            // Событие при нажатии на кнопку добавления
            AddingCityBtn.Click += (sender, e) =>
            {
                // Условие проверки правильности введения координат
                if(new Regex(@"(90.0{2,4}|[0-8][0-9].\d{2,4}|\d.\d{2,4})\,.(180.0{2,4}|[1][0-7]\d.\d{2,4}|\d{2,4}.\d{2,4}|\d.\d{2,4})").IsMatch(Coordinates.Text) == false)
                {
                    MessageBox.Show("Введённое значение координат не соответствует формату для обработки значения.\nПример: 55.7522, 37.6156");
                    // Выход из метода
                    return;
                }

                // Добавление к списку с городами новый город
                MainWindow.Instance.Cities.Add(new City()
                {
                    Name = NameCity.Text,
                    Coordinates = Coordinates.Text
                });
                // Обновление списка для отображения изменений
                MainWindow.Instance.CitiesList.Items.Refresh();
            };
        }
    }
}
