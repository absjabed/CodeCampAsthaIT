using System.Text;
using codecampapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


//builder.Configuration.GetSection("Settings").Get<Settings>();

var issuerValue = builder.Configuration.GetValue<string>("Issuer");
var audienceValue = builder.Configuration.GetValue<string>("Audience");
var keyValue = builder.Configuration.GetValue<string>("Secret");

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // jwt-todo: 3
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuerValue, // Replace with your actual issuer
            ValidAudience = audienceValue, // Replace with your actual audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue)) // Replace with your actual secret key
        };
    });


//builder.Services.AddCors();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowSpecificOrigin", builder =>
//     {
//         builder.WithOrigins("http://example.com") // Specify the allowed origin(s)
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//                //.WithMethods("GET") // Allow only the GET method
//     });
// });


//builder.Services.AddResponseCaching();
builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost"; // Redis server host
        options.InstanceName = "SampleInstance"; // Unique instance name
    });


builder.Services.AddSingleton<CodeCampService>(); 
//builder.Services.AddScoped<CodeCampService>(); // Register CodeCampService as Scoped
//builder.Services.AddTransient<CodeCampService>(); // Register CodeCampService as Transient

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(); // jwt-todo: 5
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            // Configure JWT authentication for Swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // or another scheme like "api-key"
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, new[] { "Bearer" } }
            };
            c.AddSecurityRequirement(securityRequirement);
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeCamp API V1");
        c.RoutePrefix = "swagger";
    });
}

//app.UseResponseCaching();

// app.UseCors(builder =>
// {
//     builder.AllowAnyOrigin();
//     builder.AllowAnyMethod();
//     builder.AllowAnyHeader();
// });

//app.UseCustomMiddleware1(); // This will execute first
//app.UseCustomMiddleware2(); // This will execute after the first middleware

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); // jwt-todo: 4

app.MapControllers();

app.Run();
