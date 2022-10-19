namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    public class AuditedEntityBase : EntityBase
    {
        #region Public Properties

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        #endregion Public Properties
    }
}
