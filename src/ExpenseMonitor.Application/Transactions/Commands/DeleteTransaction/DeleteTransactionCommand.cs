using MediatR;

namespace ExpenseMonitor.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}
