using Orders.Frontend.Repositories;

namespace digital.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        public List<Country>? Countries { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var responseHppt = await repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHppt.ActionResponse!;
        }

    }
}