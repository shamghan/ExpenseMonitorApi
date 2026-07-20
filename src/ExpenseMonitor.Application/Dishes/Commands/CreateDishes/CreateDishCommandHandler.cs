using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Constants;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Interfaces;
using ExpenseMonitor.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMonitor.Application.Dishes.Commands.CreateDishes
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
       IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRespository,
       IMapper mapper,
       IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new restaurant {@Dishes}", request);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId) ??
                throw new NotFoundException(nameof(Restaurants), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Update))
                throw new ForbidException();


            var dish = mapper.Map<Dish>(request);
            return await dishesRespository.Create(dish);
        }
    }
}
