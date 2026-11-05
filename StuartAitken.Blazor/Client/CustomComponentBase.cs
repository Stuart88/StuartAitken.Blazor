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
            AppStateContainer.OnChange -= OnAppStateChanged;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnPasswordChange += OnPasswordChanged;
            AppStateContainer.OnChange += OnAppStateChanged;

            await base.OnInitializedAsync();
        }

        #endregion Protected Methods

        #region Private Methods

        // Pages/components that read AppStateContainer.IsAdmin directly in their markup (e.g. to
        // show/hide edit buttons) otherwise never re-render when it changes - MainLayout
        // re-rendering itself doesn't re-render the @Body content it wraps.
        private void OnAppStateChanged()
        {
            this.StateHasChanged();
        }

        private void OnPasswordChanged()
        {
            this.Http.AddAuthHeader(AppStateContainer.Password);
        }

        #endregion Private Methods
    }
}
