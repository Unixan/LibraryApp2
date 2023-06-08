using System;
using System.Windows;
using LibraryApp.CommonLibrary;

namespace LibraryApp
{

    public partial class App : Application
    {
        internal static LibraryService? LibraryService;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LibraryService = new LibraryService();
        }
    }
}
