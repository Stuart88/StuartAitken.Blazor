using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace StuartAitken.Blazor.Client
{
    public class CustomComponentBase : LayoutComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public bool IsAdmin { get; set; } = true;
    }
}
