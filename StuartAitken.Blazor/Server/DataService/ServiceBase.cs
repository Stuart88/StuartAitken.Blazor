using StuartAitken.Blazor.Server.DataAccess;

namespace StuartAitken.Blazor.Server.DataService
{
    public class ServiceBase
    {
        #region Internal Properties

        internal StuartAitkenContext _db { get; }

        #endregion Internal Properties

        #region Public Constructors

        public ServiceBase()
        {
            this._db = new StuartAitkenContext();
        }

        #endregion Public Constructors
    }
}