using System.Linq.Expressions;
using HGKATA.Core.Domain;

namespace HGKATA.Core.Services;

public class CustomerService : ICustomerService
{
    public List<Customer>? Customers { get; set; }

    public IDictionary<CustomerCategory, Expression<Func<Customer, bool>>> _classificationRules;

    /* public CustomerService(List<Customer>? customers = null)
    {
        this.customers = customers ?? new List<Customer>
        {
            new() { Name = "John Doe" },
            new() { Name = "Jane Smith" },
            new() { Name = "Bob Wilson" }
        };
    } */

    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _classificationRules = new Dictionary<CustomerCategory, Expression<Func<Customer, bool>>>
            {
                { CustomerCategory.PolloConHierbasLover, c =>
                    c.Name.Contains('H', StringComparison.CurrentCultureIgnoreCase) || c.Name.Contains('B', StringComparison.CurrentCultureIgnoreCase) },
                { CustomerCategory.PolloPiriPiriLover, c =>
                    !c.Name.ToUpper().Contains('H') && !c.Name.ToUpper().Contains('B') }
            };
    }

    public List<Customer> GetAllCustomers() => _customerRepository.GetCustomers();

    public IDictionary<string, string[]> ClassifyCustomers(
      IDictionary<CustomerCategory, Expression<Func<Customer, bool>>> classificationRules)
    {
        var result = new Dictionary<string, string[]>();
        Customers = _customerRepository.GetCustomers();
        if (Customers == null)
        {
            return result;
        }

        foreach (var customer in Customers)
        {
            var categories = new List<string>();
            foreach (var rule in classificationRules)
            {
                var compiledRule = rule.Value.Compile();
                if (compiledRule(customer))
                {
                    categories.Add(rule.Key.ToString());
                    //break;
                }
                result[customer.Name] = categories.ToArray();
            }
        }

        return result;
    }
    public IDictionary<string, string[]> ClassifyCustomers2(
      IDictionary<CustomerCategory, Expression<Func<Customer, bool>>> classificationRules)
    {
        var result = new Dictionary<string, string[]>();
        Customers = _customerRepository.GetCustomers();
        if (Customers == null)
        {
            return result;
        }

        // Initialize dictionary with empty arrays for each category
        foreach (var category in classificationRules.Keys)
        {
            result[category.ToString()] = Array.Empty<string>();
        }

        // Group customers by category
        foreach (var rule in classificationRules)
        {
            var compiledRule = rule.Value.Compile();
            var matchingCustomers = Customers
                .Where(compiledRule)
                .Select(c => c.Name)
                .ToArray();

            result[rule.Key.ToString()] = matchingCustomers;
        }

        return result;
    }
}
