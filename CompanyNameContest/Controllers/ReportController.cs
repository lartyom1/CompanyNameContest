using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using CompanyNameContest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNameContest.Controllers
{
    [ApiController]
    [Route("[controller]")] //report
    public class ReportController : Controller
    {
        private /*readonly */ReportService _reportService;
        private IReportBuilder _reportBuilder;//?


        public ReportController(IReportBuilder reportBuilder, ReportService reportService)
        {
            _reportBuilder = reportBuilder;
            _reportService = reportService;
        }

        // GET: report/build
        [HttpGet("build")]//attribute
        public int Build() => _reportService.Create(_reportBuilder);

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
