using System;
using System.Windows;
using LibraryApp.CommonLibrary;

namespace LibraryApp
{

    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApiHelper.InitializeClient();
        }

        public static string? GetTodaysDate(int extraYear = 0)
        {
            var months = new string[]{
                "Jan", "Feb", "Mar", "Apr", "Mai", "Jun",
                "Jul", "Aug", "Sep", "Okt", "Nov", "Des"
            };
            return $"{DateTime.Today.Day} {months[DateTime.Today.Month - 1]} - {DateTime.Today.Year + extraYear}";
        }
    }
}
