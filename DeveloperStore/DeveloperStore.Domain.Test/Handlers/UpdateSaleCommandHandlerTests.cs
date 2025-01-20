using NSubstitute;
using DeveloperStore.Domain.Commands;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Handlers;
using DeveloperStore.Domain.Infra;
using DeveloperStore.Domain.Models;
using MediatR;

namespace DeveloperStore.Domain.Test.Handlers
{
    public class UpdateSaleCommandHandlerTests
    {
        private readonly IBaseRepository<Sale> _mockRepository;
        private readonly IMediator _mockMediator;
        private readonly UpdateSaleCommandHandler _handler;

        public UpdateSaleCommandHandlerTests()
        {
            _mockRepository = Substitute.For<IBaseRepository<Sale>>();
            _mockMediator = Substitute.For<IMediator>();
            _handler = new UpdateSaleCommandHandler(_mockRepository, _mockMediator);
        }

        [Fact]
        public async Task Handle_SaleNotFound_ReturnsResourceNotFoundError()
        {
            // Arrange
            var command = new UpdateSaleCommand { SaleId = Guid.NewGuid() };
            _mockRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>()).Returns((Sale)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ResourceNotFound", result.Erro);
            Assert.Equal("Sale not found", result.TypeErro);
        }

        [Fact]
        public async Task Handle_SaleCancelled_ReturnsValidationError()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sale = new Sale { Id = id.ToString(), IsCancelled = true };
            var command = new UpdateSaleCommand { SaleId = id };
            _mockRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>()).Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ValidationError", result.Erro);
            Assert.Equal("Sale cancelled", result.TypeErro);
        }

        [Fact]
        public async Task Handle_ValidSale_UpdatesSaleAndPublishesEvents()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sale = new Sale
            {
                Id = id.ToString(),
                IsCancelled = false,
                Items = new List<SaleItem> { new SaleItem { ProductName = "Item1", Quantity = 1, UnitPrice = 10 } }
            };
            var command = new UpdateSaleCommand
            {
                SaleId = id,
                Customer = "Updated Customer",
                Branch = "Updated Branch",
                Items = new List<SaleItem> { new SaleItem { ProductName = "Item1", Quantity = 2, UnitPrice = 20 } }
            };
            _mockRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>()).Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(command.Customer, sale.Customer);
            Assert.Equal(command.Branch, sale.Branch);

            // Assert
            await _mockRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _mockMediator.Received(1).Publish(Arg.Any<SaleModifiedEvent>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidInputData_ReturnsValidationError()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sale = new Sale
            {
                Id = id.ToString(),
                IsCancelled = false,
                Items = new List<SaleItem> { new SaleItem { ProductName = "Item1", Quantity = 1, UnitPrice = 10 } }
            };
            var command = new UpdateSaleCommand
            {
                SaleId = id,
                Customer = "Updated Customer",
                Branch = "Updated Branch",
                Items = new List<SaleItem> { new SaleItem { ProductName = "Item1", Quantity = 25, UnitPrice = 20 } }
            };
            _mockRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>()).Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ValidationError", result.Erro);
            Assert.Contains("Invalid input data", result.TypeErro);
            Assert.Contains("The products (Item1) must contain 20 or less items", result.Detail);
        }
    }
}
