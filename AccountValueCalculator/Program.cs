// See https://aka.ms/new-console-template for more information
using AccountValueCalculator.Domain;
using AccountValueCalculator.Infrastructure;
using AccountValueCalculator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICalculatorService, CalculatorService>();
        services.AddSingleton<ICsvReaderService, CsvReaderService>();
    }).Build();
var calculatorService = host.Services.GetRequiredService<ICalculatorService>();
var csvReaderService = host.Services.GetRequiredService<ICsvReaderService>();

string CsvFilePath = @"..\..\..\account_20230228.csv";
var transactions = csvReaderService.ReadTransactions(CsvFilePath);
//plus tard faire aussi depuis le csv la lecture de:
//-Compte au 28/02/2023 : 8300.00 EUR
//-JPY / EUR : 0.482
//-USD / EUR : 1.445

DateTime startDate = new(2022, 1, 1);
DateTime endDate = new(2023, 3, 1);
DateTime validTargetDate;
while (true)
{
    Console.Write("Entrez une date (dd/MM/yyyy) : ");
    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validTargetDate))
    {
        Console.WriteLine("Date invalide");
        continue;
    }

    if (validTargetDate < startDate || validTargetDate > endDate)
    {
        Console.WriteLine($"La date doit être comprise entre {startDate:dd/MM/yyyy} et {endDate:dd/MM/yyyy}");
        continue;
    }
    break;
}

decimal balance = calculatorService.CalculateValueAccount(transactions, validTargetDate);
Console.WriteLine($"Solde au {validTargetDate} : {balance} EUR");

var topCategories = calculatorService.GetTopDebitCategories(transactions, 3);
Console.WriteLine($"les 3  plus grandes catégories de débit sur tout l’historique");

foreach (var cat in topCategories)
{
    Console.WriteLine($"{cat.Key} : {cat.Value}");
}