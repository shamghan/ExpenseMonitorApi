using MediatR;
using ExpenseMonitor.Application.Transactions.Dtos;

namespace ExpenseMonitor.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public CreateTransactionDto TransactionDto { get; set; }
        public CreateTransactionCommand()
        {
            TransactionDto = new CreateTransactionDto();
        }
    }
}
