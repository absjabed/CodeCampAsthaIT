// AuthenticationController.cs

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// jwt-todo: 2
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IConfiguration _config;

    public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserCredentials credentials)
    {
        // Dummy validation (replace with real authentication logic)
        if (IsValidUser(credentials.Username, credentials.Password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.Username),
                // Add additional claims as needed
            };

            var issuerValue = _config.GetValue<string>("Issuer");
            var audienceValue = _config.GetValue<string>("Audience");
            var keyValue = _config.GetValue<string>("Secret");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuerValue,
                audience: audienceValue,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return Unauthorized(); // Invalid credentials
    }

    private bool IsValidUser(string username, string password)
    {
        // Dummy validation (replace with real authentication logic)
        return username == "string" && password == "string";
    }
}

public class UserCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
}
