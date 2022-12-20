using FSDOCS.Client.Pages.Users;
using FSDOCS.Client.Services;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSDOCS.Client.Pages.Groupes
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IUserService userservice { get; set; }
        public IEnumerable<GroupeRole> groupes { get; set; }


        protected override async Task OnInitializedAsync()
        {
            groupes = await userservice.GetGroupesRole();
        }
        public async Task OpenDialog(int num, GroupeRole groupe)
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
                ["editgroupe"] = groupe
            };

            if (num == 1)
            {
                var dialog = DialogService.Show<CreateGroupe>("Creation d'un nouveau groupe utilisateur", options);

                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    groupes = await userservice.GetGroupesRole();
                }
            }

            if (num == 2)
            {
                var dialog = DialogService.Show<EditGroupe>("Mise à jour d'un groupe utilisateur", parameters, options);

                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    groupes = await userservice.GetGroupesRole();
                }
            }

            if (num == 3)
            {
                var dialog = DialogService.Show<DeleteGroupe>("Suppression d'un groupe utilisateur", parameters, options);

                // wait for the dialog to close
                // execution is stopped here until the dialog returns a result
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    groupes = await userservice.GetGroupesRole();
                    // StateHasChanged call is not needed I think
                    //StateHasChanged();
                }
            }
        }
    }
}
