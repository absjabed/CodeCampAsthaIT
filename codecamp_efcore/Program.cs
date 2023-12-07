using System.Text.Json.Serialization;
using codecamp_efcore.Helpers;
using codecamp_efcore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
// {
//     var services = builder.Services;
//     var env = builder.Environment;
 
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();
// }

var app = builder.Build();


// ***migrate any database changes on startup (includes initial db creation)***
// using (var scope = app.Services.CreateScope())
// {
//     var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     dataContext.Database.Migrate();
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global cors policy
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

//app.Run("http://localhost:4000");
app.Run();