
using Cocona;
using System.IdentityModel.Tokens.Jwt;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("validate", (TokenParserCommands commonParameters, [Argument(Description = "The string to validate")]string token) =>
{
    ValidateToken(token, commonParameters.print);

}).WithDescription("Checks if a string is a valid JWToken");



await app.RunAsync();


void ValidateToken(string token, bool print)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtValid = tokenHandler.CanReadToken(token);

    Console.Write("Token is ");

    if (jwtValid)
    {
        PrintValid(token, print, tokenHandler);
    }
    else
    {
        PrintInvalid();
    }
}

void PrintInvalid()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid");
}

void PrintValid(string token, bool print, JwtSecurityTokenHandler tokenHandler)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Valid");
    if (print)
    {
        Console.ForegroundColor = ConsoleColor.White;
        var parsedToken = tokenHandler.ReadJwtToken(token);
        Console.WriteLine(parsedToken);
    }
}
record TokenParserCommands(
    [Option(name: "output", shortNames: new char[] { 'o' }, Description = "Prints the parsed token to the console if valid")] bool print = false
    ) : ICommandParameterSet;