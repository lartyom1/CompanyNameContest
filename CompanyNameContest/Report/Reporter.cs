using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class Reporter : IReporter
    {
        public void ReportError(int id)
        {
            Console.WriteLine($"fail report id:{id}");
        }

        public void ReportSuccess(byte[] data, int id)
        {
            Console.WriteLine($"success report id:{id}, " +
                              $"bytes to utf8: {System.Text.Encoding.UTF8.GetString(data)}");
        }

        public void ReportTimeout(int id)
        {
            Console.WriteLine($"timeout report id:{id}");

        }
    }
}
