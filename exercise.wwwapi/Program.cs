using exercise.Endpoints;
using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

builder.Services.AddDbContext<StudentDb>(options =>
    options.UseNpgsql(connString));

builder.Services.AddDbContext<LanguageDb>(options =>
    options.UseNpgsql(connString));

builder.Services.AddSingleton<StudentCollection>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton<LanguageCollection>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddSingleton<BookCollection>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.ConfigureStudentEndpoint();
app.ConfigureBookEndpoint();
app.ConfigureLanguageEndpoint();



app.Run();

