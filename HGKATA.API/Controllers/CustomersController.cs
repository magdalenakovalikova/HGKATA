using System.Linq.Expressions;
using HGKATA.Core.Domain;
using HGKATA.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HGKATA.API.Controllers;
[ApiController]
[Route("[Controller]")]
public class CustomersController(ILogger<CustomersController> _logger, ICustomerRepository customerRepository) : ControllerBase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerService _customerService = new CustomerService(customerRepository);
    /*     public enum CustomerCategory
        {
            PolloPiriPiriLover,
            PolloConHierbasLover
        } */

    [HttpPost("ClassifyCustomers")]
    public async Task<ActionResult<CustomerClassification>> ClassifyCustomers([FromBody] string content)
    {
        try
        {
            if (string.IsNullOrEmpty(content))
                return BadRequest();

            await Task.Yield();

            _logger.LogInformation("Processing customer classification with content: {Content}", content);

            var customers = _customerRepository.CreateCustomersFromPlainText(content);

            _logger.LogInformation("Customers found: {number}", customers.Count);

            // TODO: Add actual customer classification logic here
            /* var response = new CustomerClassification
            {
                Categories = new List<CategorySegment>
                {
                    new() { Category = CustomerCategory.PolloPiriPiriLover.ToString(), Customers = new List<string>() },
                    new() { Category = CustomerCategory.PolloConHierbasLover.ToString(), Customers = new List<string>() }
                }
            }; */
            Dictionary<CustomerCategory, Expression<Func<HGKATA.Core.Domain.Customer, bool>>> rules = new Dictionary<CustomerCategory, Expression<Func<Customer, bool>>>
            {
                { CustomerCategory.PolloConHierbasLover, c =>
                    c.Name.Contains('H', StringComparison.CurrentCultureIgnoreCase) || c.Name.Contains('B', StringComparison.CurrentCultureIgnoreCase) },
                { CustomerCategory.PolloPiriPiriLover, c =>
                    !c.Name.ToUpper().Contains('H') && !c.Name.ToUpper().Contains('B') }
            };
            var response = _customerService.ClassifyCustomers2(rules);


            _logger.LogInformation("Successfully classified customers");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error classifying customers");
            return StatusCode(500, "Internal server error");
        }
    }

    public class CustomerClassification
    {
        public List<CategorySegment>? Categories { get; set; }
    }

    public class CategorySegment
    {
        public required string Category { get; set; }
        public List<string>? Customers { get; set; }
    }

}