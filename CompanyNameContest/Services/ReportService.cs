using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using System.Threading.Tasks;

namespace CompanyNameContest.Services
{
    public class ReportService
    {

        ///private ReportBuilder reportBuilder = new ReportBuilder();
        private Reporter reporter = new Reporter();

        //private Dictionary<int, (Task<byte[]>, CancellationTokenSource)> reports =
        //    new Dictionary<int, (Task<byte[]>, CancellationTokenSource)>();

        private Dictionary<int, CancellationTokenSource> reports2 =
        new Dictionary<int, CancellationTokenSource>();

        private int i;
        //private const int n = 30000; // less than 45k to hit timeout
        private const int n = 8000;
        public int Create(/*ReportBuilder2 rb*/)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(n);
            CancellationToken token = cancellationTokenSource.Token;

            int id = new int();
            id = i;

            Console.WriteLine($"run rep{id}");
            //var tt = Task.Run(() =>
            //    new ReportBuilder(token).Build(), token);

            //tt.ContinueWith(x =>
            //{
            //    reporter.ReportSuccess(x.Result, id);
            //},
            //    token,
            //    TaskContinuationOptions.OnlyOnRanToCompletion,
            //    TaskScheduler.Current
            //);

            //tt.ContinueWith(x =>
            //{
            //    if (tt.Exception.GetBaseException().GetType() == typeof(TaskCanceledException))
            //    {
            //        reporter.ReportTimeout(id);
            //    }
            //    else
            //    {
            //        reporter.ReportError(id);
            //    }
            //},
            //    CancellationToken.None,
            //    TaskContinuationOptions.OnlyOnFaulted, // aka exception in task
            //    TaskScheduler.Current
            //);
            //reports.Add(id, (tt, cancellationTokenSource));


            var rb = new ReportBuilder2();
            var reportBuilder = rb;
            rb.Token = token;

            var reportTask = reportBuilder.BuildReport();

            reportTask.ContinueWith(x =>
            {
                reporter.ReportSuccess(reportTask.Result, id);
            },
                token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.Current
            );

            reportTask.ContinueWith(x =>
            {
                if (reportTask.Exception.GetBaseException().GetType() == typeof(TaskCanceledException))
                {
                    reporter.ReportTimeout(id);
                }
                else
                {
                    reporter.ReportError(id);
                }
            },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted, // aka exception in task
                TaskScheduler.Current
            );

            reports2.Add(id, cancellationTokenSource);

            i++;
            return id;
        }

        public void Terminate(int id)
        {
            //if (reports.ContainsKey(id))
            //{
            //    reports[id].Item2.Cancel();
            //}
            //else
            //{
            //    throw new NoReportException(id.ToString());
            //}

            if (reports2.ContainsKey(id))
            {
                reports2[id].Cancel();
            }
            else
            {
                throw new NoReportException(id.ToString());
            }

        }

    }
}
