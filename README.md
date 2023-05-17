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

Вывести менеджера и товар, который продали 22 августа 2021

SELECT m.Fio, p.Name FROM Sells s JOIN Managers m ON s.ID_Manag = m.ID
JOIN Products p ON s.ID_Prod = p.ID
WHERE s.Data = '22.08.2021'

Вывести все товары, у которых в названии имеется 'Фанера' и 
цена не ниже 1750

SELECT Name FROM Products
WHERE Cost >= 1750 AND Name LIKE '%Фанера%'


Вывести историю продаж товаров, группируя по месяцу продажи 
и наименованию товара

SELECT p.Name as "Наименовавние товара", MONTH(s.Data) as "Месяц" FROM Sells s 
JOIN Products p ON s.ID_Prod = p.ID
GROUP BY p.Name, MONTH(s.Data)


Вывести количество повторяющихся значений и сами значения 
из таблицы 'Товары', где количество повторений больше 1.

SELECT COUNT(Name) as "Количество повторяющихся значений в таблице", Name as "Наименование товара" FROM Products
GROUP BY Name


