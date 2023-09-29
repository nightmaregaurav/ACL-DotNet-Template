namespace Api.MetaData
{
    public class UserMeta
    {
        public string Guid { get; } = string.Empty;

        public UserMeta(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;
            if (user == null) return;

            var guid = user.FindFirst("uid")?.Value ?? "";
            Guid = guid;
        }
    }
}
