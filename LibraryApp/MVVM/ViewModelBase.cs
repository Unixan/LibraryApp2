using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LibraryApp.MVVM;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler ReloadRequested = null!;

    private void OnReloadRequested()
    {
        ReloadRequested?.Invoke(this, EventArgs.Empty);
    }

    public void ReloadWindow()
    {
        OnReloadRequested();
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void CloseWindow(Window window)
    {
        window.Close();
    }

}