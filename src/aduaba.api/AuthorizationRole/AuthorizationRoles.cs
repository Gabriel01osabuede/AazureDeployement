namespace aduaba.api.AuthorizationRole
{
    public class AuthorizationRoles
    {
         public enum Roles
            {
                Administrator,
                Moderator,
                User
            }
            public const Roles default_role = Roles.User;
    }
}