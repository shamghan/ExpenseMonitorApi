using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler(ILogger<CreateTransactionCommandHandler> logger,
        IMapper mapper, ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<CreateTransactionCommand, int>
    {
        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new transaction {@Transaction}", request);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var transaction = mapper.Map<Transaction>(request.TransactionDto);
            transaction.UserId = userId;
            transaction.CreatedDate = DateTime.UtcNow;
            var createdId = await transactionsRepository.Create(transaction);
            return createdId;
        }
    }
}
