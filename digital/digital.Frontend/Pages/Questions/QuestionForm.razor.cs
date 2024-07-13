using CurrieTechnologies.Razor.SweetAlert2;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using digital.Shared.Entities.Companies;
using Orders.Frontend.Repositories;

namespace digital.Frontend.Pages.Questions
{
    public partial class QuestionForm
    {
        private EditContext editContext = null!;

        [Parameter, EditorRequired] public Question Question { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; } = false;


        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; } = false;


        protected override void OnInitialized()
        {
            editContext = new(Question);
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
        private async Task SaveAsync()
        {
            await OnValidSubmit.InvokeAsync();



            //company.PorcProduccion = float.Parse(Produccion);
            //company.PorcRRHH = float.Parse(RRHH);
            //company.PorcLogistica = float.Parse(Logistica);
            //company.PorcMantenimiento = float.Parse(Mantenimiento);

            //company.PorcProductoDestinadoAMercadoLocal = float.Parse(MercadoLocal);
            //company.PorcProductoDestinadoAMercadoExterior = float.Parse(MercadoExterior);

            //var responseHttp = await Repository.PostAsync("api/companies/full", Question);

            //if (responseHttp.Error)
            //{
            //    var message = await responseHttp.GetErrorMessageAsync();
            //    if (message.Contains("Debes") || message.Contains("El campo"))
            //    {
            //        message = "Aun quedan campos obligatorios por completar";
            //    }
            //    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            //    return;
            //}

            //return;
        }
    }
}