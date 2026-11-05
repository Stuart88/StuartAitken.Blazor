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
                // Fallback for design-time tooling only - the app itself configures this via
                // AddDbContext in Program.cs, sourced from appsettings ConnectionStrings.
                optionsBuilder.UseSqlServer(
                    "Data Source=STUART-PC;Initial Catalog=portfolio;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
                );
            }
        }

        #endregion Protected Methods
    }
}
