using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class Reporter : IReporter
    {
        public void ReportError(int id)
        {
            Console.WriteLine($"fail report id:{id}");

            WriteToFile($"Error_[{id}].txt", "Report error");
        }

        public void ReportSuccess(byte[] data, int id)
        {
            Console.WriteLine($"success report id:{id}, " +
                              $"bytes to utf8: {System.Text.Encoding.UTF8.GetString(data)}");

            WriteToFile($"Report_[{id}].txt", System.Text.Encoding.UTF8.GetString(data));
            //?bytes as text or bytes like 0x00,0xAA...
        }

        public void ReportTimeout(int id)
        {
            Console.WriteLine($"timeout report id:{id}");
            WriteToFile($"Timeout_[{id}].txt", "Report error");
        }

        private void WriteToFile(string filename, string text)
        {
            if (!File.Exists(filename)) { File.Create(filename).Close(); }
            File.AppendAllText(filename, text + Environment.NewLine);
        }
        //THREADSAFE!!!
        //semaphore slim or smth

        //...
        //different filenames it's ?ok
    }
}
