using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<DbContext> _contextMock;
        private readonly Mock<IPublisher> _publisherMock;
        private readonly CreateSaleCommandHandler _handler;

        public CreateSaleCommandHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _contextMock = new Mock<DbContext>();
            _publisherMock = new Mock<IPublisher>();
            
            _handler = new CreateSaleCommandHandler(
                _saleRepositoryMock.Object,
                _contextMock.Object,
                _publisherMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_CreateSale_And_PublishEvent_WhenCommandIsValid()
        {
            // Arrange
            var command = CreateSaleCommandHandlerTestData.CreateValidCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.SaleId.Should().NotBeEmpty();

            // Verify that the repository's AddAsync was called once with any Sale object.
            _saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);

            // Verify that SaveChangesAsync was called once.
            _contextMock.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
            
            // Verify that the publisher was called once.
            _publisherMock.Verify(p => p.Publish(It.IsAny<object>(), CancellationToken.None), Times.Once);
        }
    }
}
