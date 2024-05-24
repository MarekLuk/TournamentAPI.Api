using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TournamentAPI.Data.Data;
using TournamentAPI.Core.Entities;
using TournamentAPI.Api.Extensions;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(TournamentMappings));

builder.Services.AddDbContext<TournamentApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentApiContext")));


builder.Services.AddControllers(opt=>opt.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUOW, UOW>();

var app = builder.Build();

//seed databasse
await app.SeedDataAsync();


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
