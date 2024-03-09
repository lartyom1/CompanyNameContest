using System.Runtime.Serialization;

namespace CompanyNameContest.Report
{
    public class UserCancelledException : Exception
    {
        public UserCancelledException() { }
        public UserCancelledException(string? msg) : base(msg) { }
    }
}