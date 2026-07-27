using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Transactions.Dtos;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Transactions.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryHandler(ILogger<GetAllTransactionsQueryHandler> logger,
        IMapper mapper, ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>
    {
        public async Task<IEnumerable<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all transactions");
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var transactions = await transactionsRepository.GetByUserId(userId);
            var transactionDtos = mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return transactionDtos;
        }
    }
}
