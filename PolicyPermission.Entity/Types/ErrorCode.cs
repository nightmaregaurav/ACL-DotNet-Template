using PolicyPermission.Entity.Types.Base;

namespace PolicyPermission.Entity.Types
{
    public class ErrorCode : Choice<ErrorCode>
    {
        public static readonly ErrorCode RoleAlreadyExists = new("xR0001");
        public static readonly ErrorCode RoleDoesNotExists = new("xR0002");
        public static readonly ErrorCode UserDoesNotExists = new("xU0001");

        public ErrorCode(string name) : base(name)
        {
        }
    }
}