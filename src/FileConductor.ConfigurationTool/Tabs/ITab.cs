using System;
using System.Windows;
using System.Windows.Input;

namespace ConfigurationTool.Tabs
{
    public interface ITab
    {
        Visibility IsClosable { get; set; }
        string Name { get; set; }
        ICommand CloseCommand { get; set; }
        event EventHandler CloseRequested;
    }
}