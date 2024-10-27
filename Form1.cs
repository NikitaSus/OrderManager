using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OrderManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обрабатываем клик и выдаем конечный результат
        /// </summary>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            //Для теста
            string filePath = "C:\\Users\\suspi\\source\\repos\\order.txt";
            string resultDirectory = "C:\\Orders";
            string resultFilePath = Path.Combine(resultDirectory, "resultOrder.txt");

            string district = cityDistrict.Text;
            DateTime firstDeliveryTime = DateTime.Parse(firstDeliveryDateTime.Text);
            

            //// Если время ввели неверно
            //if (!DateTime.TryParse(firstDeliveryDateTime.Text.ToString(), out firstDeliveryTime))
            //{
            //    MessageBox.Show("Введите время в формате (_:_:_)");
            //    return;
            //}

            // Если файл не выбран, то прекращаем
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Не выбран файл");
                return;
            }

            // Получаем заказы
            Order orderInstance = new Order();
            var orders = orderInstance.LoadOrder(filePath);

            // Фильтруем, передаем список всех заказов, время и район
            var filteredOrders = orderInstance.FilteredOrder(orders, firstDeliveryTime, district);

            // Очистить лист бокс от старых значений
            listBoxOrder.Items.Clear();

            // Теперь добавим визуальное отображение
            foreach (var order in filteredOrders)
            {
                listBoxOrder.Items.Add($"Номер: {order.OrderNumber}, Время: {order.DeliveryTime}, Район: {order.District}");
            }

            if (!Directory.Exists(resultDirectory))
            {
                Directory.CreateDirectory(resultDirectory);
            }

            // Так же запишим в файл результат
            using (var writer = new StreamWriter(resultFilePath))
            {
                foreach (var order in filteredOrders)
                {
                    string line = $"{order.OrderNumber},{order.Weight.ToString(CultureInfo.InvariantCulture)}," +
                                  $"{order.District},{order.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                    writer.WriteLine(line);
                }

                MessageBox.Show($"Данные записаны в файл {resultFilePath}");
            }
        }
    }
}
