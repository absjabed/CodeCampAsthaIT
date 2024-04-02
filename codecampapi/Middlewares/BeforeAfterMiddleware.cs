using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class CustomMiddleware1
{
    private readonly RequestDelegate _next;

    public CustomMiddleware1(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Custom Middleware 1: Before next middleware");

        // Call the next middleware in the pipeline
        await _next(context);

        Console.WriteLine("Custom Middleware 1: After next middleware");
    }
}

public class CustomMiddleware2
{
    private readonly RequestDelegate _next;

    public CustomMiddleware2(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Custom Middleware 2: Before next middleware");

        // Call the next middleware in the pipeline
        await _next(context);

        Console.WriteLine("Custom Middleware 2: After next middleware");
    }
}

public static class BeforeAfterMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware1(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware1>();
    }

    public static IApplicationBuilder UseCustomMiddleware2(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware2>();
    }
}
