using MediatR;
using ExpenseMonitor.Application.Transactions.Dtos;

namespace ExpenseMonitor.Application.Transactions.Queries.GetAllTransactions
{
    public class GetAllTransactionsQuery : IRequest<IEnumerable<TransactionDto>>
    {
    }
}
