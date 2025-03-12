namespace HGKATA.Core.Domain;

public interface ICustomerRepository
{
  List<Customer> CreateCustomersFromPlainText(string content);
  List<Customer> GetCustomers();
}