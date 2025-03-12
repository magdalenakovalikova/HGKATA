using System.Linq.Expressions;
using HGKATA.Core.Domain;
using HGKATA.Core.Services;
using Moq;

namespace HGKATA.Tests;


public class CustomerServiceTests
{
    /*  [Fact]
     public void CreateCustomersFromPlainText_ReturnsCorrectCustomers()
     {
         // Arrange
         var mockRepository = new Mock<ICustomerRepository>();

         // Mocking the method CreateCustomersFromPlainText
         var inputText = "John Doe\nJane Smith\nBob Wilson";
         mockRepository.Setup(repo => repo.CreateCustomersFromPlainText(inputText))
                       .Returns(new List<Customer>
                       {
                           new Customer { Name = "John Doe" },
                           new Customer { Name = "Jane Smith" },
                           new Customer { Name = "Bob Wilson" }
                       });

         var service = new CustomerService(mockRepository.Object);

         // Act
         var customers = service.ClassifyCustomers(new Dictionary<CustomerCategory, Expression<Func<Customer, bool>>>());

         // Assert
         Assert.NotNull(customers);
         Assert.Equal(3, customers.Count);
         Assert.Contains(customers, c => c.Name == "John Doe");
         Assert.Contains(customers, c => c.Name == "Jane Smith");
         Assert.Contains(customers, c => c.Name == "Bob Wilson");
     } */

    [Fact]
    public void GetCustomers_ReturnsListOfCustomers()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();

        // Mocking the method GetCustomers
        mockRepository.Setup(repo => repo.GetCustomers())
                      .Returns(new List<Customer>
                      {
                          new Customer { Name = "John Doe" },
                          new Customer { Name = "Jane Smith" },
                          new Customer { Name = "Bob Wilson" }
                      });

        var service = new CustomerService(mockRepository.Object);

        // Act
        var customers = service.GetAllCustomers();

        // Assert
        Assert.NotNull(customers);
        Assert.Equal(3, customers.Count);
        Assert.Contains(customers, c => c.Name == "John Doe");
        Assert.Contains(customers, c => c.Name == "Jane Smith");
        Assert.Contains(customers, c => c.Name == "Bob Wilson");
    }

    [Fact]
    public void ClassifyCustomers_ShouldCategorizeCorrectly()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        // Mocking the method CreateCustomersFromPlainText
        var inputText = "John Doe\nJane Smith\nBob Wilson";
        mockRepository.Setup(repo => repo.CreateCustomersFromPlainText(inputText))
                      .Returns(new List<Customer>
                      {
                          new Customer { Name = "John Doe" },
                          new Customer { Name = "Jane Smith" },
                          new Customer { Name = "Bob Wilson" },
                          new Customer { Name = "Aden Kirk"}
                      });
        // Use the mocked customers in the service
        var service = new CustomerService(mockRepository.Object);
        var rules = new Dictionary<CustomerCategory, Expression<Func<Customer, bool>>>
        {
            { CustomerCategory.PolloConHierbasLover, c =>
                c.Name.Contains('H', StringComparison.CurrentCultureIgnoreCase) || c.Name.Contains('B', StringComparison.CurrentCultureIgnoreCase) },
            { CustomerCategory.PolloPiriPiriLover, c =>
                !c.Name.ToUpper().Contains('H') && !c.Name.ToUpper().Contains('B') }
        };

        // Act
        var result = service.ClassifyCustomers(rules);

        // Assert
        Assert.Contains(CustomerCategory.PolloConHierbasLover.ToString(), result.First(c => c.Key == "John Doe").Value);
        Assert.Contains(CustomerCategory.PolloConHierbasLover.ToString(), result.First(c => c.Key == "Jane Smith").Value);
        Assert.Contains(CustomerCategory.PolloConHierbasLover.ToString(), result.First(c => c.Key == "Bob Wilson").Value);
        Assert.Contains(CustomerCategory.PolloPiriPiriLover.ToString(), result.First(c => c.Key == "Aden Kirk").Value);
    }
}