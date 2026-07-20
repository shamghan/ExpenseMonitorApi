using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Restaurants.Dtos;
using ExpenseMonitor.Application.Restaurants.Queries.GetAllRestaurants;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Interfaces;
using ExpenseMonitor.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMonitor.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper, IRestaurantsRepository restaurantsRepository,
        IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantsDto>
    {
        public async Task<RestaurantsDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant {by id {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetById(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            var restaurantDto = mapper.Map<RestaurantsDto>(restaurant);
            restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);
            return restaurantDto;
        }
    }
}
