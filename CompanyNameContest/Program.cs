using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using CompanyNameContest.Reporter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



//https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
//???DEPENDENCY INJECTION
builder.Services.AddSingleton<IReportBuilder, ReportBuilder>();
builder.Services.AddSingleton<IReporter, Reporter>();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
