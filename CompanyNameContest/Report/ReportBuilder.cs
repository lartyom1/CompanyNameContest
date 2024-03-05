using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {
        private CancellationToken _token;
        
        public ReportBuilder(CancellationToken token) { _token = token; }

        //public CancellationToken _token { get; set; }

        public byte[] Build()
        {
            Random r = new Random();

            int time = r.Next(5, 10);
            bool failure = r.Next(0, 9) == 0; // 10% failure

            //int time = r.Next(5, 45);
            //bool failure = r.Next(0, 4) == 0; // 20% failure
            Console.WriteLine($"time{time} fail{failure} (8s is overtime)");

            var timeStart = DateTime.Now;
            for (int i = 0; i < time; i++)
            {
                //Thread.Sleep(1000);
                Task.Delay(1000, _token).Wait();
                //token.ThrowIfCancellationRequested();

                if (failure && i == 2)
                    throw new Exception("Reporter Failed");

            }
            var timeElapsed = (DateTime.Now - timeStart).Seconds;//real time elapsed


            //Console.WriteLine($"finished{timeElapsed}");
            return System.Text.Encoding.UTF8.GetBytes($"Report built in {timeElapsed} s");
        }
    }
}
