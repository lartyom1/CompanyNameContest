using CompanyNameContest.Interfaces;
using CompanyNameContest.Report;
using CompanyNameContest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();




//?transient/scoped

//builder.Services.AddScoped<IReportBuilder, ReportBuilder>();

//builder.Services.AddTransient<IReportBuilder, ReportBuilder2>();

builder.Services.AddScoped<IReporter, Reporter>();

//TODO
//reportBuilder injection

//https://stackoverflow.com/questions/40900414/dependency-injection-error-unable-to-resolve-service-for-type-while-attempting
//builder.Services.AddScoped<IReportBuilder>(x => new ReportBuilder2());


//builder.Services.AddSingleton<IReporter, Reporter>();
builder.Services.AddSingleton<ReportService>();




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
