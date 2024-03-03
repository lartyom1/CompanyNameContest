using CompanyNameContest.Report;

namespace CompanyNameContest.Services
{
    public class ReportService
    {

        private ReportBuilder reportBuilder = new ReportBuilder();
        private Reporter.Reporter reporter = new Reporter.Reporter();

        private Dictionary<int, (Task<byte[]>, CancellationTokenSource)> reports =
            new Dictionary<int, (Task<byte[]>, CancellationTokenSource)>();

        private int i = 0;
        public int Create()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            var tt = Task.Run(() =>
                reportBuilder.Build(), token);

            tt.ContinueWith(x =>
            {
                reporter.ReportSuccess(x.Result, i);
            },
            token,
            TaskContinuationOptions.OnlyOnRanToCompletion,
            TaskScheduler.Default
            );

            tt.ContinueWith(x =>
            {
                reporter.ReportError(i);
            },
            token,
            TaskContinuationOptions.OnlyOnFaulted,//aka exception in task
            TaskScheduler.Default
            );

            //?cancel after

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
