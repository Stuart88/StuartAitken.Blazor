using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    public class EntityBase
    {
        [Key]
        public int ID { get; set; }
    }
}
