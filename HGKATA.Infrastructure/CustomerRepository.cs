using HGKATA.Core.Domain;

namespace HGKATA.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
  private List<Customer>? customers;

  // Convert plain text to list of customers
  public List<Customer> CreateCustomersFromPlainText(string content)
  {
    customers = new List<Customer>();

    if (string.IsNullOrWhiteSpace(content))
    {
      return customers;
    }

    // Split the content by lines
    var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (var line in lines)
    {
      var nameParts = line.Split(' ');  // Assuming each line has first and last name

      if (nameParts.Length >= 2)
      {
        customers.Add(new Customer { Name = $"{nameParts[0]} {nameParts[1]}" });
      }
    }

    return customers;
  }

  public List<Customer> GetCustomers()
  {
    return customers ?? new List<Customer>() ;
  }
}