using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {
        public CancellationToken Token { get; set; }
        public CancellationToken UserToken { get; set; }
        public byte[] Build()
        {
            Random r = new Random();

            int time = r.Next(5, 45);
            //bool failure = r.Next(0, 9) == 0; // 10% failure

            //int time = r.Next(5, 45);
            bool failure = r.Next(0, 4) == 0; // 20% failure
            Console.WriteLine($"time{time} fail{failure} (35s is overtime)");

            var timeStart = DateTime.Now;


            for (int i = 0; i < time; i++)
            {
                Task.Delay(1000, Token).Wait();
                if (UserToken.IsCancellationRequested)
                {
                    throw new UserCancelledException();
                }
               

                if (failure && i == 2)
                    throw new Exception("Reporter Failed");

            }

            var timeElapsed = (DateTime.Now - timeStart).Seconds;//real time elapsed
            return System.Text.Encoding.UTF8.GetBytes($"Report built in {timeElapsed} s");
        }

    }
}
