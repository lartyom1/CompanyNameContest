using CompanyNameContest.Report;

namespace CompanyNameContest.Services
{
    public class ReportService
    {

        private ReportBuilder reportBuilder = new Report.ReportBuilder();
        private Reporter reporter = new Report.Reporter();

        private Dictionary<int, (Task<byte[]>, CancellationTokenSource)> reports =
            new Dictionary<int, (Task<byte[]>, CancellationTokenSource)>();

        private int i = 0;

        private const int n = 30000; // less than 45k to hit timeout
        public int Create()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(n); // task will be cancelled
                                                    // ?not possible to determine user or timeout cancel

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
                TaskContinuationOptions.OnlyOnFaulted, // aka exception in task
                TaskScheduler.Default
            );

            //?cancel after
            tt.ContinueWith(x =>
            {
                reporter.ReportError(i);
            },
                token,
                TaskContinuationOptions.OnlyOnCanceled, // cancelled by user or timeout
                TaskScheduler.Default
            );

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
