using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Shared.Models
{
    public class ProjectTech
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
    }
}
