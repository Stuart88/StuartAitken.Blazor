using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectTech")]
    public class PortfolioProjectTech : EntityBase
    {
        public string Name { get; set; } = null!;
    }
}
