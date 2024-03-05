using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using CompanyNameContest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();





//builder.Services.AddSingleton<IReportBuilder, ReportBuilder>();
builder.Services.AddSingleton<IReporter, Reporter>();

builder.Services.AddSingleton<ReportService>();//?


var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
