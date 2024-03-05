using CompanyNameContest.Report;
using System.Threading.Tasks;

namespace CompanyNameContest.Services
{
    public class ReportService
    {

        ///private ReportBuilder reportBuilder = new ReportBuilder();
        private Reporter reporter = new Reporter();

        private Dictionary<int, (Task<byte[]>, CancellationTokenSource)> reports =
            new Dictionary<int, (Task<byte[]>, CancellationTokenSource)>();

        private int i;
        //private const int n = 30000; // less than 45k to hit timeout
        private const int n = 8000;
        public int Create()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(n); // task will be cancelled
            //                                        // ?not possible to determine user or timeout cancel
            CancellationToken token = cancellationTokenSource.Token;


            //CancellationTokenSource cancellationTokenSourceContinue = new CancellationTokenSource(n);
            //CancellationToken tokenContinue = cancellationTokenSourceContinue.Token;

            int id = new int();
            id = i;

            Console.WriteLine($"run rep{id}");
            var tt = Task.Run(() =>
                new ReportBuilder() { _token = token }.Build(), token);

            tt.ContinueWith(x =>
            {
                reporter.ReportSuccess(x.Result, id);
            },
                token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.Current
            );

            tt.ContinueWith(x =>
            {
                if (tt.Exception.GetBaseException().GetType() == typeof(TaskCanceledException))
                {
                    reporter.ReportTimeout(id);
                }
                else
                {
                    reporter.ReportError(id);
                }
            },
                //token,
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted, // aka exception in task
                TaskScheduler.Current
            );

            ////?cancel after
            //tt.ContinueWith(x =>
            //{
            //    if (tt.IsCanceled)
            //    {
            //        reporter.ReportTimeout(id);
            //    }
            //},
            //    CancellationToken.None,
            //    TaskContinuationOptions.None, // canceled by user or timeout
            //    TaskScheduler.Current
            //);

            reports.Add(id, (tt, cancellationTokenSource));
            i++;
            return id;
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
