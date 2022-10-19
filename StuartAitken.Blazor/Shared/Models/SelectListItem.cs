namespace StuartAitken.Blazor.Shared.Models
{
    public class SelectListItem<T>
    {
        #region Public Properties

        public string GroupName { get; set; }
        public string Label { get; set; }
        public T Value { get; set; }

        #endregion Public Properties
    }
}
