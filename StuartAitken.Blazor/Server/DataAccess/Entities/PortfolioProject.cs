using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProject")]
    public class PortfolioProject : AuditedEntityBase
    {
        #region Public Properties

        public string? Description { get; set; }
        public virtual Collection<PortfolioProjectImage>? Images { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ProjectDate { get; set; }
        public int ProjectDurationDays { get; set; }
        public int? Status { get; set; }
        public string Tech { get; set; } = null!;
        public string Type { get; set; } = null!;

        // DB column is "URLs" - the portfolio database uses a case-sensitive (binary) collation,
        // so this must match exactly or EF throws "Invalid column name".
        [Column("URLs")]
        public string? Urls { get; set; }
        public int? Views { get; set; }

        #endregion Public Properties
    }
}
