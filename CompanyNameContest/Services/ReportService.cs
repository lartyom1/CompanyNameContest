using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using System.Threading.Tasks;

namespace CompanyNameContest.Services
{
    public class ReportService
    {
        private Reporter reporter = new Reporter();

        private Dictionary<int, CancellationTokenSource> reports =
        new Dictionary<int, CancellationTokenSource>();

        private int i;
        private const int n = 30000; // less than 45k to hit timeout
        //private const int n = 8000;
        public int Create(IReportBuilder reportBuilder)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(n);

            CancellationTokenSource userCancellationTokenSource = new CancellationTokenSource();

            CancellationToken token = cancellationTokenSource.Token;
            CancellationToken userToken = userCancellationTokenSource.Token;


            int id = new int();
            id = i;

            Console.WriteLine($"run rep{id}");

            reportBuilder.SetToken(token);
            reportBuilder.SetUserToken(userToken);

            var reportTask = Task.Run(() => reportBuilder.Build(), token);

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
                else if (reportTask.Exception.GetBaseException().GetType() == typeof(UserCancelledException))
                {
                    reporter.ReportCancelled(id);          
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

            reports.Add(id, userCancellationTokenSource);

            i++;
            return id;
        }

        public void Terminate(int id)
        {

            if (reports.ContainsKey(id))
            {
                reports[id].Cancel();
            }
            else
            {
                throw new NoReportException(id.ToString());
            }

        }

    }
}
