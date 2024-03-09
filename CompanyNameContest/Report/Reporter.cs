using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    /// <summary>
    /// Отчет о работе построителя отчетов
    /// </summary>
    public class Reporter : IReporter
    {
        /// <summary>
        /// Построение отчета с ошибкой
        /// </summary>
        /// <param name="id"> id отчета </param>
        public void ReportError(int id)
        {
            Console.WriteLine($"fail report id:{id}");
            WriteToFile($"Error_[{id}].txt", "Report error");
        }

        /// <summary>
        /// Успешное завершение построения отчета
        /// </summary>
        /// <param name="data"> Данные отчета </param>
        /// <param name="id"> id отчета </param>
        public void ReportSuccess(byte[] data, int id)
        {
            Console.WriteLine($"success report id:{id}, " +
                              $"bytes to utf8: {System.Text.Encoding.UTF8.GetString(data)}");

            WriteToFile($"Report_[{id}].txt", System.Text.Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// Построение отчета заняло слишком много времени
        /// </summary>
        /// <param name="id"></param>
        public void ReportTimeout(int id)
        {
            Console.WriteLine($"timeout report id:{id}");
            WriteToFile($"Timeout_[{id}].txt", "Report error");
        }

        /// <summary>
        /// Построение отчета отменено пользователем
        /// </summary>
        /// <param name="id"></param>
        public void ReportCancelled(int id)
        {
            Console.WriteLine($"user cancelled report id:{id}");
            WriteToFile($"UserCancel_[{id}].txt", "Report cancelled");
        }

        /// <summary>
        /// Запись в файл результата построение отчета
        /// </summary>
        /// <param name="filename"> Имя файла </param>
        /// <param name="text"> Строка для добавления в файл </param>
        private void WriteToFile(string filename, string text)
        {
            if (!File.Exists(filename)) File.Create(filename).Close();
            File.AppendAllText(filename, text + Environment.NewLine);   
        }
    }
}
