using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using CompanyNameContest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSingleton<ReportService>();
builder.Services.AddSingleton<Reporter>();

builder.Services.AddScoped<IReportBuilder, ReportBuilder>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();