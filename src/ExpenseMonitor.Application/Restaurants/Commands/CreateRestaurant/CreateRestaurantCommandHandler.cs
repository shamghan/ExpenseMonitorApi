using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Restaurants.Dtos;
using ExpenseMonitor.Application.User;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMonitor.Application.Restaurants.Command.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, 
        IMapper mapper, IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("{UserName} [{UserId}] a new restaurant {@Restaurant}", currentUser.Email, currentUser.Id, request);
            var restaurant = mapper.Map<Restaurant>(request.RestaurantDto);
            restaurant.OwnerId = currentUser.Id;
            var createdId = await restaurantsRepository.Create(restaurant);
            restaurant.Id = createdId;
            return createdId;
        }
    }
}
