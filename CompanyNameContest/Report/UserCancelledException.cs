namespace CompanyNameContest.Report
{
    /// <summary>
    /// Отменен пользователем
    /// </summary>
    public class UserCancelledException : Exception
    {
        public UserCancelledException() { }
        public UserCancelledException(string? msg) : base(msg) { }
    }
}