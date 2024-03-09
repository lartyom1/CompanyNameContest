using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;

namespace CompanyNameContest.Services
{
    /// <summary>
    /// Сервис построителя отчетов
    /// </summary>
    public class ReportService
    {
        private readonly Reporter reporter = new();
        private readonly Dictionary<int, CancellationTokenSource> reports = new();

        private int idCounter;
        private const int n = 30000; // less than 45k to hit timeout

        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="reportBuilder"> Построитель отчетов </param>
        /// <returns> id отчета </returns>
        public int Create(IReportBuilder reportBuilder)
        {
            var cancellationTokenSource = new CancellationTokenSource(n);
            var userCancellationTokenSource = new CancellationTokenSource();

            var token = cancellationTokenSource.Token;
            var userToken = userCancellationTokenSource.Token;

            var id = new int();
            id = idCounter;

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
                if (reportTask.Exception?.InnerException is TaskCanceledException)
                {
                    reporter.ReportTimeout(id);
                }
                else if (reportTask.Exception?.InnerException is UserCancelledException)
                {
                    reporter.ReportCancelled(id);
                }
                else
                {
                    reporter.ReportError(id);
                }
            },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                TaskScheduler.Current
            );

            reports.Add(id, userCancellationTokenSource);

            idCounter++;
            return id;
        }

        /// <summary>
        /// Отмена построения отчета
        /// </summary>
        /// <param name="id"> id отменяемого отчета </param>
        /// <exception cref="NoReportException"> Возникает если отчет отсутствует </exception>
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
