using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FileConductor.Presentation;
using Topshelf;

namespace FileConductor
{
    class FileConductorUI
    {
        [STAThread]
        static void Main(string[] args)
        {
           Application app = new Application();
            app.Run(new MainWindow(new MainWindowsViewModel()));
            // var mainWindo = (MainWindow)app.MainWindow;


       
        }
    }
}
