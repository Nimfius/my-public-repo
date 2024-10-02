using System.Net;
using Microsoft.EntityFrameworkCore;
using Postre;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(Dns.GetHostName());
Console.WriteLine(Dns.GetHostEntryAsync(Dns.GetHostName()).GetAwaiter().GetResult().AddressList[0]);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddDbContext<BooksDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("PostgreConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
