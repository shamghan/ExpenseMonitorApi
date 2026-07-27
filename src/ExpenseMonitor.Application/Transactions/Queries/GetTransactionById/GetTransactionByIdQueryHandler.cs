using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ExpenseMonitor.Application.Transactions.Dtos;
using ExpenseMonitor.Domain.Entities;
using ExpenseMonitor.Domain.Exceptions;
using ExpenseMonitor.Domain.Repositories;
using ExpenseMonitor.Application.User;

namespace ExpenseMonitor.Application.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler(ILogger<GetTransactionByIdQueryHandler> logger,
        IMapper mapper, ITransactionsRepository transactionsRepository, IUserContext userContext) : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting transaction by id {TransactionId}", request.Id);
            var userIdString = userContext.GetCurrentUser()?.Id ?? throw new InvalidOperationException("User not authenticated");
            var userId = Guid.Parse(userIdString);
            var transaction = await transactionsRepository.GetById(request.Id, userId) ?? throw new NotFoundException(nameof(Transaction), request.Id.ToString());
            var transactionDto = mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }
    }
}
