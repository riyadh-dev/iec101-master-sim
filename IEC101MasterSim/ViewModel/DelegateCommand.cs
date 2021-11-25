using System;
using System.Windows.Input;

namespace IEC101MasterSim.ViewModel;

public class DelegateCommand : ICommand
{
    private readonly Action<object> _excuteChange;
    private readonly Predicate<object> _canExcuteChange;

    public DelegateCommand(Action<object> excuteChange, Predicate<object> canExcuteChange) => (_excuteChange, _canExcuteChange) = (excuteChange, canExcuteChange);

    public DelegateCommand(Action<object> excuteChange) => (_excuteChange, _canExcuteChange) = (excuteChange, (object _) => true);

    public void Execute(object parameter) => _excuteChange(parameter);
    public bool CanExecute(object parameter) => _canExcuteChange(parameter);

    public event EventHandler CanExecuteChanged;
    public void InvokeCanExecuteChange() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
