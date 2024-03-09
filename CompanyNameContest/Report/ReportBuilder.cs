using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {
        /// <summary>
        /// Служебный токен для автоматической отмены
        /// </summary>
        public CancellationToken Token { get; set; }

        /// <summary>
        /// Токен для отмены пользователем
        /// </summary>
        public CancellationToken UserToken { get; set; }

        /// <summary>
        /// Построение отчета
        /// </summary>
        /// <returns> Байты UTF8 строки </returns>
        /// <exception cref="UserCancelledException"> Возникает при отмене пользователем </exception>
        /// <exception cref="Exception"> Возникает в случае непредвиденной ошибки </exception>
        public byte[] Build()
        {
            var time = Random.Shared.Next(5, 45);
            var failure = Random.Shared.Next(0, 4) == 0; // 20% failure

            Console.WriteLine($"time {time} fail {failure} (35s is overtime)");

            var timeStart = DateTime.Now;

            for (var i = 0; i < time; i++)
            {
                Task.Delay(1000, Token).Wait();
                if (UserToken.IsCancellationRequested) throw new UserCancelledException();
                if (failure && i == 2) throw new Exception("Reporter Failed");
            }

            var timeElapsed = (DateTime.Now - timeStart).Seconds;//real time elapsed
            return System.Text.Encoding.UTF8.GetBytes($"Report built in {timeElapsed} s");
        }
    }
}
