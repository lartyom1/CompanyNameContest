namespace CompanyNameContest.Interfaces
{
    public interface IReporter
    {
        public void ReportSuccess(byte[] data, int id);

        public void ReportError(int id);

        public void ReportTimeout(int id);

        public void ReportCancelled(int id);
    }
}
