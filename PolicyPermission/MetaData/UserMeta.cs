using PolicyPermission.Abstraction.MetaData;

namespace PolicyPermission.MetaData
{
    internal class UserMeta : IUserMeta
    {
        public Guid Guid { get; init; }
        
        public UserMeta(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;
            if (user == null) return;

            var guid = user.FindFirst("uid")?.Value;
            var parsed = Guid.TryParse(guid, out var parsedGuid);
            
            Guid = parsed ? parsedGuid : Guid.Empty;
        }
    }
}