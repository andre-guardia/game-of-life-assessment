using GameOfLife.Api.Extensions;
using GameOfLife.Application.Extensions;
using GameOfLife.Core.Api.DependencyInjection;
using GameOfLife.Core.UseCases.DependencyInjection;
using GameOfLife.Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAppCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddContext();
builder.Services.AddRepositories();
builder.Services.AddMapping();
builder.Services.AddUseCases();
builder.Services.AddValidators();
builder.Services.AddMediatorToUseCases();
builder.Services.AddFailFastValidationBehavior();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAppCors();
app.Run();
