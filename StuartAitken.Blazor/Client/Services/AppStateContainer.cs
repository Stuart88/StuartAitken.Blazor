namespace StuartAitken.Blazor.Client.Services
{
    public class AppStateContainer
    {
        #region Private Fields

        private bool? isAdmin;
        private string? password;

        #endregion Private Fields

        #region Public Properties

        public bool IsAdmin
        {
            get { return isAdmin ?? false; }
            set
            {
                isAdmin = value;
                Console.WriteLine(IsAdmin);
                NotifyStateChanged();
            }
        }

        public string Password
        {
            get => password ?? string.Empty;
            set
            {
                password = value;
                NotifyPasswordChanged();
            }
        }

        #endregion Public Properties

        #region Public Events

        public event Action? OnChange;

        public event Action? OnPasswordChange;

        #endregion Public Events

        #region Private Methods

        private void NotifyPasswordChanged() => OnPasswordChange?.Invoke();

        private void NotifyStateChanged() => OnChange?.Invoke();

        #endregion Private Methods
    }
}
