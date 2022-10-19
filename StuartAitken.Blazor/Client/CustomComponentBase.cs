using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using StuartAitken.Blazor.Client.Helpers;
using StuartAitken.Blazor.Client.Services;

namespace StuartAitken.Blazor.Client
{
    public class CustomComponentBase : LayoutComponentBase, IDisposable
    {
        #region Public Properties

        [Inject]
        public AppStateContainer AppStateContainer { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            AppStateContainer.OnPasswordChange -= OnPasswordChanged;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnPasswordChange += OnPasswordChanged;

            await base.OnInitializedAsync();
        }

        #endregion Protected Methods

        #region Private Methods

        private void OnPasswordChanged()
        {
            this.Http.AddAuthHeader(AppStateContainer.Password);
        }

        #endregion Private Methods
    }
}
