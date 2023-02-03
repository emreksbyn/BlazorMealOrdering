using BlazorMealOrdering.Shared.Dtos;
using BlazorMealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorMealOrdering.Client.Pages.Users
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        //string baseURL = "https://localhost:44392/api/users";

        protected List<UserDto> userList = new();

        // sayfa yuklenirken tetiklenecek olan metot.
        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected async Task LoadList()
        {
            ServiceResponse<List<UserDto>> response =
                //await HttpClient.GetFromJsonAsync<ServiceResponse<List<UserDto>>>($"{baseURL}/GetAllUsers");
                await HttpClient.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/users/GetAllUsers");

            if (response.Success) userList = response.Value;
        }
    }
}