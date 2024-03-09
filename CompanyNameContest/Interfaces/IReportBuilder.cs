namespace CompanyNameContest.Interfaces
{
    /// <summary>
    /// Построитель отчетов
    /// </summary>
    public interface IReportBuilder
    {
        /// <summary>
        /// Служебный токен для автоматической отмены
        /// </summary>
        public CancellationToken Token { get; set; }

        /// <summary>
        /// Токен для отмены польлзователем
        /// </summary>
        public CancellationToken UserToken { get; set; }
        public byte[] Build();

        /// <summary>
        /// Установка токена для автоматической отмены
        /// </summary>
        /// <param name="tk"> Токен автоматической отмены </param>
        public void SetToken(CancellationToken tk) => Token = tk;

        /// <summary>
        /// Установка токена для отмены пользователем
        /// </summary>
        /// <param name="tk"> Токен отмены пользователем </param>
        public void SetUserToken(CancellationToken tk) => UserToken = tk;

    }
}
