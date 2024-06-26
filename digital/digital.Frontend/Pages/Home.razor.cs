using Blazored.Modal.Services;
using digital.Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages
{
    public partial class Home
    {
        [CascadingParameter] IModalService Modal { get; set; } = default!;


        private void ShowModal()
        {
            Modal.Show<Login>();
        }
    }
}
