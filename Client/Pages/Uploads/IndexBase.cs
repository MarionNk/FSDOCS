using FSDOCS.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Headers;


namespace FSDOCS.Client.Pages.Uploads
{
    public class IndexBase:ComponentBase
    {
        [Inject]
        public IUploadService uploadservice { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        private const int V = 5;
        public bool loading = false; 
        public StreamContent fileContent;
        public string fileName;
        private bool shouldRender;

        protected override bool ShouldRender() => shouldRender;
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task UploadFile(InputFileChangeEventArgs e)
        {
            
            long maxFileSize = 1024 * 1024 * 100;
            using var content = new MultipartFormDataContent();
            fileContent = new StreamContent(e.File.OpenReadStream(maxFileSize));

            fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(e.File.ContentType);

            content.Add
            (content: fileContent, name: "\"file\"", fileName: e.File.Name);

            loading = true;
            var response = await uploadservice.UploadAll(content);


            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            Snackbar.Configuration.MaxDisplayedSnackbars = V;
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;
            if (response)
            {
                loading = false ;
                Snackbar.Add("Base de données mise à jour avec succès", Severity.Success);
            }
            else
            {
                loading = false;
                Snackbar.Add("Echec de la mise à jour de la base de données", Severity.Error);
            }

            shouldRender = true;

        }

        public void OpenDialog()
        {
            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                CloseButton = true,
                DisableBackdropClick = true,
                NoHeader = false,
                Position = DialogPosition.TopCenter
            };
            DialogService.Show<FailedUpload>("Failed to upload", options);
        }
    }
}
