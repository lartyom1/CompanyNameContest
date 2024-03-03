using CompanyNameContest.Interfaces;
using CompanyNameContest.Reporter;

namespace CompanyNameContest.Report
{
    public class ReportBuilder : IReportBuilder
    {
        private Reporter.Reporter reporter = new Reporter.Reporter();
        public byte[] Build()
        {


            //continue with
            //strategy pattern
            //reporter.ReportTimeout();
            //reporter.ReportSuccess();
            throw new Exception("Reporter Failed");
        }
    }
}
