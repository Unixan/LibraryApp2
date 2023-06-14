using System.Windows;
using System.Windows.Input;
using LibraryApp.CommonLibrary;
using LibraryApp.ViewModel;

namespace LibraryApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel(this);
            DataContext = mainWindowViewModel;
        }
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await LibraryService.PopulateLists();
        }
    }
}
