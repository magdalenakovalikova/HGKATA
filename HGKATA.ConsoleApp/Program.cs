// See https://aka.ms/new-console-template for more information
using HGKATA.Core.Services;
using HGKATA.Infrastructure;

Console.WriteLine("Hello, World!");
if (args.Length == 0)
{
    Console.WriteLine("Please provide a file path as an argument.");
    return;
}

string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"File not found: {filePath}");
    return;
}

try
{
    var repository = new CustomerRepository();
    var customers = await repository.CreateCustomersFromFileAsync(filePath);

    var customerService = new CustomerService(repository);
    var response = customerService.ClassifyCustomers2();
    foreach (KeyValuePair<string, string[]> category in response)
    {
        Console.WriteLine($"{category.Key}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
