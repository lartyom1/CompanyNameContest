namespace CompanyNameContest.Interfaces
{
    /// <summary>
    /// Отчет о работе построителя отчетов
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Успешное завершение построения отчета
        /// </summary>
        /// <param name="data"> Содержимое отчета </param>
        /// <param name="id"> id отчета </param>
        public void ReportSuccess(byte[] data, int id);

        /// <summary>
        /// Построение отчета завершено с ошибкой
        /// </summary>
        /// <param name="id">id отчета </param>
        public void ReportError(int id);

        /// <summary>
        /// Построение отчета заняло слишком много времени
        /// </summary>
        /// <param name="id"> id отчета </param>
        public void ReportTimeout(int id);

        /// <summary>
        /// Отчет отменен пользователем
        /// </summary>
        /// <param name="id"> id отчета </param>
        public void ReportCancelled(int id);
    }
}
