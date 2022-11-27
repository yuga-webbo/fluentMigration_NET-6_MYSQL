using FluentMigrationEntity.Migrations;
using FluentMigrator.Runner;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Fluent Migration 
builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddMySql5()
                    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .ScanIn(System.Reflection.Assembly.GetExecutingAssembly()).For.All())
                    .AddLogging(config => config.AddFluentMigratorConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Database.Migrate(builder.Configuration.GetConnectionString("DefaultConnection"),
    builder.Configuration.GetConnectionString("DatabaseName"), builder.Configuration.GetConnectionString("server"),
    builder.Configuration.GetConnectionString("userName"), builder.Configuration.GetConnectionString("password")
    );

using var scope = app.Services.CreateScope();
var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
migrator.MigrateUp();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


