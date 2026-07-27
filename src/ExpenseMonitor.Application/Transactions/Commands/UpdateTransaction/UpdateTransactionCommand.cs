using MediatR;
using ExpenseMonitor.Application.Transactions.Dtos;

namespace ExpenseMonitor.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateTransactionDto TransactionDto { get; set; }
        public UpdateTransactionCommand()
        {
            TransactionDto = new UpdateTransactionDto();
        }
    }
}
