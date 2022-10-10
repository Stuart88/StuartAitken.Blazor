using StuartAitken.Blazor.Server.DataAccess;

namespace StuartAitken.Blazor.Server.DataService
{
    public class ServiceBase
    {
        internal StuartAitkenContext _db { get; }

        public ServiceBase()
        {
            this._db = new StuartAitkenContext();
        }
    }
}
