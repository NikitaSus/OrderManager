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
        public bool _isValidDistrict;

        public Form1()
        {
            InitializeComponent();
            FirstDeliveryDateTime.GotFocus += RemovePlaceholderText;
            FirstDeliveryDateTime.LostFocus += SetPlaceholderText;
            // Можно сделать лучше, пока что так
            FirstDeliveryDateTime.ForeColor = Color.Gray;
            FirstDeliveryDateTime.Text = "yyyy-MM-dd HH:mm:ss";
            FilterOrdersButton.Enabled = false;
            Logger.Log("Приложение запущено, инициализация завершена.");
        }

        #region Район

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
        /// Обновляем значение _isValidDistrict при изменении текста в поле "Район"
        /// </summary>
        private void CityDistrictTextChanged(object sender, EventArgs e)
        {
            // Проверяем, что поле не пустое
            _isValidDistrict = !string.IsNullOrWhiteSpace(CityDistrict.Text);
            UpdateFilterButtonState();
        }

        #endregion

        #region Дата и время

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

        public void FirstDeliveryTimeTextChanged(object sender, EventArgs e)
        {
            // Проверяем формат даты при потере фокуса
            DateTime parsedDate;
            string inputValue = FirstDeliveryDateTime.Text;
            _isValidDateTime = DateTime.TryParseExact(inputValue, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            UpdateFilterButtonState();
        }

        #endregion

        #region Сохранить и выбрать

        /// <summary>
        /// Обрабатываем клик и выдаем конечный результат
        /// </summary>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            Logger.Log("Нажата кнопка фильтрации заказов.");

            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файл с заказами";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Файл не выбран. Операция отменена.");
                    Logger.Log("Файл для загрузки заказов не выбран.");
                    return;
                }

                filePath = openFileDialog.FileName;
                Logger.Log($"Выбран файл заказов: {filePath}");
            }

            //string resultDirectory = "C:\\Orders";
            string resultFilePath; //= Path.Combine(resultDirectory, "resultOrder.txt");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Сохраните результат фильтрации";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FileName = "resultOrder.txt";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Путь для сохранения не выбран. Операция отменена.");
                    Logger.Log("Не указан путь для сохранения результата.");
                    return;
                }

                resultFilePath = saveFileDialog.FileName;
                Logger.Log($"Файл для сохранения результата: {resultFilePath}");
            }

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
                Logger.Log("Фильтрация завершена и данные успешно записаны.");
            }
        }

        /// <summary>
        /// Обновляем состояние кнопки FilterButton в зависимости от валидации даты и района
        /// </summary>
        private void UpdateFilterButtonState()
        {
            // Кнопка активна только если и дата, и район валидны
            FilterOrdersButton.Enabled = _isValidDateTime && _isValidDistrict;
        }

        #endregion
    }
}
