using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Exceptions;

namespace PolicyPermission.MetaData
{
    internal class UserMeta : IUserMeta
    {
        public UserMeta(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;
            if (user == null) return;

            var guid = user.FindFirst("uid")?.Value;

            Guid = Guid.Parse(guid ?? "");
            
            Validate();
        }

        public Guid Guid { get; init; }

        private void Validate()
        {
            if (Guid == Guid.Empty) throw new UserNotAuthenticatedException();
        }
    }
}