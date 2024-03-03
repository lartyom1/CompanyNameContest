using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Reporter
{
    public class Reporter : IReporter
    {
        public void ReportError(int id)
        {
            throw new NotImplementedException();
        }

        public void ReportSuccess(byte[] data, int id)
        {
            throw new NotImplementedException();
        }

        public void ReportTimeout(int id)
        {
            throw new NotImplementedException();
        }
    }
}
