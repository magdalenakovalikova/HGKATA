using Microsoft.AspNetCore.Mvc;

namespace HGKATA.API.Controllers;
[ApiController]
[Route("[Controller]")]
public class CustomersController(ILogger<CustomersController> _logger) : ControllerBase
{
    public enum CustomerCategory
    {
        PolloPiriPiriLover,
        PolloConHierbasLover
    }

    [HttpPost("ClassifyCustomers")]
    public async Task<ActionResult<CustomerClassification>> ClassifyCustomers([FromBody] string content)
    {
        try
        {
            if (string.IsNullOrEmpty(content))
                return BadRequest();

            await Task.Yield();

            _logger.LogInformation("Processing customer classification with content: {Content}", content);
            
            // TODO: Add actual customer classification logic here
            var response = new CustomerClassification
            {
                Categories = new List<CategorySegment>
                {
                    new() { Category = CustomerCategory.PolloPiriPiriLover.ToString(), Customers = new List<string>() },
                    new() { Category = CustomerCategory.PolloConHierbasLover.ToString(), Customers = new List<string>() }
                }
            };

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