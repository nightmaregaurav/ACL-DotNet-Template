using PolicyPermission.Abstraction.MetaData;

namespace PolicyPermission.MetaData
{
    internal class UserMeta : IUserMeta
    {
        public Guid Guid { get; init; }
    }
}