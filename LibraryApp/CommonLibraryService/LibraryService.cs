using LibraryApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryApp.CommonLibrary;

public static class LibraryService
{
    internal static ObservableCollection<Book>? Books;
    internal static ObservableCollection<User>? Users;
    internal static User? SelectedUser;
    internal static Book? SelectedBook;

    public static async Task PopulateLists()
    {
        Users = await GetUsersList();
        Books = await GetBooksList();
    }

    internal static async Task<ObservableCollection<Book>> GetBooksList()
    {
        var url = "https://localhost:7072/libraryBooks";
        using var response = await ApiHelper.ApiClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<ObservableCollection<Book>>();
            return result;
        }
        else
        {
            MessageBox.Show($"Error!\n{response.ReasonPhrase}");
            throw new Exception(response.ReasonPhrase);
        }
    }

    internal static async Task<ObservableCollection<User>> GetUsersList()
    {
        var url = "https://localhost:7072/libraryUsers";
        using var response = await ApiHelper.ApiClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<ObservableCollection<User>>();
            return result;
        }
        else
        {
            MessageBox.Show($"Error!\n{response.ReasonPhrase}");
            throw new Exception(response.ReasonPhrase);
        }
    }

    internal static async Task AddUser(User user)
    {
        var url = "https://localhost:7072/libraryUser";
        using var response = await ApiHelper.ApiClient.PostAsJsonAsync(url, user);
        response.EnsureSuccessStatusCode();
    }

    internal static async Task DeleteUser(Guid userID)
    {
        var url = $"https://localhost:7072/libraryUser/{userID}";
        var response = await ApiHelper.ApiClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }
}
