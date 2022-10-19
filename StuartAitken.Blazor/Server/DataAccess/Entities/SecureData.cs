using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("SecureData")]
    public class SecureData : EntityBase
    {
        #region Public Properties

        public string Name { get; set; }
        public string Value { get; set; }

        #endregion Public Properties
    }
}
