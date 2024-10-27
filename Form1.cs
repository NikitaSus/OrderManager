using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace OrderManager
{
    public partial class Form1 : Form
    {
        public bool _isValidDateTime;
        public string _isValidDistrict;

        public Form1()
        {
            InitializeComponent();
            FirstDeliveryDateTime.GotFocus += RemovePlaceholderText;
            FirstDeliveryDateTime.LostFocus += SetPlaceholderText;
        }

        /// <summary>
        /// Проверяем, является ли введенный символ буквой
        /// </summary>
        private void CitiDistrictKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Если это не буква и не Backspace, то блокируем ввод
                e.Handled = true;
            }
        }

        /// <summary>
        /// Убираем текст подсказки при фокусе на FirstDeliveryDateTime
        /// </summary>
        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            // Убираем текст подсказки при фокусе на TextBox
            if (FirstDeliveryDateTime.ForeColor == Color.Gray)
            {
                FirstDeliveryDateTime.Text = "";
                FirstDeliveryDateTime.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Восстанавливаем текст подсказки, если FirstDeliveryDateTime пустой
        /// </summary>
        private void SetPlaceholderText(object sender, EventArgs e)
        {
            // Восстанавливаем текст подсказки, если TextBox пустой
            if (string.IsNullOrWhiteSpace(FirstDeliveryDateTime.Text))
            {
                FirstDeliveryDateTime.ForeColor = Color.Gray;
                FirstDeliveryDateTime.Text = "yyyy-MM-dd HH:mm:ss";
            }
        }

        /// <summary>
        /// Разрешаем только цифры, дефисы, двоеточия и пробелы для даты и времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDeliveryTimeKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ':' && e.KeyChar != ' ' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// При потере фокуса проверяем введенный текст
        /// </summary>
        public void FirstDeliveryTimeLeave(object sender, EventArgs e)
        {
            // Проверяем формат даты при потере фокуса
            DateTime parsedDate;
            string inputValue = FirstDeliveryDateTime.Text;
            _isValidDateTime = DateTime.TryParseExact(inputValue, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        }

        /// <summary>
        /// Обрабатываем клик и выдаем конечный результат
        /// </summary>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            if (!_isValidDateTime)
            {
                MessageBox.Show("Введите дату в формате: yyyy-MM-dd HH:mm:ss", "Неверный формат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FirstDeliveryDateTime.Focus();
                return;
            }

            //Для теста
            string filePath = "C:\\Users\\suspi\\source\\repos\\order.txt";
            string resultDirectory = "C:\\Orders";
            string resultFilePath = Path.Combine(resultDirectory, "resultOrder.txt");

            string district = CityDistrict.Text;
            DateTime firstDeliveryTime = DateTime.Parse(FirstDeliveryDateTime.Text);

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
