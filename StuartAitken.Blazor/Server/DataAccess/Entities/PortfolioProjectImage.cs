using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectImage")]
    public class PortfolioProjectImage : AuditedEntityBase
    {
        #region Public Properties

        public int? PrimaryImage { get; set; }

        public virtual PortfolioProject? Project { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public int Status { get; set; }

        #endregion Public Properties
    }
}
