namespace CompanyNameContest.Services
{
    /// <summary>
    /// Отчет отсутсвует
    /// </summary>
    public class NoReportException : Exception
    {
        public NoReportException(string id) : base($"No report with key {id}") { }       
    }
}