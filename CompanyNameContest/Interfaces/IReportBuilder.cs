namespace CompanyNameContest.Interfaces
{
    public interface IReportBuilder
    {
        public CancellationToken Token { get; set; }
        public byte[] Build();
        public void SetToken(CancellationToken tk) => Token = tk;
    
    }
}
