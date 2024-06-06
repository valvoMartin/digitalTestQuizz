using digital.Frontend.Repositories;
using digital.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;

namespace digital.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        public List<Country>? Countries { get; set; }

       [Inject] private IRepository repository { get; set; } = null!;

        //protected async override Task OnInitializedAsync()
        //{
        //    var responseHppt = await repository.GetAsync<List<Country>>("api/countries");
        //    Countries = responseHppt.Response!;
        //}

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response!;
        }


    }
}