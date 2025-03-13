using System.Linq.Expressions;
using HGKATA.Core.Domain;

namespace HGKATA.Core.Services;

public interface ICustomerService
{
    /// <summary>
    /// Classifies customers into categories based on specified rules
    /// </summary>
    /// <param name="classificationRules">Dictionary mapping categories to classification rules</param>
    /// <returns>Dictionary mapping customers to their assigned categories</returns>
    IDictionary<string, string[]> ClassifyCustomers(
      IDictionary<CustomerCategory, Expression<Func<Customer, bool>>> classificationRules);
    /// <summary>
    /// Classifies customers into categories based on specified rules
    /// </summary>
    /// <param name="classificationRules">Dictionary mapping categories to classification rules</param>
    /// <returns>Dictionary mapping customers to their assigned categories</returns>
    IDictionary<string, string[]> ClassifyCustomers2(
      IDictionary<CustomerCategory, Expression<Func<Customer, bool>>> classificationRules);

      List<Customer> GetAllCustomers();
}