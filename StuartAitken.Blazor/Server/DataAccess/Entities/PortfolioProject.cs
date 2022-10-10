using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProject")]
    public class PortfolioProject : AuditedEntityBase
    {
        public string Name { get; set; } = null!;
        public DateTime ProjectDate { get; set; }
        public int ProjectDurationDays { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string Tech { get; set; } = null!;
        public string? Urls { get; set; }
        public int? Views { get; set; }
        public int? Status { get; set; }

        public virtual Collection<PortfolioProjectImage>? Images { get; set; }
    }
}
