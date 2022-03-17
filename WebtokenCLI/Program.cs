
using Cocona;
using System.IdentityModel.Tokens.Jwt;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("validate", ([Option(name: "output", shortNames: new char[] {'o'}, Description = "Prints the parsed token to the console if valid" )] bool print,
    [Argument(Description = "The string to validate")]string token) =>
{
    ValidateToken(token, print);

}).WithDescription("Checks if a string is a valid JWToken");

await app.RunAsync();

void ValidateToken(string token, bool? print)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtValid = tokenHandler.CanReadToken(token);

    Console.Write("Token is ");

    if (jwtValid)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Valid");
        if(print != null && print == true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var parsedToken = tokenHandler.ReadJwtToken(token);
            Console.WriteLine(parsedToken);
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid");
    }
}