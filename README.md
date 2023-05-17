# WeatherNow

(90\.00|[0-8][0-9]\.\d{2}||\d\.\d{2})\, (180\.00|[1][0-7]\d\.\d{2}|\d{2}\.\d{2}|\d\.\d{2})


Вывести менеджеров у которых имеется номер телефона

SELECT * FROM Managers
WHERE Phone IS NOT NULL

Вывести кол-во продаж за 20 июня 2021

SELECT Count(*) as "Количество продаж за 20.06.2021" FROM Sells
WHERE Data = '20.06.2021'

Вывести среднюю сумму продажи с товаром 'Фанера'

SELECT AVG(Sum) FROM Sells
WHERE ID_Prod =
(SELECT ID FROM Products
WHERE Name = 'Фанера')

Вывести фамилии менеджеров и общую сумму продаж для 
каждого с товаром 'ОСБ'

SELECT m.Fio as "ФИО", Sum(s.Sum) as "Общая сумма продаж" FROM Sells s JOIN Managers m
ON s.ID_Manag = m.ID
WHERE ID_Prod = 
(SELECT ID FROM Products
WHERE Name = 'ОСБ')
GROUP BY m.Fio

