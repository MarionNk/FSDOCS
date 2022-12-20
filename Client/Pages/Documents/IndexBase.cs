using FSDOCS.Client.Services;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;

namespace FSDOCS.Client.Pages.Documents
{
    public class IndexBase:ComponentBase
    {
        [Inject]
        public IPreinscriptionService PreinsService { get; set; }
        public IEnumerable<PreinsGet> Preins { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string token { get; set; }
        public bool enabled = true;
        public string searchString1 = "";

        public bool FilterFunc1(PreinsGet element) => FilterFunc(element, searchString1);

        public bool FilterFunc(PreinsGet element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Etudiant.Noms.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Etudiant.Prenoms.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CodeEtape.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CodeAA.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.CodePreinscription}".Contains(searchString))
                return true;
            return false;
        }
        
        protected override async Task OnInitializedAsync()
            {
                Preins = await PreinsService.GetItems();
            }

        public async Task Gotodocs(int codepreins)
        {
            NavigationManager.NavigateTo($"/documents/{codepreins}");
        }
    }
}
