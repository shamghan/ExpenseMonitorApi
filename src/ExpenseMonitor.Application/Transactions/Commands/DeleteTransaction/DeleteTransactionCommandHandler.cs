using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler(ILogger<DeleteTransactionCommandHandler> logger,
        ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<DeleteTransactionCommand>
    {
        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting transaction with id {TransactionId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var transaction = await transactionsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(Transaction), request.Id.ToString());
            await transactionsRepository.Delete(transaction);
        }
    }
}
