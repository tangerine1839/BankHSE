using System.Diagnostics;

namespace BankHSE.Commands;

public class TimingDecorator : ICommand
{
    private readonly ICommand _command;

    public TimingDecorator(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        var stopwatch = Stopwatch.StartNew();
        _command.Execute();
        stopwatch.Stop();
        Console.WriteLine($"Time of command execution: {stopwatch.ElapsedMilliseconds} ms");
    }
}