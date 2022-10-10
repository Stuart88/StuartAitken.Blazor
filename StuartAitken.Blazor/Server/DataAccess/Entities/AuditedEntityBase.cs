namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    public class AuditedEntityBase : EntityBase
    {
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
