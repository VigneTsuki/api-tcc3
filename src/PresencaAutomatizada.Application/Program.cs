using Microsoft.AspNetCore.Mvc;
using PresencaAutomatizada.Application.Api.Middlewares;
using PresencaAutomatizada.Application.Data.Base;
using PresencaAutomatizada.Application.Data.Repository;
using PresencaAutomatizada.Application.Domain.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<ISalaRepository, SalaRepository>();
builder.Services.AddTransient<ICronogramaRepository, CronogramaRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();

var app = builder.Build();

app.UseMiddleware<UnhandledMiddleware>();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
