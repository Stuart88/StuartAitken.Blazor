using MudBlazor;

namespace StuartAitken.Blazor.Client.Helpers
{
    public static class DialogHelpers
    {
        #region Public Methods

        /// <summary>
        /// MudBlazor removed the simple IDialogService.ShowMessageBox(...) extension in v7; this restores the same call shape using MudMessageBox directly.
        /// </summary>
        public static async Task<bool?> ShowMessageBox(
            this IDialogService dialogService,
            string title,
            string message,
            string yesText = "OK",
            string? cancelText = null
        )
        {
            var parameters = new DialogParameters<MudMessageBox>
            {
                { x => x.Message, message },
                { x => x.YesText, yesText },
                { x => x.CancelText, cancelText }
            };

            IDialogReference reference = await dialogService.ShowAsync<MudMessageBox>(title, parameters);
            DialogResult? result = await reference.Result;

            if (result == null || result.Canceled || result.Data is not bool data)
                return null;

            return data;
        }

        #endregion Public Methods
    }
}
