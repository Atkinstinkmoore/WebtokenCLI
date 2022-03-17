
using Cocona;
using System.IdentityModel.Tokens.Jwt;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("validate", ([Argument(Description = "The string to validate")]string token) =>
{
    ValidateToken(token);

}).WithDescription("Checks if a string is a valid JWToken");

await app.RunAsync();

void ValidateToken(string token)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtValid = tokenHandler.CanReadToken(token);

    Console.Write("Token is ");

    if (jwtValid)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Valid");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid");
    }
}