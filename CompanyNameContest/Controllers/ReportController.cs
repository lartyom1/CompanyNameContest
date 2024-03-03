using CompanyNameContest.Interfaces;
using CompanyNameContest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNameContest.Controllers
{

    [ApiController]
    [Route("[controller]")] //report
    public class ReportController : Controller//, IReportBuilder
    {
        private readonly IReportBuilder _reportBuilder;

        private ReportService reportService = new ReportService();
      
        public ReportController(IReportBuilder reportBuilder)
        {
            _reportBuilder = reportBuilder;
        }

        // GET: ReportController/Get
        [HttpGet("build")]//attribute
        public int Build() => reportService.Create();


        public IActionResult Stop(int id)
        {
            try
            {
                reportService.Terminate(id);
                return Ok();
            }
            catch (NoReportException)
            {
                return NotFound();
            }
        }
    }
}
