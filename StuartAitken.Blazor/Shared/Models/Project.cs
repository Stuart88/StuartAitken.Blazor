namespace StuartAitken.Blazor.Shared.Models
{
    public class Project
    {
        #region Public Properties

        public DateTime CreationDate { get; set; }
        public string? Description { get; set; }
        public int ID { get; set; }
        public IEnumerable<ProjectImage> Images { get; set; } = new List<ProjectImage>();
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; } = "";
        public DateTime ProjectDate { get; set; }
        public int ProjectDurationDays { get; set; }
        public int? Status { get; set; }
        public string Tech { get; set; } = "";
        public string Type { get; set; } = "";
        public string? Urls { get; set; } = "";
        public int? Views { get; set; }

        #endregion Public Properties
    }
}