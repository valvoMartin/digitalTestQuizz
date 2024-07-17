using CurrieTechnologies.Razor.SweetAlert2;
using digital.Shared.Entities.Companies;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using digital.Shared.Entities.Countries;
using Orders.Frontend.Repositories;

namespace digital.Frontend.Pages.Categories
{
    public partial class CategoryForm
    {
        private EditContext editContext = null!;
        //private List<Sector>? sectors = new();
        //private List<Country>? countries = new();

        [Parameter, EditorRequired] public Category Category { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public List<Sector> Sectors { get; set; } = new();
        [Parameter] public List<Country> Countries { get; set; } = new();





        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; } = false;


        protected override void OnInitialized()
        {
            editContext = new(Category);
        }



        private async Task OnDataAnnotationsValidatedAsync()
        {
            await OnValidSubmit.InvokeAsync();
        }



        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited)
            {
                return;
            }

            if (FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }
    }
}
