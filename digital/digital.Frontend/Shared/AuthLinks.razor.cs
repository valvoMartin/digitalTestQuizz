using Blazored.Modal.Services;
using digital.Frontend.Pages.Auth;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Shared
{
    public partial class AuthLinks
    {
        [CascadingParameter] IModalService Modal { get; set; } = default!;


        private void ShowModal()
        {
            Modal.Show<Login>();
        }

    }
}