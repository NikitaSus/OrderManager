using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManager
{
    public class Logger
    {
        private static readonly string logFilePath = "C:\\logs\\app_log.txt"; // Укажите путь для файла логов

        public static void Log(string message)
        {
            try
            {
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи в лог: {ex.Message}");
            }
        }

    }
}
