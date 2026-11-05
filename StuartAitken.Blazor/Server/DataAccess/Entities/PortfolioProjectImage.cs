using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectImage")]
    public class PortfolioProjectImage : AuditedEntityBase
    {
        #region Public Properties

        public int? PrimaryImage { get; set; }

        public virtual PortfolioProject? Project { get; set; }

        // DB column is "ProjectID" - the portfolio database uses a case-sensitive (binary)
        // collation, so this must match exactly or EF throws "Invalid column name".
        [Column("ProjectID")]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public int SortOrder { get; set; }

        public int Status { get; set; }

        #endregion Public Properties
    }
}
