using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace FileConductor.ConfigurationTool.Tabs
{
    public abstract class Tab : ITab
    {
        protected Tab()
        {
            IsClosable = Visibility.Visible;
            CloseCommand = new ActionCommand(x => CloseRequested?.Invoke(this, EventArgs.Empty));
        }

        public string Name { get; set; }
        public ICommand CloseCommand { get; set; }
        public event EventHandler CloseRequested;
        public Visibility IsClosable { get; set; }
    }
}
