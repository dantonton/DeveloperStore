using DeveloperStore.Domain.Models;

namespace DeveloperStore.Domain.Test.Models
{
    public class SaleTests
    {
        [Fact]
        public void Validate_Should_ReturnNull_When_Items_Are_Valid()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
            {
                new SaleItem { ProductName = "Product A", Quantity = 5, UnitPrice = 20 },
                new SaleItem { ProductName = "Product B", Quantity = 10, UnitPrice = 50 }
            }
            };

            // Act
            var result = sale.Validate();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Validate_Should_ReturnErrorMessage_When_Items_Are_Invalid()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
            {
                new SaleItem { ProductName = "Product A", Quantity = 25, UnitPrice = 20 },
                new SaleItem { ProductName = "Product B", Quantity = 30, UnitPrice = 50 }
            }
            };

            // Act
            var result = sale.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Product A", result);
            Assert.Contains("Product B", result);
        }

        [Fact]
        public void Total_Should_CalculateCorrectly()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
            {
                new SaleItem { ProductName = "Product A", Quantity = 5, UnitPrice = 20 },
                new SaleItem { ProductName = "Product B", Quantity = 10, UnitPrice = 50 }
            }
            };

            // Act
            var total = sale.Total;

            // Assert
            Assert.Equal(490.0M, total); // (5*20 + 10*50) - discounts
        }
    }
}