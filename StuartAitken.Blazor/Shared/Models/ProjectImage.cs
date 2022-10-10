using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Shared.Models
{
    public class ProjectImage
    {
        public int ID { get; set; }
        public int ProjectId { get; set; }
        public int? PrimaryImage { get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
