using BankHSE.Models;

namespace BankHSE.Observer;

public interface IOperationObserver
{
    void OnOperationCreated(Operation operation);
}