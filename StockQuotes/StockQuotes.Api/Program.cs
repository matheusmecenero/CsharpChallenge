using AutoMapper;
using FluentValidation;
using StockQuotes.Api.Mapper;
using StockQuotes.Api.Models;
using StockQuotes.Api.Validators;
using StockQuotes.Domain;
using StockQuotes.Repository;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service;
using StockQuotes.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Just using Singleton because I'm not using EF/database map. So I have to keep in memory. 
builder.Services.AddSingleton<IChatRepository, ChatRepository>();
builder.Services.AddSingleton<IChatService, ChatService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();

var config = new MapperConfiguration(cfg =>
{
	cfg.AddProfile<ChatProfile>();
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();