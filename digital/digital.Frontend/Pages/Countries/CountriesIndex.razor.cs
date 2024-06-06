using digital.Frontend.Repositories;
using digital.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        public List<Country>? Countries { get; set; }

       [Inject] private IRepository repository { get; set; } = null!;



        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response!;
        }


    }
}