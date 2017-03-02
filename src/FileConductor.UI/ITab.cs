using System;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace FileConductorUI.UI
{
    public interface ITab
    {
        string Name { get; set; }

        ICommand CloseCommand { get; set; }
        event EventHandler CloseRequested;
    }


    public abstract class Tab : ITab
    {
        public Tab()
        {
            CloseCommand = new ActionCommand(x => CloseRequested?.Invoke(this, EventArgs.Empty));
        }

        public string Name { get; set; }
        public ICommand CloseCommand { get; set; }
        public event EventHandler CloseRequested;
    }
}