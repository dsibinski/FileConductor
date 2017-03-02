using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileConductorUI.UI.Services
{
    public static class WindowService 
    {

        public static void ShowWindow(object viewModel)
        {
            var win = new Window();
            win.Owner = Application.Current.MainWindow;
            win.WindowStyle = Application.Current.MainWindow.WindowStyle;
            win.Content = viewModel;
            win.Show();
        }

    }
}
