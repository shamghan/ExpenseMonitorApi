using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using ExpenseMonitor.Application.User;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Infrastructure.Authorization.Requirments;
using Xunit;

namespace ExpenseMonitor.Infrastructure.Tests.Authorization.Requirments;

public class CreatedMultipleRestaurantsRequirmentHandlerTests
{
    [Fact()]
    public async Task HandleRequirmentAsync_UserHasCreatedMultipleRetaurants_ShouldSucceed()
    {
        var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);
        var restaurant = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurant);
        var requirment = new CreatedMultipleRestaurantsRequirment(2);
        var handler = new CreatedMultipleRestaurantsRequirmentHandler(restaurantsRepositoryMock.Object,
            userContextMock.Object);

        var context = new AuthorizationHandlerContext([requirment], null, null);

        //act
        await handler.HandleAsync(context);

        //assert
        object value = context.HasSucceeded.Should().BeTrue();

    }
    [Fact()]
    public async Task HandleRequirmentAsync_UserHasNotCreatedMultipleRetaurants_ShouldSucceed()
    {
        var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);
        var restaurant = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
          
            new()
            {
                OwnerId = "2"
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurant);
        var requirment = new CreatedMultipleRestaurantsRequirment(2);
        var handler = new CreatedMultipleRestaurantsRequirmentHandler(restaurantsRepositoryMock.Object,
            userContextMock.Object);

        var context = new AuthorizationHandlerContext([requirment], null, null);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}