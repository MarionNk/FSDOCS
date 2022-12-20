using FSDOCS.Client.Services;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace FSDOCS.Client.Pages.Documents
{
    public class DocsEtudBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IPreinscriptionService PreinsService { get; set; }
        public IEnumerable<StudentFiles>? studfiles { get; set; }
        public PreinsGet? preinsInfo { get; set; }
        public string Errormessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                studfiles = await PreinsService.GetStudentFiles(Id);
                preinsInfo = await PreinsService.GetPreins(Id);
            }
            catch (Exception ex)
            {
                Errormessage = ex.Message;
            }
        }
    }
}
