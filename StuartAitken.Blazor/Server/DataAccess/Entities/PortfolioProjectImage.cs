using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectImage")]
    public class PortfolioProjectImage : AuditedEntityBase
    {
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public int? PrimaryImage { get; set; }
        public int Status { get; set; }

        public virtual PortfolioProject? Project { get; set; }
    }
}
