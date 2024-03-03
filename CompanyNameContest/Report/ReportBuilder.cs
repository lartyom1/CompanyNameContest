using CompanyNameContest.Interfaces;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {      
        public byte[] Build()
        {
            // ?reflection

            Random r = new Random();
            int time = r.Next(5, 45);
            bool failure = r.Next(0, 4) == 0; // 20% failure

            for (int i = 0; i < time; i++)
            {
                Thread.Sleep(1000);
                if(failure && i == 2) throw new Exception("Reporter Failed");


                //??
                //if (token.IsCancellationRequested)
                //    token.ThrowIfCancellationRequested();
            }

            return System.Text.Encoding.UTF8.GetBytes($"Report build in {time} s");
        }
    }
}
