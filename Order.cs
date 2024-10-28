using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OrderManager
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string District { get; set; }
        public double Weight { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Order() { }

        /// <summary>
        /// Чтений данных из файла с заказами
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        public List<Order> LoadOrder(string filePath)
        {
            Logger.Log($"Начата загрузка заказов из файла: {filePath}");

            List<Order> orders = new List<Order>();

            foreach (var line in File.ReadLines(filePath))
            {
                var element = line.Split(',');

                // Проверяем, что количество элементов соответствует ожиданиям
                if (element.Length < 4)
                {
                    Console.WriteLine($"Недостаточно данных в строке: {line}");
                    Logger.Log($"Недостаточно данных в строке: {line}");
                    continue; // Переходим к следующей строке
                }

                try
                {
                    // Создаем заказ
                    var order = new Order
                    {
                        OrderNumber = int.Parse(element[0].Trim()), // Преобразуем в целое число
                        Weight = double.Parse(element[1].Trim(), CultureInfo.InvariantCulture), // Преобразуем в double с учетом культуры
                        District = element[2].Trim(), // Убираем лишние пробелы
                        DeliveryTime = DateTime.ParseExact(element[3].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) // Парсинг даты и времени
                    };

                    orders.Add(order);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка в строке: {line}. Ошибка: {ex.Message}");
                    Logger.Log($"Ошибка при обработке строки: {line}. Ошибка: {ex.Message}");   
                }
            }

            return orders;
        }

        /// <summary>
        /// Отфильтровать заказы по времени и району
        /// </summary>
        public List<Order> FilteredOrder(List<Order> orders, DateTime firstDeliveryTime, string district)
        {
            Logger.Log($"Начата фильтрация заказов для района: {district} и времени: {firstDeliveryTime:yyyy-MM-dd HH:mm:ss}");
            List<Order> filteredOrder = new List<Order>();

            foreach (var order in orders)
            {
                // Накидываем к первоначальному времени 30 минут
                if (order.District == district &&
                    order.DeliveryTime >= firstDeliveryTime &&
                    order.DeliveryTime <= firstDeliveryTime.AddMinutes(30))
                {
                    filteredOrder.Add(order);
                }
            }

            Logger.Log($"Фильтрация завершена. Найдено {filteredOrder.Count} заказов.");
            return filteredOrder;
        }

    }
}
