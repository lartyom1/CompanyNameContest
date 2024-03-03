using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {      
        public byte[] Build()
        {
            // ?reflection
            Random r = new Random();

            //int time = r.Next(5, 45);
            int time = r.Next(5, 10);
            //bool failure = r.Next(0, 4) == 0; // 20% failure
            bool failure = r.Next(0, 9) == 0; // 10% failure
            Console.WriteLine($"time{time} fail{failure} (8s is overtime)");

            var timeStart = DateTime.Now;
            for (int i = 0; i < time; i++)
            {
                Thread.Sleep(1000);
                if(failure && i == 2) 
                    throw new Exception("Reporter Failed");


                //??
                //if (token.IsCancellationRequested)
                //    token.ThrowIfCancellationRequested();
            }
            var timeElapsed = (DateTime.Now - timeStart).Seconds;//real time elapsed

            return System.Text.Encoding.UTF8.GetBytes($"Report built in {timeElapsed} s");
        }
    }
}
