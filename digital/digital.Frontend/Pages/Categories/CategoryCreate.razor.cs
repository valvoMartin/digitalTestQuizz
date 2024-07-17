using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoryCreate
    {
        private Category category = new();
        

        private CategoryForm? categoryForm;
        private List<Sector>? sectors = new();
        private List<Country>? countries = new();

        private bool loading = true;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;



        protected override async Task OnInitializedAsync()
        {
            await LoadCountriesAsync();
            await LoadSectorsAsync();
            loading = false;
        }


        private async Task CreateAsync()
        {
            var httpActionResponse = await Repository.PostAsync("/api/categories/full", category);

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
        }

        private void Return()
        {
            categoryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/categories");
        }


        private async Task LoadSectorsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Sector>>($"api/sectors");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            sectors = responseHttp.Response;
            
        }


        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>($"api/countries");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            countries = responseHttp.Response;
            
        }



    }
}