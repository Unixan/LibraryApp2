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
        const string url = "https://localhost:7072/libraryBooks";
        try
        {
            using var response = await ApiHelper.ApiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<ObservableCollection<Book>>();
            return result;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            throw;
        }
    }

    internal static async Task<ObservableCollection<User>> GetUsersList()
    {
        const string url = "https://localhost:7072/libraryUsers";
        try
        {
            using var response = await ApiHelper.ApiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<ObservableCollection<User>>();
            return result;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            throw;
        }
    }

    internal static async Task AddUser(User user)
    {
        var url = "https://localhost:7072/libraryUser";
        using var response = await ApiHelper.ApiClient.PostAsJsonAsync(url, user);
        response.EnsureSuccessStatusCode();
    }

    internal static async Task ChangeLoanCardStatus(User user)
    {
        var url = $"https://localhost:7072/libraryUser/{user.UserID}";
        using var response = await ApiHelper.ApiClient.PutAsJsonAsync(url, user);
        response.EnsureSuccessStatusCode();
    }

    internal static async Task DeleteUser(Guid userID)
    {
        var url = $"https://localhost:7072/libraryUser/{userID}";
        using var response = await ApiHelper.ApiClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }

    internal static async Task AddBook(Book book)
    {
        var url = $"https://localhost:7072/libraryBook";
        using var response = await ApiHelper.ApiClient.PostAsJsonAsync(url, book);
        response.EnsureSuccessStatusCode();
    }
    internal static async Task DeleteBook(Guid bookID)
    {
        var url = $"https://localhost:7072/libraryBook/{bookID}";
        using var response = await ApiHelper.ApiClient.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }

    internal static async Task UpdateLoanedStatus()
    {
        var url = $"$\"https://localhost:7072/libraryBook";
        using var response = await ApiHelper.ApiClient.PutAsync<ObservableCollection<Book>>()
    }
}
