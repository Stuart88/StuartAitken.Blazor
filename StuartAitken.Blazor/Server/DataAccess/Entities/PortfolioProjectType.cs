using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectType")]
    public class PortfolioProjectType : EntityBase
    {
        public string Name { get; set; } = null!;
    }
}
