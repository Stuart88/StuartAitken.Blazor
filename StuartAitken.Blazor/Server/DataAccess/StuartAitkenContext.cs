using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.DataAccess.Entities;

namespace StuartAitken.Blazor.Server.DataAccess
{
    public partial class StuartAitkenContext : DbContext
    {
        #region Public Properties

        public virtual DbSet<PortfolioProjectImage> PortfolioProjectImages { get; set; } = null!;

        public virtual DbSet<PortfolioProject> PortfolioProjects { get; set; } = null!;

        public virtual DbSet<PortfolioProjectTech> PortfolioProjectTeches { get; set; } = null!;

        public virtual DbSet<PortfolioProjectType> PortfolioProjectTypes { get; set; } = null!;

        public virtual DbSet<SecureData> SecureData { get; set; } = null!;

        #endregion Public Properties

        #region Public Constructors

        public StuartAitkenContext() { }

        public StuartAitkenContext(DbContextOptions<StuartAitkenContext> options) : base(options)
        { }

        #endregion Public Constructors

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=Database\\database.db");
            }
        }

        #endregion Protected Methods
    }
}
