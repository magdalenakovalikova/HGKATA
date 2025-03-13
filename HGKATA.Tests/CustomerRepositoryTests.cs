using HGKATA.Infrastructure;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class CustomerRepositoryTests
{
  [Fact]
  public async Task GetCustomersFromFileAsync_ReturnsCorrectCustomers()
  {
    // Arrange
    var mockHttpClient = new Mock<HttpClient>(); // No actual HTTP requests needed
    var repository = new CustomerRepository();
    string filePath = Path.Combine(AppContext.BaseDirectory, "assets", "hg-be-kata-data.dat");
    //var filePath = "C:\\Users\\Magdalena\\source\\repos\\HGKATA\\HGKATA.Tests\\file.dat"; // You can mock the file reading in tests
    await repository.CreateCustomersFromFileAsync(filePath);

    // Act
    var customers = repository.GetCustomers();

    // Assert
    Assert.NotEmpty(customers);
  }
}