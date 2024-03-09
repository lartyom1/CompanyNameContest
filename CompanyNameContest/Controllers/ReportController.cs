using CompanyNameContest.Interfaces;
using CompanyNameContest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNameContest.Controllers
{
    /// <summary>
    /// Контроллер отчетов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;
        private readonly IReportBuilder _reportBuilder;

        public ReportController(IReportBuilder reportBuilder, ReportService reportService)
        {
            _reportBuilder = reportBuilder;
            _reportService = reportService;
        }

        /// <summary>
        /// Построение отчета
        /// </summary>
        /// <returns> id отчета </returns>
        [HttpGet("build")]
        public int Build() => _reportService.Create(_reportBuilder);

        /// <summary>
        /// Остановка построения отчета
        /// </summary>
        /// <param name="id"> id отчета для остановки </param>
        /// <returns> false если запрашиваемый отчет не сущетсвует </returns>
        [HttpPost("stop")]
        public IActionResult Stop([FromBody] int id)
        {
            try
            {
                _reportService.Terminate(id);
                return Ok();
            }
            catch (NoReportException)
            {
                return NotFound();
            }
        }
    }
}
