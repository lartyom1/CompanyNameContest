using CompanyNameContest.Report;

namespace CompanyNameContest.Services
{
    public class ReportService
    {

        private ReportBuilder reportBuilder = new ReportBuilder();

        private Dictionary<int, (Task<byte[]>, CancellationTokenSource)> reports =
            new Dictionary<int, (Task<byte[]>, CancellationTokenSource)>();

        private int i = 0;
        public int CreateAction()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            var tt = Task.Run(() =>
                reportBuilder.Build(), token);

            reports.Add(i, (tt, cancellationTokenSource));
            return i++;
        }

 
        public void Terminate(int id)
        {
            if (reports.ContainsKey(id))
            {
                reports[id].Item2.Cancel();
            }
            else
            {
                throw new NoReportException(id.ToString());
            }

        }

    }
}
