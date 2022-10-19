using System.ComponentModel.DataAnnotations.Schema;

namespace StuartAitken.Blazor.Server.DataAccess.Entities
{
    [Table("PortfolioProjectType")]
    public class PortfolioProjectType : EntityBase
    {
        #region Public Properties

        public string Name { get; set; } = null!;

        #endregion Public Properties
    }
}
