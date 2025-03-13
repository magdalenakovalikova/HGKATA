namespace HGKATA.Core.Domain;

public interface ICustomerRepository
{
  List<Customer> CreateCustomersFromPlainText(string content);
  List<Customer> GetCustomers();

  Task<List<Customer>> CreateCustomersFromFileAsync(string filePath);
}