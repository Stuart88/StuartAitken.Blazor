using System.ComponentModel.DataAnnotations;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    public class EntityBase
    {
        #region Public Properties

        [Key]
        public int ID { get; set; }

        #endregion Public Properties
    }
}