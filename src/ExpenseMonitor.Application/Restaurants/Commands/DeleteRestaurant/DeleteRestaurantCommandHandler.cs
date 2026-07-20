using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Restaurants.Command.CreateRestaurant;
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

namespace ExpenseMonitor.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper, IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id: {RestaurantId}",request.Id);
            var restaurant = await restaurantsRepository.GetById(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Delete))
                throw new ForbidException(); 

            await restaurantsRepository.Delete(restaurant);
            
        }
    }
}
