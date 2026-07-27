using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler(ILogger<UpdateTransactionCommandHandler> logger,
        IMapper mapper, ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<UpdateTransactionCommand>
    {
        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating transaction with id {TransactionId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var transaction = await transactionsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(Transaction), request.Id.ToString());
            mapper.Map(request.TransactionDto, transaction);
            await transactionsRepository.SaveChanges();
        }
    }
}
