using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace StuartAitken.Blazor.Client
{
    public class CustomComponentBase : LayoutComponentBase
    {
        #region Public Properties

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public bool IsAdmin { get; set; } = true;

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        #endregion Public Properties
    }
}