namespace CompanyNameContest.Services
{
    public class NoReportException : Exception
    {
        public NoReportException(string id) : base($"No report with key {id}") { }       
    }
}