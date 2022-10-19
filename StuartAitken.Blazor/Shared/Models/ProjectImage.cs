namespace StuartAitken.Blazor.Shared.Models
{
    public class ProjectImage
    {
        #region Public Properties

        public DateTime CreationDate { get; set; }
        public int ID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? PrimaryImage { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }

        #endregion Public Properties
    }
}
