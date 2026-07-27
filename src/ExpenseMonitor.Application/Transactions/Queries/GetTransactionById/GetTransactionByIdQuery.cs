using MediatR;
using ExpenseMonitor.Application.Transactions.Dtos;

namespace ExpenseMonitor.Application.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQuery(int id) : IRequest<TransactionDto>
    {
        public int Id { get; set; } = id;
    }
}
