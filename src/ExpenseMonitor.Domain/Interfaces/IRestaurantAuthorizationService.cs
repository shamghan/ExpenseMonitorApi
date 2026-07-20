using ExpenseMonitor.Domain.Constants;
using ExpenseMonitor.Domain.Entities;

namespace ExpenseMonitor.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperations resourceOperations);
}
