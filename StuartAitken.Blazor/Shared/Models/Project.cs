using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Shared.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ProjectDate { get; set; }
        public int ProjectDurationDays { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public string Tech { get; set; } = null!;
        public string? Urls { get; set; }
        public int? Views { get; set; }
        public int? Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public IEnumerable<ProjectImage> Images { get; set; } = new List<ProjectImage>();
    }
}
