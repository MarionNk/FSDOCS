using FSDOCS.Client.Services;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSDOCS.Client.Pages.Users
{
    public class IndexBase:ComponentBase
    {
        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IUserService userservice { get; set; }
        public IEnumerable<UserGet> users { get; set; }


        protected override async Task OnInitializedAsync()
        {
            users = await userservice.GetUsers();
        }
        public async Task OpenDialog(int num,UserGet user)
        {
            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Small,
                CloseButton = true,
                DisableBackdropClick = true,
                NoHeader = false,
                Position = DialogPosition.TopCenter
            };
            var parameters = new DialogParameters
            {
                ["user"] = user
            };

            if (num == 1)
            {
                var dialog=DialogService.Show<CreateUser>("Creation d'un nouvel utilisateur",options);

                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    users = await userservice.GetUsers();
                }
            }

            if (num == 2)
            {
                var dialog=DialogService.Show<EditUser>("Mise à jour d'un utilisateur",parameters, options);

                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    users = await userservice.GetUsers();
                }
            }

            if (num == 3)
            {
                var dialog=DialogService.Show<DeleteUser>("Suppression d'un utilisateur",parameters, options);

                // wait for the dialog to close
                // execution is stopped here until the dialog returns a result
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    users = await userservice.GetUsers();
                    // StateHasChanged call is not needed I think
                    //StateHasChanged();
                }
            }
        }

    }
}
