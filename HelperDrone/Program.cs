using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using HelperDrone.Repositories;
using Microsoft.OpenApi.Models;
using Oracle.ManagedDataAccess.Client;
using RabbitMQ.Client;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IAreaRiscoRepository, AreaRiscoRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<SentimentAnalysis>();

var sentimentAnalysis = new SentimentAnalysis();

sentimentAnalysis.TrainModel();

var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddScoped<IDbConnection>(sp => new OracleConnection(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HelperDrone API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelperDrone V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
