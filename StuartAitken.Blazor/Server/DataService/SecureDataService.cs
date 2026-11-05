using StuartAitken.Blazor.Server.DataAccess;
using StuartAitken.Blazor.Server.DataAccess.Entities;

namespace StuartAitken.Blazor.Server.DataService
{
    public class SecureDataService : ServiceBase
    {
        #region Public Constructors

        public SecureDataService(StuartAitkenContext db) : base(db) { }

        #endregion Public Constructors

        #region Public Methods

        public SecureData? GetSecureData(string name)
        {
            return this._db.SecureData.FirstOrDefault(i => i.Name == name);
        }

        #endregion Public Methods
    }
}
